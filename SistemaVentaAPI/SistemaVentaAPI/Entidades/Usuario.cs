namespace SistemaVentaAPI.Entidades
{
    public class Usuario: Persona
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

        public Usuario() 
        {
            Id = 0;
            Nombre = String.Empty;
            Apellido = String.Empty;
            Email = String.Empty;
            NombreUsuario = String.Empty;
            Contraseña = String.Empty;

        }
    }
}
