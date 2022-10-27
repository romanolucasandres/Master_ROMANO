using SistemaVentaAPI.Datos;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.IAltaModificar;
using System.Data;
using System.Data.SqlClient;
using static SistemaVentaAPI.Controllers.VentaController;

namespace SistemaVentaAPI.Repository
{
    public class ADO_Venta /*: IAltaModificar<Venta>*/
    {
        public static List<Venta> DevolverVentas()
        {
            var listaVentas = new List<Venta>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Venta";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var venta = new Venta();
                    venta.Id = Convert.ToInt32(reader.GetValue(0));
                    venta.Id_Cliente = Convert.ToInt32(reader.GetValue(1));
                    venta.Id_Producto = Convert.ToInt32(reader.GetValue(2));
                    venta.Id_Usuario = Convert.ToInt32(reader.GetValue(3));
                    venta.FechaHora = (DateTime)reader.GetValue(4);
                    venta.Cantidad = Convert.ToInt32(reader.GetValue(5));
                    venta.PrecioUnitario = Convert.ToDouble(reader.GetValue(6));
                    venta.Total = Convert.ToDouble(reader.GetValue(7));

                    listaVentas.Add(venta);
                }

                reader.Close();
                connection.Close();
            }
            return listaVentas;
        }

        public static List<Venta> DevolverVentasPorUsuario()
        {
            var listaVentas = new List<Venta>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT v.Id, c.Apellido AS Cliente, p.Nombre AS Nombre_Producto, u.Nombre AS " +
                    "Nombre_Usuario, v.FechaHora, v.Cantidad,v.PrecioUnitario,v.Total FROM Venta AS v INNER JOIN Cliente AS c " +
                    "ON v.Id_Cliente = c.id INNER JOIN Producto AS p ON v.Id_Producto = p.Id INNER JOIN Usuario AS u ON v.Id_Usuario " +
                    "= u.Id WHERE u.Nombre = @usuario";

                string nombreUsu = "Lucas";
                cmd.Parameters.Add(new SqlParameter("@usuario", nombreUsu));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var venta = new Venta();
                  

                    venta.Id = Convert.ToInt32(reader.GetValue(0));
                    venta.Id_Cliente = Convert.ToInt32(reader.GetValue(1));
                    venta.Id_Producto = Convert.ToInt32(reader.GetValue(2));
                    venta.Id_Usuario = Convert.ToInt32(reader.GetValue(3));
                    venta.FechaHora = Convert.ToDateTime(reader.GetValue(4));
                    venta.Cantidad = Convert.ToInt32(reader.GetValue(5));
                    venta.PrecioUnitario = Convert.ToDouble(reader.GetValue(6));
                    venta.Total = Convert.ToDouble(reader.GetValue(7));

                    listaVentas.Add(venta);
                }

                reader.Close();
                connection.Close();
            }
            return listaVentas;
        }

        public static void Eliminar(int x)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Venta WHERE Id =@IdVenta ";

                var p = new SqlParameter();
                p.ParameterName = "IdVenta";
                p.SqlDbType = SqlDbType.BigInt;
                p.Value = x;

                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Alta(Orden orden)
        {
           

            try
            {
                
                using (var connection = new SqlConnection(Connection.ConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Alta_Venta", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", orden.Id_Cliente);
                    cmd.Parameters.AddWithValue("@IdProducto", orden.Id_Producto);
                    cmd.Parameters.AddWithValue("@IdUsuario", orden.Id_Usuario); ;
                    cmd.Parameters.AddWithValue("@FechaHora", orden.FechaHora);
                    cmd.Parameters.AddWithValue("@Cantidad", orden.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", orden.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@Total", orden.Total);

                    cmd.ExecuteScalar();


                    foreach (Producto producto in orden.ListaProductos)
                    {

                        //Actualizar Stock en Productos
                        SqlCommand cmd2 = new SqlCommand("UPDATE Producto SET Stock = Stock - @Stock WHERE Id_Producto = @IdProducto", connection);
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = producto.Stock;
                        cmd2.Parameters.Add(new SqlParameter("Id_Producto", SqlDbType.BigInt)).Value = producto.Id;
                        cmd2.ExecuteNonQuery();
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                string error = e.Message;

            }


             
               
            
        }

    
        public static void Modificar(Venta oVenta)
        {
            
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Modificar_Venta", connection);
                    cmd.Parameters.AddWithValue("@Id", oVenta.Id);
                    cmd.Parameters.AddWithValue("@IdCliente", oVenta.Id_Cliente);
                    cmd.Parameters.AddWithValue("@IdProducto", oVenta.Id_Producto);
                    cmd.Parameters.AddWithValue("@IdUsuario", oVenta.Id_Usuario);
                    cmd.Parameters.AddWithValue("@FechaHora", oVenta.FechaHora);
                    cmd.Parameters.AddWithValue("@Cantidad", oVenta.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", oVenta.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@Total", oVenta.Total);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            
        }
    } 
}
