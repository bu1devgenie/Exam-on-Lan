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
    public class SubmissionDBContext : DBContext
    {
        public Dictionary<Submission, Mark> viewListResult(int id)
        {
            string view = @$"SELECT Submission.Id,Submission.SubmitDate,Submission.Duration,Test.Code,Submission.SubmitDate,Mark.Mark,
Test.Code,Test.CreateDate,Test.Duration,Test.EndTime,Test.Id,Test.Review,Test.StartTime,Test.SubjectCode,Test.TestDate,Subject.SubjectName,
Student.Id,Student.ClassCode,Student.Dob,Student.FullName,Student.Password,Student.StudentCode,Class.ClassName,Mark.Note
FROM Submission JOIN Student ON Submission.StudentId = Student.Id JOIN Test
ON Test.Id = Submission.TestId JOIN Mark on (Mark.StudentId = Student.Id and Mark.TestId = Test.Id) 
JOIN Subject ON Subject.SubjectCode = Test.SubjectCode JOIN Class ON Class.ClassCode = Student.ClassCode  WHERE Student.Id = {id}";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(view, connection);
                Dictionary<Submission, Mark> listView = new Dictionary<Submission, Mark>();
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ClassDBContext cDb = new ClassDBContext();
                        Submission s = new Submission();
                        //s.Duration = reader.GetTimeSpan(2);
                        s.SubmitDate = reader.GetDateTime(4);
                        s.Id = reader.GetInt32(0);
                        s.Test = new Test()
                        {
                            Code = reader.GetString(6),
                            CreateDate = reader.GetDateTime(7),
                            Duration = reader.GetTimeSpan(8),
                            Id = reader.GetInt32(10),
                            IsReview = reader.GetBoolean(11),
                            StartTime = reader.GetTimeSpan(12),
                            Subject = new Subject()
                            {
                                SubjectCode = reader.GetString(13),
                                SubjectName = reader.GetString(15)
                            },

                            TestDate = reader.GetDateTime(14)

                        };
                        Mark m = new Mark()
                        {
                            Grade = reader.GetFloat(5),
                            Student = new Student
                            {
                                Id = reader.GetInt32(16),
                                Class = new Class()
                                {
                                    ClassCode = reader.GetString(17),
                                    ClassName = reader.GetString(22)
                                },
                                DateOfBirth = reader.GetDateTime(18),
                                FullName = reader.GetString(19),
                                Password = reader.GetString(20),
                                StudentCode = reader.GetString(21)

                            },
                            Test = s.Test,
                        };
                        listView.Add(s, m);
                    }
                    return listView;
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

        public bool CheckIsTested(int testId)
        {
            bool result = false;
            string sql_select_submission = @"SELECT [Id]
                                          ,[TestId]
                                          ,[StudentId]
                                          ,[SubmitDate]
                                          ,[Duration]
                                      FROM [Submission]
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


        public Submission GetSubmission(int submitID)
        {
            string sql_select_test = @"SELECT s.[Id]
                                              ,[TestId]
                                              ,[StudentId]
                                              ,[SubmitDate]
	                                          , t.[Code]
	                                          , t.TestDate
	                                          , t.Review
	                                          , stu.StudentCode
	                                          , stu.FullName
	                                          , stu.ClassCode
	                                          , sj.SubjectCode
	                                          , sj.SubjectName
                                          FROM [Submission] s 
                                          JOIN [TEST] t ON s.TestId = t.Id
                                          JOIN [Student] stu ON s.StudentId = stu.Id
                                          JOIN [Subject] sj ON t.SubjectCode = sj.SubjectCode
                                          WHERE s.[Id] = @submissionID";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_test, connection);
                command.Parameters.AddWithValue("@submissionID", submitID);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Submission()
                    {
                        Id = reader.GetInt32("Id"),
                        SubmitDate = reader.GetDateTime("SubmitDate"),
                        Test = new Test
                        {
                            Id = reader.GetInt32("TestId"),
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
                            Id = reader.GetInt32("StudentId"),
                            StudentCode = reader.GetString("StudentCode"),
                            FullName = reader.GetString("FullName"),
                            Class = new Class
                            {
                                ClassCode = reader.GetString("ClassCode")
                            }
                        }
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

        public Submission GetSubmission(int testId, int studentId)
        {
            string sql_select_test = @$"SELECT s.[Id]
                                              ,[TestId]
                                              ,[StudentId]
                                              ,[SubmitDate]
	                                          , t.[Code]
	                                          , t.TestDate
	                                          , t.Review
	                                          , stu.StudentCode
	                                          , stu.FullName
	                                          , stu.ClassCode
	                                          , sj.SubjectCode
	                                          , sj.SubjectName
                                          FROM [Submission] s 
                                          JOIN [TEST] t ON s.TestId = t.Id
                                          JOIN [Student] stu ON s.StudentId = stu.Id
                                          JOIN [Subject] sj ON t.SubjectCode = sj.SubjectCode
                                          WHERE [TestId] = {testId} AND [StudentId] = {studentId} ";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_test, connection);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Submission()
                    {
                        Id = reader.GetInt32("Id"),
                        SubmitDate = reader.GetDateTime("SubmitDate"),
                        Test = new Test
                        {
                            Id = reader.GetInt32("TestId"),
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
                            Id = reader.GetInt32("StudentId"),
                            StudentCode = reader.GetString("StudentCode"),
                            FullName = reader.GetString("FullName"),
                            Class = new Class
                            {
                                ClassCode = reader.GetString("ClassCode")
                            }
                        }
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

        public List<SubmissionQA> getSubByTest(int testId, int stuID)
        {
            string sql_get = @$"SELECT Question.Id,Question.Content,Answer.Id,Answer.Content,Answer.isCorrect FROM Submission_QA JOIN Answer ON Submission_QA.AswerId = Answer.Id  JOIN Submission ON Submission_QA.SubmissionId = Submission.Id JOIN Question ON Question.Id = Submission_QA.QuestionId JOIN Student ON Student.Id = Submission.StudentId WHERE Submission.TestId = {testId} AND Student.Id = {stuID}";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_get, connection);
                List<SubmissionQA> listView = new List<SubmissionQA>();
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ClassDBContext cDb = new ClassDBContext();
                        Submission s = new Submission()
                        {
                            Student = new Student
                            {
                                Id = stuID
                            },
                            Test = new Test
                            {
                                Id = testId
                            }
                        };
                        //s.Duration = reader.GetTimeSpan(2);

                        Question q = new Question()
                        {
                            Id = reader.GetInt32(0),
                            Content = reader.GetString(1)
                        };
                        Answer a = new Answer()
                        {
                            Id = reader.GetInt32(2),
                            Content = reader.GetString(3),
                            IsCorrect = reader.GetBoolean(4)
                        };
                        SubmissionQA sQA = new SubmissionQA()
                        {
                            Answer = a,
                            Question = q,
                            Submission = s,
                        };
                    }
                }
                return listView;
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

        public List<Submission> GetManageSubmissions(string testCode, string classCode, string stuCode)
        {
            List<Submission> submissions = new();
            try
            {
                string sql = @"SELECT s.[Id], t.[Code] AS TestCode,
		                            stu.ClassCode, 
		                            sj.SubjectCode, 
		                            stu.StudentCode, 
		                            stu.FullName,
		                            [SubmitDate]
                            FROM [Submission] s JOIN [TEST] t ON s.TestId = t.Id
                                                JOIN [Student] stu ON s.StudentId = stu.Id
                                                JOIN [Subject] sj ON t.SubjectCode = sj.SubjectCode";
                connection = new SqlConnection(GetConnectionString());
                if (!testCode.Equals(""))
                {
                    sql += "\nWHERE t.[Code] = @testcode";
                }
                if (!classCode.Equals(""))
                {
                    sql += " AND stu.[ClassCode] = @classcode";
                }
                if (!stuCode.Equals(""))
                {
                    sql += " AND stu.[StudentCode] = @stucode";
                }
                sql += " ORDER BY s.[Id] asc";
                command = new SqlCommand(sql, connection);
                if (!testCode.Equals(""))
                {
                    command.Parameters.AddWithValue("@testcode", testCode);
                }
                if (!classCode.Equals(""))
                {
                    command.Parameters.AddWithValue("@classcode", classCode);
                }
                if (!stuCode.Equals(""))
                {
                    command.Parameters.AddWithValue("@stucode", stuCode);
                }
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Submission submission = new()
                        {
                            Id = reader.GetInt32("Id"),
                            Test = new()
                            {
                                Code = reader.GetString("TestCode"),
                            },
                            Student = new()
                            {
                                StudentCode = reader.GetString("StudentCode"),
                                FullName = reader.GetString("FullName"),
                                Class = new()
                                {
                                    ClassCode = reader.GetString("ClassCode"),
                                },
                            },
                            SubmitDate = reader.GetDateTime("SubmitDate"),
                        };
                        submissions.Add(submission);
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
            return submissions;
        }

    }
}
