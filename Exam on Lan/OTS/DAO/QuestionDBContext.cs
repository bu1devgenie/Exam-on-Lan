using Microsoft.Data.SqlClient;
using OTS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = OTS.Models.Type;

namespace OTS.DAO
{
    public class QuestionDBContext : DBContext
    {
        //public string Content { get; private set; }

        public List<Question> getQues(string searchKey, string option)
        {
            string whereQuery = "";
            switch (option)
            {
                case "content":
                    whereQuery = " [Content] Like '%' + @content + '%'";
                    break;
                case "code":
                    whereQuery = " [SubjectCode] Like '%' +  @code + '%'";
                    break;
                default: whereQuery = " (1=1) "; break;
            }
            List<Question> ListQues = new List<Question>();
            List<Models.Type> listType = new List<Models.Type>();
            string getallQuestion = @$"SELECT Question.Id,Question.Content,Level.[Name],Question.SubjectCode
                                    ,Type.[Name],Question.Content,Level.Id,Question.[Type] 
                                    FROM Question JOIN [Type] ON Question.[Type] = Type.Id  
                                    JOIN [Level] ON Level.Id = Question.[Level] WHERE {whereQuery}";
            TypeDBContext tDB = new TypeDBContext();
            SubjectDBContext sDB = new SubjectDBContext();
            LevelDBContext lDB = new LevelDBContext();
            AnswerDBContext aDB = new AnswerDBContext();
            try
            {

                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(getallQuestion, connection);
                switch (option)
                {
                    case "content":
                        command.Parameters.AddWithValue("@content", searchKey);
                        break;
                    case "code":
                        command.Parameters.AddWithValue("@code", searchKey);
                        break;
                }
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Models.Type t = tDB.GetTypeById(reader.GetInt16(7));
                    Subject s = sDB.getSubbyId(reader.GetString(3));
                    Level l = lDB.GetLevelById(reader.GetInt16(6));
                    //List<Answer> answers = aDB.getAnswerByCID(reader.GetInt32(0));
                    Question ques = new Question()
                    {
                        Content = reader.GetString(1),
                        Type = t,
                        Subject = s,
                        Level = l,
                        Id = reader.GetInt32(0),
                        //Answers = aDB.getAnswerByCID(reader.GetInt32(0))
                    };
                    //ques.Answers = answers;
                    ListQues.Add(ques);
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
            return ListQues;
        }

        public int DeleteQuestionTest(int testId)
        {
            int rowAffects = 0;
            string sql_delete_oldQuestion = @"DELETE FROM [Question_Test]
                                              WHERE TestId=@testId";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                connection.Open();
                command = new SqlCommand(sql_delete_oldQuestion, connection);
                command.Parameters.AddWithValue("@testId", testId);
                rowAffects = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return rowAffects;
        }

        public int UpdateTestQuestion(int testId, List<int> questionIds)
        {
            int rowAffects = 0;
            //Delete Old question
            string sql_delete_oldQuestion = @"DELETE FROM [Question_Test]
                                              WHERE TestId=@testId";
            string sql_insert_newQuestion = @"INSERT INTO [Question_Test]
                                               ([QuestionId]
                                               ,[TestId])
                                         VALUES
                                               (@questionId
                                               ,@testId)";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                connection.Open();
                command = new SqlCommand(sql_delete_oldQuestion, connection);
                command.Parameters.AddWithValue("@testId", testId);
                command.ExecuteNonQuery();
                foreach (int questionId in questionIds)
                {
                    command = new SqlCommand(sql_insert_newQuestion, connection);
                    command.Parameters.AddWithValue("@questionId", questionId);
                    command.Parameters.AddWithValue("@testId", testId);
                    rowAffects += command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return rowAffects;
        }

        public Question GetQuestion(int questionId)
        {
            string sql_select_question = @"SELECT  Question.[Id]
                                      ,[Content]
                                      ,[Image]
                                      ,[Level], Level.Name AS LevelName
                                      ,Question.SubjectCode, Subject.SubjectName
                                      ,[Type], Type.Name AS TypeName
                                  FROM [Question] JOIN Type ON Question.Type=Type.Id
		                                JOIN Level ON Question.Level=Level.Id
		                                JOIN Subject ON Question.SubjectCode=Subject.SubjectCode
                                  WHERE Question.Id = @questionId";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_question, connection);
                command.Parameters.AddWithValue("@questionId", questionId);

                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Question()
                    {
                        Id = questionId,
                        Content = reader.GetString("Content"),
                        Level = new Level()
                        {
                            Id = reader.GetInt16("Level"),
                            Name = reader.GetString("LevelName"),
                        },
                        Type = new Type()
                        {
                            Name = reader.GetString("TypeName"),
                            Id = reader.GetInt16("Type"),
                        },
                        Subject = new Subject()
                        {
                            SubjectCode = reader.GetString("SubjectCode"),
                            SubjectName = reader.GetString("SubjectName")
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

        public Question GetRandomQuestionWithLevel(int levelId, string subjectCode, int type, List<int> exceptIds)
        {
            List<string> exceptIdsParameters = new List<string>();
            for(int i = 0; i<exceptIds.Count; i++)
            {
                exceptIdsParameters.Add("@ExceptId" + i);
            }
            string select_random_question = @$"SELECT TOP 1 Question.[Id]
                                              ,[Content]
                                              ,[Image]
                                              ,[Level], Level.Name AS LevelName
                                              ,Question.SubjectCode, Subject.SubjectName
                                              ,[Type], Type.Name AS TypeName
                                          FROM [Question] JOIN Type ON Question.Type=Type.Id
		                                        JOIN Level ON Question.Level=Level.Id
		                                        JOIN Subject ON Question.SubjectCode=Subject.SubjectCode
                                          WHERE Question.Level=@Level AND Question.SubjectCode=@Subject AND Question.Type=@Type
                                                    AND Question.id NOT IN ({String.Join(", ", exceptIdsParameters)})
                                          ORDER BY NEWID()";

            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(select_random_question, connection);
                command.Parameters.AddWithValue("@Level", levelId);
                command.Parameters.AddWithValue("@Subject", subjectCode);
                command.Parameters.AddWithValue("@Type", type);
                for (int i = 0; i < exceptIds.Count; i++)
                {
                    command.Parameters.AddWithValue("@ExceptId" + i, exceptIds[i]);
                }

                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Question()
                    {
                        Id = reader.GetInt32("Id"),
                        Content = reader.GetString("Content"),
                        Level = new Level()
                        {
                            Id = reader.GetInt16("Level"),
                            Name = reader.GetString("LevelName"),
                        },
                        Type = new Type()
                        {
                            Name = reader.GetString("TypeName"),
                            Id = reader.GetInt16("Type"),
                        },
                        Subject = new Subject()
                        {
                            SubjectCode = reader.GetString("SubjectCode"),
                            SubjectName = reader.GetString("SubjectName")
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return null;
        }

        public List<QuestionTest> GetQuestionByTests(int testID)
        {
            List<QuestionTest> result = new List<QuestionTest>();
            string sql_select_question = @"SELECT [QuestionId], Question.Content, Level.Name AS LevelName
                                        , Type.Name AS TypeName
                                  FROM [Question_Test] INNER JOIN Question ON Question.Id=Question_Test.QuestionId
	                                INNER JOIN Type ON Question.Type=Type.Id
	                                INNER JOIN LEVEL ON Question.Level=Level.Id
	                                WHERE TestId=@testId";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_select_question, connection);
                command.Parameters.AddWithValue("testId", testID);
                connection.Open();
                reader = command.ExecuteReader();
                Test test = null;
                while (reader.Read())
                {
                    if (test == null)
                    {
                        test = new Test()
                        {
                            Id = testID,
                        };
                    }
                    Question question = new Question()
                    {
                        Id = reader.GetInt32("QuestionId"),
                        Content = reader.GetString("Content"),
                        Level = new Level()
                        {
                            Name = reader.GetString("LevelName"),
                        },
                        Type = new Type()
                        {
                            Name = reader.GetString("TypeName"),
                        }

                    };
                    result.Add(new QuestionTest()
                    {
                        Question = question,
                        Test = test,
                    });
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }

            return result;
        }


        public Question getQues(int id)
        {
            List<Question> ListQues = new List<Question>();
            List<Models.Type> listType = new List<Models.Type>();
            string getallQuestion = @$"SELECT [Id]
      ,[Content]
      ,[Level]
      ,[SubjectCode]
      ,[Type]
  FROM [dbo].[Question] WHERE Id = {id}";
            TypeDBContext tDB = new TypeDBContext();
            SubjectDBContext sDB = new SubjectDBContext();
            LevelDBContext lDB = new LevelDBContext();
            AnswerDBContext aDB = new AnswerDBContext();
            try
            {

                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(getallQuestion, connection);
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Models.Type t = tDB.GetTypeById(reader.GetInt16(4));
                    Subject s = sDB.getSubbyId(reader.GetString(3));
                    Level l = lDB.GetLevelById(reader.GetInt16(2));
                    //List<Answer> answers = aDB.getAnswerByCID(reader.GetInt32(0));
                    if (!listType.Contains(t))
                    {
                        listType.Add(t);
                    }
                    Question ques = new Question()
                    {
                        Content = reader.GetString(1),
                        Type = t,
                        Subject = s,
                        Level = l,
                        Id = id,
                        //Answers = aDB.getAnswerByCID(reader.GetInt32(0))
                    };
                    //ques.Answers = answers;
                    return ques;
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

        public int AddQues(Question q)
        {
            int rowAffects = 0;
            string sql_inser_ques = @"INSERT INTO [dbo].[Question]
           ([Content]
           ,[Level]
           ,[SubjectCode]
           ,[Type])
            VALUES
           (@Content
           ,@Level
           ,@SubjectCode
           ,@Type);SELECT SCOPE_IDENTITY();";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_inser_ques, connection);
                command.Parameters.AddWithValue("@Content", q.Content);
                command.Parameters.AddWithValue("@Level", q.Level.Id);
                command.Parameters.AddWithValue("@SubjectCode", q.Subject.SubjectCode);
                command.Parameters.AddWithValue("@Type", q.Type.Id);
                connection.Open();
                rowAffects = Convert.ToInt32(command.ExecuteScalar());

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

        public int UpdateQues(Question targetQues)
        {
            int rowAffects = 0;
            string sql_update_class = @"UPDATE [dbo].[Question]
   SET [Content] = @Content
      ,[Level] = @Level
      ,[SubjectCode] = @Subject
      ,[Type] = @Type
 WHERE Id = @Id";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_update_class, connection);
                command.Parameters.AddWithValue("@Content", targetQues.Content);
                command.Parameters.AddWithValue("@Level", targetQues.Level.Id);
                command.Parameters.AddWithValue("@Subject", targetQues.Subject.SubjectCode);
                command.Parameters.AddWithValue("@Type", targetQues.Type.Id);
                command.Parameters.AddWithValue("@Id", targetQues.Id);
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

        public int DeleteQues(Question q)
        {
            int rowAffects = 0;
            string sql_delete_classes = @$"DELETE FROM [dbo].[Question]
      WHERE Id = @Id";

            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql_delete_classes, connection);
                command.Parameters.AddWithValue("@Id", q.Id);
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

        public List<Question> getQuesByTest(int testId)
        {
            List<Question> list = new List<Question>();
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
                    ques.Answers.Add(a);
                    list.Add(ques);
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

        public List<Question> GetRandomQuestions(int top, int type, int level, string code)
        {
            List<Question> questions = new List<Question>();
            string sql = @$"SELECT TOP {top} q.[Id]
                                          ,[Content]
                                          ,[Image]
                                          ,l.[Id] AS LevelId, l.[Name] AS LevelName
                                          ,s.[SubjectCode], s.[SubjectName]
                                          ,t.[Id] AS TypeId, t.[Name] AS TypeName
                          FROM [Question] q JOIN [Type] t ON q.[Type] = t.[Id]
		                                    JOIN [Level] l ON q.[Level] = l.[Id]
		                                    JOIN [Subject] s ON q.[SubjectCode] = s.[SubjectCode]
                          WHERE q.[Type] = @type
	                        AND q.[Level] = @level
	                        AND q.[SubjectCode] = @code
                          ORDER BY NEWID()";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@level", level);
                command.Parameters.AddWithValue("@code", code);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Question q = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Content = reader.GetString("Content"),
                        //Image = reader.GetString("image"),
                        Level = new()
                        {
                            Id = reader.GetInt16("LevelId"),
                            Name = reader.GetString("LevelName"),
                        },
                        Subject = new()
                        {
                            SubjectCode = reader.GetString("SubjectCode"),
                            SubjectName = reader.GetString("SubjectName"),
                        },
                        Type = new()
                        {
                            Id = reader.GetInt16("TypeId"),
                            Name = reader.GetString("TypeName"),
                        }
                    };
                    questions.Add(q);
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
            return questions;
        }

        public int InsertQuestion_Test(int quest, int test)
        {
            int row = 0;
            string sql = @"INSERT INTO [Question_Test]
                                       ([QuestionId]
                                       ,[TestId])
                                 VALUES
                                       (@quest
                                       ,@test)";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@quest", quest);
                command.Parameters.AddWithValue("@test", test);
                connection.Open();
                row = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return row;
        }

        public bool CheckQuest_Test(int id)
        {
            string sql = @"SELECT [QuestionId]
                                  ,[TestId]
                              FROM [Question_Test]
                             WHERE [TestId] = @id";
            try
            {
                connection = new SqlConnection(GetConnectionString());
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return true;
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
            return false;
        }
    }
}
