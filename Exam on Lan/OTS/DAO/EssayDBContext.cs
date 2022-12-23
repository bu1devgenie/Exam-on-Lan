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
    public class EssayDBContext : DBContext
    {
        public List<Essay> GetEssays(int testId, int studentId)
        {
            List<Essay> essays = new List<Essay>();
            try
            {
                string sql_select_essay = @"SELECT e.[Id]
                                                  ,[TestId]
                                                  ,[StudentId]
                                                  ,[QuestionId]
                                                  ,[SubmitDate]
	                                              ,t.Code
	                                              ,t.TestDate
	                                              ,t.Review
	                                              ,sj.SubjectCode
	                                              ,sj.SubjectName
	                                              ,s.StudentCode
	                                              ,s.FullName
	                                              ,s.ClassCode
                                                  ,e.[Content] as EssayContent
	                                              ,q.Content as Question
	                                              ,q.[Type]
	                                              ,q.[Image]
                                              FROM [Essay] e
                                              JOIN [Test] t ON e.TestId = t.Id
                                              JOIN [Subject] sj ON t.SubjectCode = sj.SubjectCode
                                              JOIN [Student] s ON e.StudentId = s.Id
                                              JOIN [Question] q ON e.QuestionId = q.Id		
                                              WHERE TestId=@testId AND StudentId=@StudentId";
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_essay, connection);
                command.Parameters.AddWithValue("@testId", testId);
                command.Parameters.AddWithValue("@studentId", studentId);
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    essays.Add(new Essay()
                    {
                        Id = reader.GetInt32("Id"),
                        SubmitDate = reader.GetDateTime("SubmitDate"),
                        Question = new Question()
                        {
                            Id = reader.GetInt32("QuestionID"),
                            Content = reader.GetString("Question"),
                            Type = new Models.Type
                            {
                                Id = reader.GetInt16("Type")
                            }
                        },
                        Test = new Test
                        {
                            Id=reader.GetInt32("TestID"),
                            Code = reader.GetString("Code"),
                            TestDate = reader.GetDateTime("TestDate"),
                            IsReview = reader.GetBoolean("Review"),
                            Subject = new Subject
                            {
                                SubjectCode = reader.GetString("SubjectCode"),
                                SubjectName = reader.GetString("SubjectName")
                            }
                        },
                        Student = new Student
                        {
                            Id = reader.GetInt32("StudentID"),
                            StudentCode = reader.GetString("StudentCode"),
                            FullName = reader.GetString("FullName"),
                            Class = new Class
                            {
                                ClassCode = reader.GetString("ClassCode")
                            }
                        },
                        Content = reader.GetString("EssayContent")
                    });
                }
            }catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }

            return essays;
        }
        public Essay GetEssay(int essayId)
        {
            string sql_select_essayId = @"SELECT Essay.[Id]
                              ,[TestId],Test.Code
                              ,[QuestionId], Question.Content AS QuestionContent
                              ,Level.Name AS LevelName
                              ,[SubmitDate]
                              ,Essay.[Duration]
                              ,Essay.[Content]
                          FROM [Essay]  JOIN Test ON Essay.TestId=Test.Id
		                        JOIN Question ON Essay.QuestionId = Question.Id
                                JOIN Level ON Question.Level = Level.Id
                            WHERE Essay.Id = @essayId";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_essayId, connection);
                command.Parameters.AddWithValue("@essayId", essayId);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read()) {

                    return new Essay()
                    {
                        Content = reader.GetString("Content"),
                        Question = new Question()
                        {
                            Content = reader.GetString("QuestionContent"),
                            Id = reader.GetInt32("QuestionId"),
                            Level = new Level()
                            {
                                Name = reader.GetString("LevelName"),
                            }
                        },
                        Id = reader.GetInt32("Id"),
                        Duration = (TimeSpan) reader["Duration"],
                        SubmitDate = reader.GetDateTime("SubmitDate"),
                    };
                }

            }catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return null;
        }
        public bool CheckIsTested(int testId)
        {
            bool result = false;
            string sql_select_submission = @"SELECT [Id]
                                      FROM [Essay]
                                      WHERE TestId=@testId";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_submission, connection);
                command.Parameters.AddWithValue("@testId", testId);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return result;
        }

        public Essay GetEssay(int testId, int studentId)
        {
            string sql_select_essayId = @$"SELECT Essay.[Id]
                              ,[TestId],Test.Code
                              ,[QuestionId], Question.Content AS QuestionContent
                              ,Level.Name AS LevelName
                              ,[SubmitDate]
                              ,Essay.[Duration]
                              ,Essay.[Content]
                          FROM [Essay]  JOIN Test ON Essay.TestId=Test.Id
		                        JOIN Question ON Essay.QuestionId = Question.Id
                                JOIN Level ON Question.Level = Level.Id
                            WHERE Essay.TestId = {testId} AND Essay.StudentId = {studentId}";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_essayId, connection);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {

                    return new Essay()
                    {
                        Content = reader.GetString("Content"),
                        Question = new Question()
                        {
                            Content = reader.GetString("QuestionContent"),
                            Id = reader.GetInt32("QuestionId"),
                            Level = new Level()
                            {
                                Name = reader.GetString("LevelName"),
                            }
                        },
                        Id = reader.GetInt32("Id"),
                        Duration = (TimeSpan)reader["Duration"],
                        SubmitDate = reader.GetDateTime("SubmitDate"),
                    };
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return null;
        }
    }
}
