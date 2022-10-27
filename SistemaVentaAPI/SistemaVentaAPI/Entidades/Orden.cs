namespace SistemaVentaAPI.Entidades
{
    public class Orden:Venta
    {
       
        public List<Venta> ListaVentas { get; set; }
        public List<Producto> ListaProductos { get; set; }
        public double Total { get; set; }


        public void TotalOrden()
        {
            foreach (var item in ListaVentas)
            {
                Total = Total + item.Total; 
            }
           
        }
    }
}
