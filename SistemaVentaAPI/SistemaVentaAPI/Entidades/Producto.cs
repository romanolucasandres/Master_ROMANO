namespace SistemaVentaAPI.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public int Stock { get; set; }
        public double PrecioVenta { get; set; }
        public string Descripcion { get; set; }

        public Usuario Usuario { get; set; }

        public Producto()
        {

        }

    }
}
