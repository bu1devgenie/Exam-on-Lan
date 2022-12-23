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
    internal class SubmissionQADBContext : DBContext
    {
        public List<SubmissionQA> GetListSubmissionQAs(int submissionID)
        {
            List<SubmissionQA> qAs = new List<SubmissionQA>();
            string sql = @"SELECT [SubmissionId]
                                  ,sqa.[QuestionId]
                                  ,[AswerId]
	                              ,q.[Content] as Question
	                              ,q.[Type]
	                              ,a.[Content] as Answer
	                              ,a.[isCorrect]
                              FROM [Submission_QA] sqa
                              JOIN [Question] q ON sqa.QuestionId = q.Id
                              JOIN [Answer] a ON sqa.AswerId = a.Id
                              WHERE [SubmissionId] = @submissionId";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@submissionId", submissionID);
                connection.Open();
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        qAs.Add(new SubmissionQA
                        {
                            Submission = new Submission
                            {
                                Id = submissionID
                            },
                            Question = new Question
                            {
                                Id = reader.GetInt32("QuestionID"),
                                Content = reader.GetString("Question"),
                                Type = new Models.Type
                                {
                                    Id = reader.GetInt16("Type")
                                }
                            },
                            Answer = new Answer
                            {
                                Id = reader.GetInt32("AswerID"),
                                Content = reader.GetString("Answer"),
                                IsCorrect = reader.GetBoolean("isCorrect")
                            }
                        });
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
            return qAs;
        }

        public int InsertSubmit(Submission submission)
        {
            int result = 0;
            string sql_insert = @"INSERT INTO [dbo].[Submission]
           ([TestId]
           ,[StudentId]
           ,[SubmitDate]
           ,[Duration])
     VALUES
           (@TestId
           ,@StudentId
           ,getDate()
           ,@Duration)";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_insert, connection);
                command.Parameters.AddWithValue("@TestId", submission.Test.Id);
                command.Parameters.AddWithValue("@StudentId", submission.Student.Id);
                command.Parameters.AddWithValue("@Duration", submission.Duration);
                connection.Open();
                result = command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public int InsertSubmitQA(SubmissionQA s)
        {
            int result = 0;
            string sql_insert = @"INSERT INTO [dbo].[Submission_QA]
           ([SubmissionId]
           ,[QuestionId]
           ,[AswerId])
     VALUES
           (@SubId
           ,@QuesID
           ,@AnsId)";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_insert, connection);
                command.Parameters.AddWithValue("@SubId", s.Submission.Id);
                command.Parameters.AddWithValue("@QuesID", s.Question.Id);
                command.Parameters.AddWithValue("@AnsId", s.Answer.Id);
                connection.Open();
                result = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}
