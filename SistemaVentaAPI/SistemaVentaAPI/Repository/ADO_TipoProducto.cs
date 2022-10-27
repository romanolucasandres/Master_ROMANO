using SistemaVentaAPI.Datos;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.IAltaModificar;
using System.Data;
using System.Data.SqlClient;

namespace SistemaVentaAPI.Repository
{
    public class ADO_TipoProducto
    {
        public static List<TipoProducto> Listar()
        {
            var oLista = new List<TipoProducto>();
            var cn = new Connection();
            using (var connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_Tipo_Producto", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    oLista.Add(new TipoProducto()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                    });
                }
                dr.Close();
                
                connection.Close();
            }
            return oLista;
        }

        public static TipoProducto Obtener(int id)
        {
            var oTipoProducto = new TipoProducto();

            
            using (var connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_Tipo_Producto", connection);
                cmd.Parameters.AddWithValue("Id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oTipoProducto.Id = Convert.ToInt32(dr.GetValue(0));
                        oTipoProducto.Nombre = dr.GetValue(1).ToString();
                    }
                    dr.Close();
                }
                
                connection.Close();
            }
            return oTipoProducto;
        }


        public static void Guardar(TipoProducto oTipoProducto)
        {
            if (oTipoProducto != null)
            {
                using (var connection = new SqlConnection(Connection.ConnectionString()))
                {
                    connection.Open();

                    SqlCommand cmd;

                    if (oTipoProducto.Id == 0)
                    {
                        cmd = new SqlCommand("sp_Alta_Usuario", connection);
                    }
                    else
                    {
                        cmd = new SqlCommand("sp_Modificar_Usuario", connection);
                        cmd.Parameters.AddWithValue("@Id", oTipoProducto.Id);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", oTipoProducto.Nombre);

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            throw new Exception("Por favor intente nuevamente");
        }
        public static void Eliminar(int id)
        {          
            try
            {
                var cn = new Connection();
                using (var connection = new SqlConnection(Connection.ConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar_Tipo_Producto", connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.ExecuteNonQuery();
                    connection.Open();
                }
                
            }
            catch (Exception e)
            {
                string error = e.Message;
                
            }

            
        }

        
    }
}
