using IntegrandoApisConAdo.Models;
using IntegrandoApisConAdo.Repository;
using System.Data.SqlClient;
using System.Data;

namespace IntegrandoApisConAdo.Service
{
    internal class UsuarioService : GenericDB
    {
        public Usuario ValidateSession(string userName, string password)
        {
            string cmdText = "SELECT * FROM Usuario" +
                " WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña;";

            Usuario _usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar)).Value = userName;
                    sqlCommand.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar)).Value = password;

                    sqlConnection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt64(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();

                                _usuario = usuario;
                            }
                        }
                    }
                }
            }

            return _usuario;
        }
    }
}
