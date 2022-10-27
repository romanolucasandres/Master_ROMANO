

using SistemaVentaAPI.Datos;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.IAltaModificar;
using System.Data;
using System.Data.SqlClient;

namespace SistemaVentaAPI.Repository
{
    public class ADO_Usuario
    {

        public static List<Usuario> DevolverUsuarios()
        {
            var listaUsuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.Email = reader.GetValue(3).ToString();
                    usuario.NombreUsuario = reader.GetValue(4).ToString();
                    usuario.Contraseña = reader.GetValue(5).ToString();

                    listaUsuarios.Add(usuario);
                }

                reader.Close();
                connection.Close();
            }
            return listaUsuarios;
        }

        

        public static Usuario traerUsuario(string nombreUsu)
        {
            var usuario = new Usuario();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario WHERE nombreUsuario =@usuario ";

                
                cmd.Parameters.Add(new SqlParameter("@usuario", nombreUsu));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.Email = reader.GetValue(3).ToString();
                    usuario.NombreUsuario = reader.GetValue(4).ToString();
                    usuario.Contraseña = reader.GetValue(5).ToString();

                   
                }

                reader.Close();
                connection.Close();
            }
            return usuario;
        }

        public static Usuario ValidarUsuario(string nombreUsuario,string password)
        {
            var usuario = new Usuario();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario WHERE nombreUsuario =@usuario AND Contraseña = @pass ";

                cmd.Parameters.Add(new SqlParameter("usuario", nombreUsuario));
                cmd.Parameters.Add(new SqlParameter("pass", password));
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader.GetValue(0));
                        usuario.Nombre = reader.GetValue(1).ToString();
                        usuario.Apellido = reader.GetValue(2).ToString();
                        usuario.Email = reader.GetValue(3).ToString();
                        usuario.NombreUsuario = reader.GetValue(4).ToString();
                        usuario.Contraseña = reader.GetValue(5).ToString();
                    }
                }
                else
                    {
                        usuario.Id = 0;
                        usuario.Nombre = "";
                        usuario.Apellido = "";
                        usuario.Email = "";
                        usuario.NombreUsuario = "";
                        usuario.Contraseña = "";
                    }

                
                reader.Close();
                connection.Close();               
            }
            return usuario;

        }

        public static void Guardar(Usuario oUsuario)
        {
           
            if(oUsuario != null)
            {               
                    using (var connection = new SqlConnection(Connection.ConnectionString()))
                    {
                        connection.Open();

                        SqlCommand cmd;

                        if (oUsuario.Id == 0)
                        {
                            cmd = new SqlCommand("sp_Alta_Usuario", connection);
                        }
                        else
                        {
                            cmd = new SqlCommand("sp_Modificar_Usuario", connection);
                            cmd.Parameters.AddWithValue("@Id", oUsuario.Id);
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", oUsuario.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", oUsuario.Apellido);
                        cmd.Parameters.AddWithValue("@Email", oUsuario.Email); ;
                        cmd.Parameters.AddWithValue("@NombreUsuario", oUsuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@Contraseña", oUsuario.Contraseña);


                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
            }
            throw new Exception("Por favor intente nuevamente");      
        }
        public static void Eliminar(int oUsuario)
        {

            try
            {
                var cn = new Connection();
                using (var connection = new SqlConnection(Connection.ConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar_Usuario", connection);
                    cmd.Parameters.AddWithValue("Id", oUsuario);

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
