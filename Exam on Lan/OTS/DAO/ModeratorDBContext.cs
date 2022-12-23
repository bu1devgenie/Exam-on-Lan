using Microsoft.Data.SqlClient;
using OTS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.DAO
{
    public class ModeratorDBContext : DBContext 
    {
        public Moderator GetModerator(string username, string password)
        {
            try
            {
                string sql = @"SELECT [UserName]
                              ,[FullName]
                              ,[Password]
                          FROM [Moderator]
                          WHERE BINARY_CHECKSUM([UserName]) = BINARY_CHECKSUM(@username)
                          AND BINARY_CHECKSUM([Password]) = BINARY_CHECKSUM(@password)";
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                connection.Open();

                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    Moderator moderator = new Moderator
                    {
                        UserName = reader.GetString("UserName"),
                        FullName = reader.GetString("FullName"),
                        Password = reader.GetString("Password")
                    };
                    
                    return moderator;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return null;
        }
    }
}
