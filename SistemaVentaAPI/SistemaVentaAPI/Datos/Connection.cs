using System.Data.SqlClient;

namespace SistemaVentaAPI.Datos
{
    public class Connection
    {
      
        public static string ConnectionString()
        {
            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();
            connectionBuilder.DataSource = @"LAPTOP-RJIKNK5J\SQLEXPRESS";
            connectionBuilder.InitialCatalog = "SistemaVenta";
            connectionBuilder.IntegratedSecurity = true;
            var cs = connectionBuilder.ConnectionString;
            return cs;
        }
    }
}
