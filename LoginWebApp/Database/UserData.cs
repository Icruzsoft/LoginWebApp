using System;
using System.Data;
using System.Data.SqlClient;
using LoginWebApp.Models;

namespace LoginWebApp.Database
{
    public class UserData
    {
        private static string CadenaSQL = "Data Source=DESKTOP-IT8CIBE\\SQLEXPRESS;Initial Catalog=APILOGIN;User ID=iCross;Password=SA";

        public static bool Registrar(Client client)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    string query = "insert into Client(UserName, LastName, Email, PhoneNumber, Password, Restore, Confirmed, Token, TermsAccepted, CreatedDate, UserTypeId)";
                    query += " values(@UserName, @LastName, @Email, @PhoneNumber, @Password, @Restore, @Confirmed, @Token, @TermsAccepted, @CreatedDate, @UserTypeId)";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserName", client.UserName);
                    cmd.Parameters.AddWithValue("@LastName", client.LastName);
                    cmd.Parameters.AddWithValue("@Email", client.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Password", client.Password);
                    cmd.Parameters.AddWithValue("@Restore", client.Restore);
                    cmd.Parameters.AddWithValue("@Confirmed", client.Confirmed);
                    cmd.Parameters.AddWithValue("@Token", client.Token);
                    cmd.Parameters.AddWithValue("@TermsAccepted", client.TermsAccepted);
                    cmd.Parameters.AddWithValue("@CreatedDate", client.CreatedDate);
                    cmd.Parameters.AddWithValue("@UserTypeId", client.UserTypeId);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool RestablecerContraseña(string email, string nuevaContraseña, string token)
        {
            bool result = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("RestablecerContraseña", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@NewPassword", nuevaContraseña);
                    command.Parameters.AddWithValue("@Token", token); 

                    result = command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static bool ConfirmarCuenta(string token)
        {
            bool result = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("ConfirmarCuenta", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Token", token);

                    result = command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static Client Validar(string email, string password)
        {
            Client client = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    string query = "select UserName, LastName, Email, PhoneNumber, Password, Restore, Confirmed, Token, TermsAccepted, CreatedDate, UserTypeId from Client";
                    query += " where Email=@Email and Password = @Password";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            client = new Client()
                            {
                                UserName = dr["UserName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                Email = dr["Email"].ToString(),
                                PhoneNumber = dr["PhoneNumber"].ToString(),
                                Password = dr["Password"].ToString(),
                                Restore = (bool)dr["Restore"],
                                Confirmed = (bool)dr["Confirmed"],
                                Token = dr["Token"].ToString(),
                                TermsAccepted = (bool)dr["TermsAccepted"],
                                CreatedDate = (DateTime)dr["CreatedDate"],
                                UserTypeId = (int)dr["UserTypeId"]
                            };
                        }
                    }
                }

                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Client Obtener(string email)
        {
            Client client = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    string query = "select UserName, LastName, Email, PhoneNumber, Password, Restore, Confirmed, Token, TermsAccepted, CreatedDate, UserTypeId from Client";
                    query += " where Email=@Email";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            client = new Client()
                            {
                                UserName = dr["UserName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                Email = dr["Email"].ToString(),
                                PhoneNumber = dr["PhoneNumber"].ToString(),
                                Password = dr["Password"].ToString(),
                                Restore = (bool)dr["Restore"],
                                Confirmed = (bool)dr["Confirmed"],
                                Token = dr["Token"].ToString(),
                                TermsAccepted = (bool)dr["TermsAccepted"],
                                CreatedDate = (DateTime)dr["CreatedDate"],
                                UserTypeId = (int)dr["UserTypeId"]
                            };
                        }
                    }
                }

                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}