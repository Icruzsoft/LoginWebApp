using System;
using System.Data;
using System.Data.SqlClient;
using LoginWebApp.Models;

namespace LoginWebApp.Database
{
    public static class UserTypeData
    {
        private static string CadenaSQL = "Data Source=DESKTOP-IT8CIBE\\SQLEXPRESS;Initial Catalog=APILOGIN;User ID=iCross;Password=SA";

        public static UserType GetUserTypeById(int userTypeId)
        {
            UserType userType = null;
            using (SqlConnection connection = new SqlConnection(CadenaSQL))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUserTypeById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserTypeId", userTypeId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userType = new UserType
                        {
                            UserTypeId = (int)reader["UserTypeId"],
                            TypeName = reader["TypeName"].ToString(),
                            IdentityIdentity = (int)reader["IdentityIdentity"],
                            IdentitySeed = (int)reader["IdentitySeed"],
                            IdentityIncrement = (int)reader["IdentityIncrement"]
                        };
                    }
                }
            }
            return userType;
        }

        // Metodos

    }
}
