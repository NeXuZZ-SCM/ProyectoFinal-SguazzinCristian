using IntegrandoApisConAdo.Models;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace IntegrandoApisConAdo.Repository
{
    public class VentaRepository : GenericDB
    {
        public bool AddVenta(List<Producto> listaProductos, int idVendedor)
        {
            //1 Agregamos venta
            int idNuevaVenta = 0;

            string cmdText = "INSERT INTO Venta VALUES " +
                "(@Comentarios); SELECT CAST(scope_identity() AS int)";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@Comentarios", SqlDbType.VarChar, 255)).Value = "Nueva Venta";

                        idNuevaVenta = (int)sqlCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            if (idNuevaVenta != 0)
            {
                List<VentaEfectuada> ventaEfectuadas = AnalizarProductos(listaProductos, idVendedor, idNuevaVenta);

                //chequeamos si por casualidad intentan vender mas de lo que tenemos en almacen
                foreach (var item in ventaEfectuadas)
                {
                    if (item.StockEnAlmacen < item.StockProducto)
                    {
                        return false;
                    }
                }
                //2 agregamos productos vendidos
                ProductoVendidoRepository productoVendidoRepository = new ProductoVendidoRepository();
                productoVendidoRepository.AddProductoVendido(ventaEfectuadas);
                //3 restamos stock
                ProductoRepository productoRepository = new ProductoRepository();
                foreach (var item in ventaEfectuadas)
                {
                    var NuevoStockAlmacen = item.StockEnAlmacen - item.StockProducto;//en este punto nunca obtendriamos un numero negativo
                    productoRepository.UpdateStockProducto(item.IdProducto, NuevoStockAlmacen);
                }
            }

            return true;
        }
        private List<VentaEfectuada> AnalizarProductos(List<Producto> listaProductos, int idVendedor, int idNuevaVenta)
        {
            //necesito id producto 
            //stock producto
            //id del vendedor
            List<VentaEfectuada> ventaEfectuadas = new List<VentaEfectuada>();

            bool ExistProduct = false;
            foreach (var itemProducto in listaProductos)
            {
                foreach (var itemVenta in ventaEfectuadas)
                {
                    if (itemVenta.IdProducto == itemProducto.Id)
                    {
                        itemVenta.StockProducto++;
                        ExistProduct = true;
                    }
                }
                if (ExistProduct is not true)
                {
                    VentaEfectuada productoRecienVendido = new VentaEfectuada();
                    productoRecienVendido.IdProducto = itemProducto.Id;
                    productoRecienVendido.IdVendedor = idVendedor;
                    productoRecienVendido.StockProducto = 1;
                    productoRecienVendido.IdVenta = idNuevaVenta;
                    productoRecienVendido.StockEnAlmacen = itemProducto.Stock;

                    ventaEfectuadas.Add(productoRecienVendido);
                }
                ExistProduct = false;
            }
            return ventaEfectuadas; //si llegamos a este punto SIEMPRE tendremos una lista de productos vendidos ya que controlamos los productos en cero en el lado del controller
        }
        public List<VentasYProductos> GetVentasYProductos()
        {
            string cmdText = "SELECT * FROM Venta";
            List<VentasYProductos> listaVentasYProductos = new List<VentasYProductos>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                VentasYProductos ventasYProductos = new VentasYProductos();

                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt64(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

                                ventasYProductos.Venta = venta;
                                ventasYProductos.ListaProducto = ProductoVendidoRepository.GetProductosVendidosByIdVenta(venta.Id);

                                listaVentasYProductos.Add(ventasYProductos);
                            }
                        }
                    }
                }
            }
            return listaVentasYProductos;
        }
        public bool DeleteVenta(int idVenta)
        {
            //volamos venta
            string cmdText = "DELETE FROM Venta WHERE id = @idVenta";

            try
            {
                List<ProductoVendido> ListaProductosVendidos = ProductoVendidoRepository.GetProductosVendidosSinDetalleByIdVenta(idVenta);
                //actualizamos stock productos
                foreach (var item in ListaProductosVendidos)
                {
                    ProductoRepository.UpdateStockProductoXProductoVendido(item.IdProducto, item.Stock);
                }
                //volamos productos vendidos
                ProductoVendidoRepository.DeleteProductoVendidoPorIdVenta(idVenta);



                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@idVenta", SqlDbType.BigInt)).Value = idVenta;

                        sqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false; // en caso de error 
            }


        }

    }

}
