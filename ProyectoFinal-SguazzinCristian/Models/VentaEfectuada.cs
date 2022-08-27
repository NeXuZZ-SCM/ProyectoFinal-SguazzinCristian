namespace IntegrandoApisConAdo.Models
{
    public class VentaEfectuada
    {
        public long IdProducto { get; set; }
        public int StockProducto { get; set; }
        public long IdVendedor { get; set; }
        public long IdVenta { get; set; }
        public int StockEnAlmacen { get; set; }
    }
}
