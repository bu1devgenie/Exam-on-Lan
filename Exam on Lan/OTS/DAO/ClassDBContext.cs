using Microsoft.Data.SqlClient;
using OTS.DAO;
using OTS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.DAO
{
    internal class ClassDBContext : DBContext
    {
        public Class GetClass(string classCode)
        {
            string sql_select_class = @"SELECT [ClassCode]
                                      ,[ClassName]
                                  FROM [Class]
                                  WHERE ClassCode=@classCode";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_class, connection);
                command.Parameters.AddWithValue("classCode", classCode);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Class()
                    {
                        ClassCode = reader.GetString("ClassCode"),
                        ClassName = reader.GetString("ClassName"),
                    };
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
        public List<Class> GetClassByTest(int testId)
        {
            List<Class> classes = new List<Class>();
            string sql_select_class_byTest = @"SELECT [TestId]
                                  ,Test_Class.ClassCode,Class.ClassName
                              FROM [Test_Class] INNER JOIN Class ON Test_Class.ClassCode = Class.ClassCode
                              WHERE Test_Class.TestId=@testId";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_class_byTest, connection);
                command.Parameters.AddWithValue("@testId", testId);
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    classes.Add(new Class()
                    {
                        ClassCode = reader.GetString("ClassCode"),
                        ClassName = reader.GetString("ClassName")
                    });
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

            return classes;
        }
        public int DeleteClass(List<Class> Classes)
        {
            int rowAffects = 0;
            string parameters = "0";
            List<string> listParam = new List<string>();

            for (int i = 0; i < Classes.Count; i++)
            {
                listParam.Add("@ClassID" + i);
            }
            if (Classes.Count > 0)
                parameters = String.Join(", ", listParam);

            string sql_delete_classes = @$"DELETE FROM [Class]
                                        WHERE ClassCode IN ({parameters})";

            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_delete_classes, connection);
                for (int i = 0; i < Classes.Count; i++)
                {
                    command.Parameters.AddWithValue(listParam[i], Classes[i].ClassCode);
                }
                connection.Open();
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return rowAffects;
        }
        public int UpdateClass(Class targetClass)
        {
            int rowAffects = 0;
            string sql_update_class = @"UPDATE [Class]
                                       SET [ClassName] = @name
                                     WHERE ClassCode = @id";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_update_class, connection);
                command.Parameters.AddWithValue("@name", targetClass.ClassName);
                command.Parameters.AddWithValue("@id", targetClass.ClassCode);

                connection.Open();
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return rowAffects;
        }
        public bool IsClassExist(string classCode)
        {
            bool isExist = false;

            string sql_select_class = @"SELECT [ClassCode]
                                      ,[ClassName]
                                  FROM [Class] WHERE ClassCode = @ClassCode";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_class, connection);
                command.Parameters.AddWithValue("@ClassCode", classCode);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    isExist = true;
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

            return isExist;
        }

        public List<Class> getClasses(string querySearch, string searchOption)
        {
            List<Class> classes = new List<Class>();
            string whereQuery = "";
            switch (searchOption)
            {
                case "name":
                    whereQuery = " [ClassName] Like '%' + @name + '%'";
                    break;
                case "code":
                    whereQuery = " [ClassCode] Like '%' +  @code + '%'";
                    break;
                default: whereQuery = " (1=1) "; break;
            }

            string sql_select_class = @$"SELECT [ClassCode]
                                      ,[ClassName]
                                  FROM [Class]
                                  WHERE {whereQuery}";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_class, connection);
                switch (searchOption)
                {
                    case "name":
                        command.Parameters.AddWithValue("@name", querySearch);
                        break;
                    case "code":
                        command.Parameters.AddWithValue("@code", querySearch);
                        break;
                }

                connection.Open();

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Class cls = new Class()
                    {
                        ClassName = reader.GetString("ClassName"),
                        ClassCode = reader.GetString("ClassCode"),
                    };
                    classes.Add(cls);

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

            return classes;
        }

        public int AddClass(Class newClass)
        {
            int rowAffects = 0;
            string sql_insert_class = @"INSERT INTO [Class]
                                           ([ClassCode] ,[ClassName])
                                     VALUES (@classCode, @className)";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(@sql_insert_class, connection);
                command.Parameters.AddWithValue("@className", newClass.ClassName);
                command.Parameters.AddWithValue("@classCode", newClass.ClassCode);

                connection.Open();
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return rowAffects;
        }

        public Class getClassbyId(string code)
        {
            List<Class> list = getClasses("", "");
            foreach (var item in list)
            {
                if(item.ClassCode.Equals(code))
                {
                    return item;
                }
            }
            return null;
        }
        
        public List<Class> GetClasses()
        {
            List<Class> classes = new List<Class>();
            string sql = @"SELECT [ClassCode]
                              ,[ClassName]
                          FROM [Class]";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql, connection);

                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        classes.Add(new Class
                        {
                            ClassCode = reader.GetString(0),
                            ClassName = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return classes;
        }
    }

}
