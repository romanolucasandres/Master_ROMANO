namespace SistemaVentaAPI.Entidades
{
    public class Venta
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Producto { get; set; }
        public int Id_Usuario { get; set; }
        public DateTime FechaHora { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Total { get; set; }

        
    }
}
