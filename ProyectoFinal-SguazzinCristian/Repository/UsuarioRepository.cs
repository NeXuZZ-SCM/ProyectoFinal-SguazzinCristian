using IntegrandoApisConAdo.Models;
using System.Data;
using System.Data.SqlClient;

namespace IntegrandoApisConAdo.Repository
{
    public class UsuarioRepository : GenericDB
    {
        public bool AddUser(Usuario usuario)
        {
            Usuario _userDataBase = GetUsuariosByUserName(usuario.NombreUsuario);
            if (_userDataBase.NombreUsuario is null)
            {
                string cmdText = "INSERT INTO Usuario VALUES" +
                "(@Nombre, @Apellido, @NombreUsuario , @Contraseña, @Mail);";

                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                        {
                            sqlConnection.Open();

                            sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 255)).Value = usuario.Nombre;
                            sqlCommand.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 255)).Value = usuario.Apellido;
                            sqlCommand.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 255)).Value = usuario.NombreUsuario;
                            sqlCommand.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar, 255)).Value = usuario.Contraseña;
                            sqlCommand.Parameters.Add(new SqlParameter("@Mail", SqlDbType.VarChar, 255)).Value = usuario.Mail;

                            sqlCommand.ExecuteNonQuery();
                        }

                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false; // en caso de error 
                }
            }
            return false; // en caso de error por usuario repetido
        }
        public int UpdateUser(Usuario usuario)
        {
            int rowsAffected = 0;

            string cmdText = "UPDATE Usuario SET " +
                "Nombre = @Nombre, " +
                "Apellido = @Apellido, " +
                "NombreUsuario = @NombreUsuario , " +
                "Contraseña = @Contraseña , " +
                "Mail = @Mail " +
                "WHERE Id=@id ;";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 255)).Value = usuario.Nombre;
                        sqlCommand.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 255)).Value = usuario.Apellido;
                        sqlCommand.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 255)).Value = usuario.NombreUsuario;
                        sqlCommand.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar, 255)).Value = usuario.Contraseña;
                        sqlCommand.Parameters.Add(new SqlParameter("@Mail", SqlDbType.VarChar, 255)).Value = usuario.Mail;
                        sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar, 255)).Value = usuario.Id;

                        rowsAffected = sqlCommand.ExecuteNonQuery();
                    }

                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                return -1; // en caso de error 
            }
        }
        public Usuario GetUsuariosByUserName(string userName)
        {
            string cmdText = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario;";
            Usuario _usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 20)).Value = userName;


                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
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
        public bool DeleteUser(long id)
        {
            string cmdText = "DELETE FROM Usuario WHERE Id = @id";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = id;

                        sqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false; // en caso de error 
            }

        }

    }
}
