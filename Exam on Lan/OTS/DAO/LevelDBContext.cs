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
    public class LevelDBContext : DBContext
    {
        public List<Level> GetLevels()
        {
            List<Level> levels = new List<Level>();
            string getLevels = "SELECT [Id], [Name] FROM [Level]";

            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(getLevels, connection);
                connection.Open();

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Level l = new Level()
                    {
                        Id = reader.GetInt16("Id"),
                        Name = reader.GetString("Name")
                    };
                    levels.Add(l);
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
            return levels;
        }

        public Level GetLevelById(short id)
        {
            List<Level> levels = GetLevels();
            foreach (var t in levels)
            {
                if (t.Id == id)
                {
                    return t;
                }
            }

            return null;
        }

        public Level GetLevel(int id)
        {
            string sql = @"SELECT [Id]
                                  ,[Name]
                              FROM [Level]
                              WHERE [Id] = @id";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Level level = new()
                    {
                        Id = (short)reader["Id"],
                        Name = (string)reader["Name"],
                    };
                    return level;
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
