using IntegrandoApisConAdo.Models;
using System.Data.SqlClient;
using System.Data;

namespace IntegrandoApisConAdo.Repository
{
    public class ProductoVendidoRepository : GenericDB
    {
        public void AddProductoVendido(List<VentaEfectuada> ventaEfectuadas)
        {

            string cmdText = "INSERT INTO ProductoVendido VALUES " +
                "(@Stock, @IdProducto, @IdVenta);";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        foreach (var item in ventaEfectuadas)
                        {
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add(new SqlParameter("@Stock", SqlDbType.VarChar, 255)).Value = item.StockProducto;
                            sqlCommand.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Money)).Value = item.IdProducto;
                            sqlCommand.Parameters.Add(new SqlParameter("@IdVenta", SqlDbType.Money)).Value = item.IdVenta;

                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public bool DeleteProductoVendido(int id)
        {
            string cmdText = "DELETE FROM ProductoVendido WHERE idProducto = @id";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = id;

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
        public static bool DeleteProductoVendidoPorIdVenta(int idVenta)
        {
            string cmdText = "DELETE FROM ProductoVendido WHERE IdVenta = @idVenta;";

            try
            {
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
        public List<Producto> GetProductosVendidosByIdUser(int idUsuario)
        {

            string cmdText = "SELECT * from Producto as P " +
                " inner join ProductoVendido as PV" +
                " on PV.IdProducto = P.id" +
                " where P.idUsuario = @idUsuario;";

            List<Producto> _productos = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.BigInt)).Value = idUsuario;


                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt64(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.Costo = Convert.ToDecimal(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToDecimal(dataReader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt64(dataReader["IdUsuario"]);

                                _productos.Add(producto);
                            }
                        }
                    }
                }
            }

            return _productos;
        }
        public static List<Producto> GetProductosVendidosByIdVenta(long idVenta)
        {

            string cmdText = "SELECT * from Producto as P " +
                "inner join ProductoVendido as PV " +
                "ON PV.IdProducto = P.Id " +
                "where PV.IdVenta = @idVenta";

            List<Producto> _productos = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("@idVenta", SqlDbType.BigInt)).Value = idVenta;


                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt64(dataReader["Id"]);
                                producto.Descripciones = dataReader["Stock"].ToString();
                                producto.Costo = Convert.ToDecimal(dataReader["IdProducto"]);
                                producto.PrecioVenta = Convert.ToDecimal(dataReader["IdVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["IdVenta"]);
                                producto.IdUsuario = Convert.ToInt64(dataReader["IdVenta"]);

                                _productos.Add(producto);
                            }
                        }
                    }
                }
            }

            return _productos;
        }

        public static List<ProductoVendido> GetProductosVendidosSinDetalleByIdVenta(long idVenta)
        {
            string cmdText = "SELECT * from ProductoVendido as PV " +
                "where PV.IdVenta = @idVenta";

            List<ProductoVendido> _productosVendido = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("@idVenta", SqlDbType.BigInt)).Value = idVenta;


                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt64(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt64(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt64(dataReader["IdVenta"]);

                                _productosVendido.Add(productoVendido);
                            }
                        }
                    }
                }
            }

            return _productosVendido;
        }

    }
}
