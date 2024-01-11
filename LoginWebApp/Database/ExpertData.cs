using System;
using System.Data;
using System.Data.SqlClient;
using LoginWebApp.Models;

namespace LoginWebApp.Database
{
    public static class ExpertData
    {
        private static string CadenaSQL = "Data Source=DESKTOP-IT8CIBE\\SQLEXPRESS;Initial Catalog=APILOGIN;User ID=iCross;Password=SA";

        public static bool InsertExpert(Expert expert)
        {
            bool result = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertExpert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // Agregando todos los parámetros necesarios
                    command.Parameters.AddWithValue("@UserName", expert.UserName);
                    command.Parameters.AddWithValue("@Address", expert.Address);
                    command.Parameters.AddWithValue("@City", expert.City);
                    command.Parameters.AddWithValue("@State", expert.State);
                    command.Parameters.AddWithValue("@Country", expert.Country);
                    command.Parameters.AddWithValue("@EducationLevel", expert.EducationLevel);
                    command.Parameters.AddWithValue("@LearningMethod", expert.LearningMethod);
                    command.Parameters.AddWithValue("@CVPath", expert.CVPath);
                    command.Parameters.AddWithValue("@CVContent", expert.CVContent);
                    command.Parameters.AddWithValue("@CVExtension", expert.CVExtension);
                    command.Parameters.AddWithValue("@UserTypeId", expert.UserTypeId);

                    int affectedRows = command.ExecuteNonQuery();
                    result = affectedRows > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Exception: " + ex.Message);
            }
            return result;
        }

        public static Expert GetExpertById(int userId)
        {
            Expert expert = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetExpertById", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            expert = new Expert
                            {
                                UserId = (int)dr["UserId"],
                                UserName = dr["UserName"].ToString(),
                                Address = dr["Address"].ToString(),
                                City = dr["City"].ToString(),
                                State = dr["State"].ToString(),
                                Country = dr["Country"].ToString(),
                                EducationLevel = dr["EducationLevel"].ToString(),
                                LearningMethod = dr["LearningMethod"].ToString(),
                                CVPath = dr["CVPath"].ToString(),
                                CVContent = (byte[])dr["CVContent"],
                                CVExtension = dr["CVExtension"].ToString(),
                                UserTypeId = dr.IsDBNull(dr.GetOrdinal("UserTypeId")) ? (int?)null : (int)dr["UserTypeId"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return expert;
        }

        public static Expert ValidateExpert(string email, string password)
        {
            Expert expert = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("ValidateExpert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            expert = new Expert
                            {
                                UserId = (int)dr["UserId"],
                                UserName = dr["UserName"].ToString(),
                                Address = dr["Address"].ToString(),
                                City = dr["City"].ToString(),
                                State = dr["State"].ToString(),
                                Country = dr["Country"].ToString(),
                                EducationLevel = dr["EducationLevel"].ToString(),
                                LearningMethod = dr["LearningMethod"].ToString(),
                                CVPath = dr["CVPath"].ToString(),
                                CVContent = (byte[])dr["CVContent"],
                                CVExtension = dr["CVExtension"].ToString(),
                                UserTypeId = dr.IsDBNull(dr.GetOrdinal("UserTypeId")) ? (int?)null : (int)dr["UserTypeId"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return expert;
        }

       
    }
}
