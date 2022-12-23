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
    public class AnswerDBContext : DBContext
    {
        public List<Answer> getAnswer()
        {
            List<Answer> list = new List<Answer>();
            QuestionDBContext qDb = new QuestionDBContext();
            string getAnsByQues = $"SELECT [Id], [Content], [QuestionId],[isCorrect] FROM [Answer]";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(getAnsByQues, connection);
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Question q = new Question();
                    q.Id = reader.GetInt32("QuestionId");
                    Answer a = new Answer()
                    {
                        Content = reader.GetString("Content"),
                        Id = reader.GetInt32("Id"),
                        IsCorrect = reader.GetBoolean("isCorrect"),
                        Question = q
                    };
                    list.Add(a);
                }
                //Question q = qDb.findQuesID(id);
                //q.Answers = list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        public List<Answer> getAnswerByQues(int quesId)
        {
            List<Answer> list = new List<Answer>();
            string getAnsByQues = @"SELECT A.Id,A.Content,A.QuestionId,A.isCorrect 
                                    FROM Answer AS A 
                                    INNER JOIN Question AS B 
                                    ON A.QuestionId = B.Id 
                                    WHERE B.Id = " + quesId;
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(getAnsByQues, connection);
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Question q = new Question();
                    q.Id = reader.GetInt32("QuestionId");
                    Answer a = new Answer()
                    {
                        Content = reader.GetString("Content"),
                        Id = reader.GetInt32("Id"),
                        Question = q,
                        IsCorrect = reader.GetBoolean("isCorrect")
                    };
                    list.Add(a);
                }
                //Question q = qDb.findQuesID(id);
                //q.Answers = list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        public int AddAns(Answer a)
        {
            int rowAffects = 0;
            string sql_inser_ans = @"INSERT INTO [dbo].[Answer] ([Content],[QuestionId],[isCorrect]) VALUES(@Content,@QuestionId,@isCorrect)";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_inser_ans, connection);
                command.Parameters.AddWithValue("@Content", a.Content);
                command.Parameters.AddWithValue("@QuestionId", a.Question.Id);
                command.Parameters.AddWithValue("@isCorrect", a.IsCorrect);
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

        public List<Answer> getByQues(Question q)
        {
            List<Question> ListQues = new List<Question>();
            List<Models.Type> listType = new List<Models.Type>();
            string getallAns = @$"SELECT [Id]
                                    ,[Content]
                                    ,[QuestionId]
                                    ,[isCorrect]
                                FROM [dbo].[Answer] Where QuestionId = {q.Id}";
            TypeDBContext tDB = new TypeDBContext();
            SubjectDBContext sDB = new SubjectDBContext();
            LevelDBContext lDB = new LevelDBContext();
            AnswerDBContext aDB = new AnswerDBContext();
            try
            {

                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(getallAns, connection);
                connection.Open();
                reader = command.ExecuteReader();
                List<Answer> l = new List<Answer>();
                while (reader.Read())
                {
                    Answer a = new Answer()
                    {
                        Id = reader.GetInt32(0),
                        Content = reader.GetString(1),
                        IsCorrect = reader.GetBoolean(3),
                        Question = q
                        //Answers = aDB.getAnswerByCID(reader.GetInt32(0))
                    };
                    //ques.Answers = answers;
                    l.Add(a);
                }
                return l;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public int UpdateAns(Answer targetAns)
        {
            int rowAffects = 0;
            string sql_update_ans = @"UPDATE [dbo].[Answer]
   SET [Content] = @Content
      ,[isCorrect] = @isCorrect
 WHERE Id = @Id";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_update_ans, connection);
                command.Parameters.AddWithValue("@Content", targetAns.Content);
                command.Parameters.AddWithValue("@isCorrect", targetAns.IsCorrect);
                command.Parameters.AddWithValue("@Id", targetAns.Id);
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


        public int DeleteAns(Answer a)
        {
            int rowAffects = 0;


            string sql_delete_classes = @$"DELETE FROM [dbo].[Answer]
      WHERE Id = @Id";

            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_delete_classes, connection);
                command.Parameters.AddWithValue("@Id", a.Id);
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

        public List<Answer> getQuesByTest(int testId)
        {
            List<Answer> list = new List<Answer>();
            string sql_test_ques = @$"SELECT Question.Id,Question.Content,Answer.isCorrect,Answer.Content
FROM Test JOIN Question_Test ON Test.Id = Question_Test.TestId JOIN Question ON Question.Id = Question_Test.QuestionId
JOIN Answer ON Answer.QuestionId = Question.Id
WHERE Test.Id = {testId} AND Answer.isCorrect = 1";
            TypeDBContext tDB = new TypeDBContext();
            SubjectDBContext sDB = new SubjectDBContext();
            LevelDBContext lDB = new LevelDBContext();
            AnswerDBContext aDB = new AnswerDBContext();
            try
            {

                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_test_ques, connection);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    //List<Answer> answers = aDB.getAnswerByCID(reader.GetInt32(0));
                    Question ques = new Question()
                    {
                        Content = reader.GetString(1),
                        Id = reader.GetInt32(0),

                    };
                    Answer a = new Answer()
                    {
                        IsCorrect = reader.GetBoolean(2),
                        Content = reader.GetString(3)
                    };
                    a.Question = ques;
                    list.Add(a);
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
            return list;
        }
        
    }


}
