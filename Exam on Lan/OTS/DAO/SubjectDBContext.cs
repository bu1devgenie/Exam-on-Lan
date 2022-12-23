using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OTS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTS.DAO
{
    public class SubjectDBContext : DBContext
    {// Insert for Insert button
        public int InsertSubject(String subjectCode, String subjectName)
        {
            int rowAffects = 0;
            string sql_insert_subject = @"INSERT INTO Subject (SubjectCode, SubjectName)
                                        VALUES (@subjectCode,@subjectName);";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(@sql_insert_subject, connection);
                command.Parameters.AddWithValue("@subjectCode", subjectCode);
                command.Parameters.AddWithValue("@subjectName", subjectName);
                connection.Open();
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Warnning",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

            return rowAffects;
        }
        //--------------------------------------------------------
       
        //--------------------------------------------------------
        public int UpdateSubject(String option, String oldsubjectCode, String oldsubjectName, String newsubjectCode, String newsubjectName)
        {
            int rowAffects = 0;
            string sql_update_subject = "";
            if (option.Equals("UpdateCode"))
            {
                sql_update_subject = @"UPDATE Subject
                                       SET SubjectCode = @newCode
                                       WHERE SubjectCode = @oldCode;";
            }
            if (option.Equals("UpdateName"))
            {
                sql_update_subject = @"UPDATE Subject
                                       SET  SubjectName = @newName
                                       WHERE SubjectCode = @oldCode;";
            }
            if (option.Equals("UpdateCodeAndName"))
            {
                sql_update_subject = @"UPDATE Subject
                                       SET  SubjectCode = @newCode, SubjectName = @newName
                                       WHERE SubjectCode = @oldsubjectCode;";
            }


            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_update_subject, connection);


                if (option.Equals("UpdateCode"))
                {
                    command.Parameters.AddWithValue("@newCode", newsubjectCode);
                    command.Parameters.AddWithValue("@oldCode", oldsubjectCode);

                }
                else if (option.Equals("UpdateName"))
                {
                    command.Parameters.AddWithValue("@newName", newsubjectName);
                    command.Parameters.AddWithValue("@oldCode", oldsubjectCode);
                }
                else if (option.Equals("UpdateCodeAndName"))
                {
                    command.Parameters.AddWithValue("@newCode", newsubjectCode);
                    command.Parameters.AddWithValue("@newName", newsubjectName);
                    command.Parameters.AddWithValue("@oldCode", oldsubjectCode);
                }



                connection.Open();
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Warnning",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
            return rowAffects;
        }

        //public int UpdateSubject(String oldsubjectCode, String oldsubjectName, String newsubjectCode, String newsubjectName)
        //{
        //    int rowAffects = 0;
        //    string sql_update_subject = "";
        //    if (option.Equals("UpdateCode"))
        //    {
        //        sql_update_subject = @"UPDATE Subject
        //                               SET SubjectCode = @newCode
        //                               WHERE SubjectCode = @oldCode;";
        //    }
        //    else if (option.Equals("UpdateName"))
        //    {
        //        sql_update_subject = @"UPDATE Subject
        //                               SET  SubjectName = @newName
        //                               WHERE SubjectCode = @oldCode;";
        //    }
        //    else if (option.Equals("UpdateCodeAndName"))
        //    {
        //        sql_update_subject = @"UPDATE Subject
        //                               SET  SubjectCode = @newCode, SubjectName = @newName
        //                               WHERE SubjectCode = @oldsubjectCode;";
        //    }


        //    try
        //    {
        //        connection = new SqlConnection(GetConnectionString());
        //        command = new SqlCommand(sql_update_subject, connection);


        //        if (option.Equals("UpdateCode"))
        //        {
        //            command.Parameters.AddWithValue("@newCode", newsubjectCode);
        //            command.Parameters.AddWithValue("@oldCode", oldsubjectCode);

        //        }
        //        else if (option.Equals("UpdateName"))
        //        {
        //            command.Parameters.AddWithValue("@newName", newsubjectName);
        //            command.Parameters.AddWithValue("@oldCode", oldsubjectCode);
        //        }
        //        else if (option.Equals("UpdateCodeAndName"))
        //        {
        //            command.Parameters.AddWithValue("@newCode", newsubjectCode);
        //            command.Parameters.AddWithValue("@newName", newsubjectName);
        //            command.Parameters.AddWithValue("@oldCode", oldsubjectCode);
        //        }



        //        connection.Open();
        //        rowAffects = command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString(), "Warnning",
        //                MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return rowAffects;
        //}
        //--------------------------------------------------------
        public int DeleteSubject(String oldsubjectCode)
        {
            int rowAffects = 0;
            string sql_update_subject = "";

            sql_update_subject = @"DELETE FROM Subject  WHERE SubjectCode = @oldCode ;";

            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_update_subject, connection);
                command.Parameters.AddWithValue("@oldCode", oldsubjectCode);

                connection.Open();
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Warnning",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
            return rowAffects;
        }

        public List<Models.Subject> FindSubject(String option, String subjectCode, String subjectName)
        {
            int rowAffects = 0;
            string sql_view_subject = "";
            List<Models.Subject> sub = new List<Models.Subject>();
            if (option.Equals("FindBySubjectCode"))
            {
                sql_view_subject = @"Select [SubjectCode],[SubjectName]
                                            from Subject
                                        Where SubjectCode=@subjectCode;";
            }
            else if (option == "FindBySubjectName")
            {
                sql_view_subject = @"Select [SubjectCode],[SubjectName]
                                            from Subject
                                       Where SubjectName like '%' + @SubjectName + '%'";
            }
            else if (option == "FindBySubjectCodeAndName")
            {
                sql_view_subject = @"Select [SubjectCode],[SubjectName]
                                        from Subject
                                        Where SubjectCode=@subjectCode And SubjectName like '%' + @SubjectName + '%' ";
            }
            else if (option == "getAll")
            {
                sql_view_subject = @"Select [SubjectCode],[SubjectName]
                                        from Subject";
            }
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(@sql_view_subject, connection);


                if (option == "FindBySubjectCode")
                {
                    command.Parameters.AddWithValue("@subjectCode", subjectCode);
                }
                else if (option == "FindBySubjectName")
                {
                    command.Parameters.AddWithValue("@subjectName", subjectName);
                }
                else if (option == "FindBySubjectCodeAndName")
                {
                    command.Parameters.AddWithValue("@subjectCode", subjectCode);
                    command.Parameters.AddWithValue("@subjectName", subjectName);
                }
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            Models.Subject subject = new Models.Subject();
                            subject.SubjectCode = reader.GetString("SubjectCode");
                            subject.SubjectName = reader.GetString("SubjectName");
                            sub.Add(subject);
                        }

                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                connection.Open();
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Warnning",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
            return sub;
        }
        //public int FindSubject(String option, String subjectCode, String subjectName, DataGridView gdvUpdateSubject)
        //{
        //    int rowAffects = 0;
        //    string sql_view_subject = "";
        //    List<Models.Subject> sub = new List<Models.Subject>();
        //    if (option.Equals("FindBySubjectCode"))
        //    {
        //        sql_view_subject = @"Select [SubjectCode]
        //                                from Subject
        //                                Where SubjectCode=@subjectCode;";
        //    }
        //    else if (option == "FindBySubjectName")
        //    {
        //        sql_view_subject = @"Select [SubjectName]
        //                                from Subject
        //                               Where SubjectName like '%' + @SubjectName + '%'";
        //    }
        //    else if (option == "FindBySubjectCodeAndName")
        //    {
        //        sql_view_subject = @"Select [SubjectCode],[SubjectName]
        //                                from Subject
        //                                Where SubjectCode=@subjectCode And SubjectName like '%' + @SubjectName + '%' ";
        //    }
        //    else if (option == "getAll")
        //    {
        //        sql_view_subject = @"Select [SubjectCode],[SubjectName]
        //                                from Subject";
        //    }
        //    try
        //    {
        //        connection = new SqlConnection(GetConnectionString());
        //        command = new SqlCommand(@sql_view_subject, connection);
        public List<Subject> subjects()
        {
            List<Subject> subjects = new List<Subject>();
            string getSub = "SELECT [SubjectCode], [SubjectName] FROM [Subject]";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(getSub, connection);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Subject subject = new Subject()
                        {
                            SubjectCode = reader.GetString("SubjectCode"),
                            SubjectName = reader.GetString("SubjectName")
                        };
                        //subject.SubjectCode = reader.GetString("SubjectCode");
                        //subject.SubjectName = reader.GetString("SubjectName");
                        subjects.Add(subject);
                    }
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
            return subjects;
        }


        //        if (option == "FindBySubjectCode")
        //        {
        //            command.Parameters.AddWithValue("@subjectCode", subjectCode);
        //        }
        //        else if (option == "FindBySubjectName")
        //        {
        //            command.Parameters.AddWithValue("@subjectName", subjectName);
        //        }
        //        else if (option == "FindBySubjectCodeAndName")
        //        {
        //            command.Parameters.AddWithValue("@subjectCode", subjectCode);
        //            command.Parameters.AddWithValue("@subjectName", subjectName);
        //        }
        //        try
        //        {
        //            connection.Open();
        //            reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        //            if (reader.HasRows == true)
        //            {
        //                while (reader.Read())
        //                {
        //                    Models.Subject subject = new Models.Subject();
        //                    subject.SubjectCode = reader.GetString("SubjectCode");
        //                    subject.SubjectName = reader.GetString("SubjectName");
        //                    sub.Add(subject);
        //                }

        //            }
        //            connection.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Error");
        //        }
        //        connection.Open();
        //        rowAffects = command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString(), "Warnning",
        //                MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return sub;
        //}
        //--------------------------------------------------------
        public Subject GetSubject(String option, String subjectCode, String subjectName)
        {
            int rowAffects = 0;
            string sql_view_subject = "";
            Subject sub = new Subject();
            if (option.Equals("FindBySubjectCode"))
            {
                sql_view_subject = @"Select [SubjectCode],[SubjectName]
                                        from Subject
                                        Where SubjectCode=@subjectCode;";
            }
            else if (option == "FindBySubjectName")
            {
                sql_view_subject = @"Select [SubjectCode],[SubjectName]
                                        from Subject
                                       Where SubjectName like '%' + @SubjectName + '%'";
            }
            else if (option == "FindBySubjectCodeAndName")
            {
                sql_view_subject = @"Select [SubjectCode],[SubjectName]
                                        from Subject
                                        Where SubjectCode=@subjectCode And SubjectName like '%' + @SubjectName + '%' ";
            }

            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(@sql_view_subject, connection);


                if (option == "FindBySubjectCode")
                {
                    command.Parameters.AddWithValue("@subjectCode", subjectCode);
                }
                else if (option == "FindBySubjectName")
                {
                    command.Parameters.AddWithValue("@subjectName", subjectName);
                }
                else if (option == "FindBySubjectCodeAndName")
                {
                    command.Parameters.AddWithValue("@subjectCode", subjectCode);
                    command.Parameters.AddWithValue("@subjectName", subjectName);
                }

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            Models.Subject subject = new Models.Subject();
                            subject.SubjectCode = reader.GetString("SubjectCode");
                            subject.SubjectName = reader.GetString("SubjectName");
                            sub = subject;
                        }

                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                connection.Open();
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Warnning",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return sub;
        }

        //=======
        //            List<Models.Subject> subjects = new List<Models.Subject>();
        //            string sql = @"SELECT [SubjectCode]
        //                                  ,[SubjectName]
        //                              FROM [Subject]";
        //            connection = new SqlConnection(GetConnectionString());
        //            command = new SqlCommand(sql, connection);
        //>>>>>>> main

        //<<<<<<< HEAD
        //                        while (reader.Read())
        //                        {
        //                            Models.Subject subject = new Models.Subject();
        //                            subject.SubjectCode = reader.GetString("SubjectCode");
        //                            subject.SubjectName = reader.GetString("SubjectName");
        //                            sub = subject;
        //                        }

        //                    }
        //                    connection.Close();
        //=======
        //                        Models.Subject subject = new Models.Subject();
        //                        subject.SubjectCode = reader.GetString("SubjectCode");
        //                        subject.SubjectName = reader.GetString("SubjectName");
        //                        subjects.Add(subject);
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {


        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //            return subjects;
        //        }

        public List<Subject> GetSubjects()
        {
            List<Subject> subjects = new List<Subject>();
            string sql = @"SELECT [SubjectCode]
                                  ,[SubjectName]
                              FROM [Subject]";
            connection = new SqlConnection(GetConnectionString());
            command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        subjects.Add(new Subject
                        {
                            SubjectCode = reader.GetString("SubjectCode"),
                            SubjectName = reader.GetString("SubjectName")
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Warnning",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return subjects;
        }
        //>>>>>>> main
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message, "Error");
        //                }
        //                connection.Open();
        //                rowAffects = command.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //<<<<<<< HEAD
        //                MessageBox.Show(ex.Message.ToString(), "Warnning",
        //                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        //=======
        //                throw new Exception(ex.Message);
        //>>>>>>> main

        //<<<<<<< HEAD
        //=======

        public Subject getSubbyId(string code)
        {
            List<Subject> subs = subjects();
            foreach (var s in subs)
            {
                if (s.SubjectCode.Equals(code))
                {
                    return s;
                }
            }
            return null;
        }

        public Subject GetSubject(string code)
        {
            string sql = @"SELECT [SubjectCode]
                                  ,[SubjectName]
                              FROM [Subject]
                              WHERE [SubjectCode] = @code";

            connection = new SqlConnection(GetConnectionString());
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@code", code);
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Subject subject = new Subject()
                    {
                        SubjectCode = reader.GetString("SubjectCode"),
                        SubjectName = reader.GetString("SubjectName")
                    };
                    return subject;
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

        public Subject GetSubjectBySubmission(int id)
        {
            string sql = @"SELECT s.[SubjectCode],
                                s.[SubjectName]
                          FROM [Subject] s INNER JOIN [Test] t ON s.[SubjectCode] = t.[SubjectCode]
					                        INNER JOIN [Submission] sub ON sub.[TestId] = t.[Id]
                          WHERE sub.[Id] = @id";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Subject subject = new()
                    {
                        SubjectCode = reader.GetString("SubjectCode"),
                        SubjectName = reader.GetString("SubjectName"),
                    };
                    return subject;
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

