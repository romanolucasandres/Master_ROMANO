namespace SistemaVentaAPI.Entidades
{
    public class Cliente: Persona
    {
        public DateTime FechaIngreso { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Dni { get; set; }
        public string Observacion { get; set; }

        public Cliente()
        {
            Id = 0;
            Nombre = String.Empty;
            Apellido = String.Empty;
            Email = String.Empty;
            FechaIngreso = DateTime.Now;
            FechaNacimiento = DateTime.Now;
            Telefono = String.Empty;
            Dni = String.Empty;
            Observacion = String.Empty;
            
        }
    }
}
