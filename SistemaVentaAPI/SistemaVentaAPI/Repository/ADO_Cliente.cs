using SistemaVentaAPI.Datos;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.IAltaModificar;
using System.Data;
using System.Data.SqlClient;

namespace SistemaVentaAPI.Repository
{
    public class ADO_Cliente
    {
        public static List<Cliente> DevolverClientes()
        {
            var listaCliente = new List<Cliente>();


            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Cliente";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var cliente = new Cliente();
                    cliente.Id = Convert.ToInt32(reader.GetValue(0));
                    cliente.Nombre = reader.GetValue(1).ToString();
                    cliente.Apellido = reader.GetValue(2).ToString();
                    cliente.FechaIngreso = Convert.ToDateTime(reader.GetValue(3));
                    cliente.FechaNacimiento = Convert.ToDateTime(reader.GetValue(4));
                    cliente.Email = reader.GetValue(5).ToString();
                    cliente.Dni = reader.GetValue(6).ToString();
                    cliente.Observacion = reader.GetValue(7).ToString();

                    listaCliente.Add(cliente);
                }

                reader.Close();
                connection.Close();
            }
            return listaCliente;
        }

        public static void Eliminar(int x)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Cliente WHERE Id =@IdCliente ";

                var p = new SqlParameter();
                p.ParameterName = "IdCliente";
                p.SqlDbType = SqlDbType.BigInt;
                p.Value = x;

                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Alta(Cliente x)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Cliente(Nombre,Apellido,FechaIngreso,FechaNacimiento,Telefono,Mail,Dni,Observacion) " +
                    "VALUES (@NombreCliente,@ApellidoCliente,@FechaIng,@FechaNac,@Telefono,@Mail,@Dni,@Observacion) ";

                var pNombre = new SqlParameter();
                pNombre.ParameterName = "NombreCliente";
                pNombre.SqlDbType = SqlDbType.VarChar;
                pNombre.Value = x.Nombre;
                
                var pApellido = new SqlParameter();
                pApellido.ParameterName = "ApellidoCliente";
                pApellido.SqlDbType = SqlDbType.VarChar;
                pApellido.Value = x.Apellido;
                
                var pFechaIng = new SqlParameter();
                pFechaIng.ParameterName = "FechaIng";
                pFechaIng.SqlDbType = SqlDbType.DateTime;
                pFechaIng.Value = x.FechaIngreso;
                
                var pFechaNac = new SqlParameter();
                pFechaNac.ParameterName = "FechaNac";
                pFechaNac.SqlDbType = SqlDbType.Date;
                pFechaNac.Value = x.FechaNacimiento;
                
                var pTel = new SqlParameter();
                pTel.ParameterName = "Telefono";
                pTel.SqlDbType = SqlDbType.VarChar;
                pTel.Value = x.Telefono;
                
                var pMail = new SqlParameter();
                pMail.ParameterName = "Mail";
                pMail.SqlDbType = SqlDbType.VarChar;
                pMail.Value = x.Email;
                
                var pDni = new SqlParameter();
                pDni.ParameterName = "Dni";
                pDni.SqlDbType = SqlDbType.VarChar;
                pDni.Value = x.Dni;
                
                var pObs = new SqlParameter();
                pObs.ParameterName = "Observacion";
                pObs.SqlDbType = SqlDbType.VarChar;
                pObs.Value = x.Observacion;

                cmd.Parameters.Add(pNombre);
                cmd.Parameters.Add(pApellido);
                cmd.Parameters.Add(pFechaIng);
                cmd.Parameters.Add(pFechaNac);
                cmd.Parameters.Add(pTel);
                cmd.Parameters.Add(pMail);
                cmd.Parameters.Add(pDni);
                cmd.Parameters.Add(pObs);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            
        }

        public static void Modificar(Cliente x)
        {
            
        }
    }

    
}
