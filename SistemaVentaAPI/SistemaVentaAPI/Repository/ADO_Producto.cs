using SistemaVentaAPI.Datos;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.IAltaModificar;
using System.Data;
using System.Data.SqlClient;

namespace SistemaVentaAPI.Repository
{
    public class ADO_Producto
    {
        public static List<Producto> DevolverProductosPorUsuario()
        {
            var listaProductos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {

                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT p.Id,p.Nombre,tp.Nombre as Tipo_Producto, p.Stock, p.PrecioVenta, p.Descripcion,u.Id as " +
                    "Id_Usuario FROM Producto as p INNER JOIN Tipo_Producto as tp ON p.Id_TipoProducto = tp.Id INNER JOIN Usuario as u " +
                    "ON p.Id_Usuario = u.Id WHERE u.Id = @usuario ";

                int IdUsu = 1;
                cmd.Parameters.Add(new SqlParameter("@usuario", IdUsu));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var producto = new Producto();
                    producto.TipoProducto = new TipoProducto();
                    producto.Usuario = new Usuario();

                    producto.Id = Convert.ToInt32(reader.GetValue(0));
                    producto.Nombre = reader.GetValue(1).ToString();
                    producto.TipoProducto.Nombre = reader.GetValue(2).ToString();
                    producto.Stock = Convert.ToInt32(reader.GetValue(3));
                    producto.PrecioVenta = Convert.ToDouble(reader.GetValue(4));
                    producto.Descripcion = reader.GetValue(5).ToString();
                    producto.Usuario.Id = Convert.ToInt32(reader.GetValue(6));

                    listaProductos.Add(producto);
                }

                reader.Close();
                connection.Close();
            }
            return listaProductos;
        }

        public static void Eliminar(int x)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Producto WHERE Id =@IdProducto ";

                var p = new SqlParameter();
                p.ParameterName = "IdProducto";
                p.SqlDbType = SqlDbType.BigInt;
                p.Value = x;

                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
      
        public static void Guardar(Producto oProducto)
        {

            if (oProducto != null)
            {
                using (var connection = new SqlConnection(Connection.ConnectionString()))
                {
                    connection.Open();

                    SqlCommand cmd;

                    if (oProducto.Id == 0)
                    {
                        cmd = new SqlCommand("sp_Alta_Producto", connection);
                    }
                    else
                    {
                        cmd = new SqlCommand("sp_Modificar_Usuario", connection);
                        cmd.Parameters.AddWithValue("@Id", oProducto.Id);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", oProducto.Nombre);
                    cmd.Parameters.AddWithValue("@Id_TipoProducto", oProducto.TipoProducto);
                    cmd.Parameters.AddWithValue("@Stock", oProducto.Stock); ;
                    cmd.Parameters.AddWithValue("@PrecioVenta", oProducto.PrecioVenta);
                    cmd.Parameters.AddWithValue("@Descripcion", oProducto.Descripcion);
                    cmd.Parameters.AddWithValue("@Id_Usuario", oProducto.Usuario);


                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            throw new Exception("Por favor intente nuevamente");
        }
        
    }
}
