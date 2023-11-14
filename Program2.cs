//using Microsoft.Data.Sqlite;
//using SQLitePCL;

//class Program
//{
//    public static void Main()
//    {
//        var message = new Message { Content = "tava lá neh.. lavando tudo dnv", RemetentID = 123, DestinataryId = 456 };
//        var message2 = new Message { Content = "tava lá neh.. o.O", RemetentID = 123, DestinataryId = 456 };
//        var message3 = new Message { Content = "-- Terceira mensagem pra quebrar o fluxx", RemetentID = 123, DestinataryId = 456 };
//        BancoDados.SaveMessage(message);
//        BancoDados.SaveMessage(message2);
//        BancoDados.SaveMessage(message3);
//        var messages = BancoDados.GetMessages();

//        foreach (var mes in messages)
//        {
//            Console.WriteLine(mes.Content);
//        }
//    }
//}

//public static class BancoDados
//{

//    private static SqliteConnection Open()
//    {
//        Batteries.Init();

//        SqliteConnection connection = new SqliteConnection("Data Source=messages.db;");

//        connection.Open();

//        string createTableQuery = "CREATE TABLE IF NOT EXISTS Messages (Id INTEGER PRIMARY KEY AUTOINCREMENT, Content TEXT, RemetentId INTEGER, DestinataryId INTEGER);";
//        using (SqliteCommand createTableCommand = new SqliteCommand(createTableQuery, connection))
//        {
//            // Executa o comando SQLite usando a junção da query com a conexão criada
//            createTableCommand.ExecuteNonQuery();
//        }

//        return connection;
//    }

//    public static void SaveMessage(Message message)
//    {
//        using (SqliteConnection connection = Open())
//        {
//            string insertQuery = "INSERT INTO Messages (Content, RemetentID, DestinataryId) VALUES (@content, @remetentID, @destinataryID);";
//            using (SqliteCommand insertCommand = new SqliteCommand(insertQuery, connection))
//            {
//                insertCommand.Parameters.AddWithValue("@content", message.Content);
//                insertCommand.Parameters.AddWithValue("@remetentID", message.RemetentID);
//                insertCommand.Parameters.AddWithValue("@destinataryID", message.DestinataryId);
//                insertCommand.ExecuteNonQuery();
//            }
//        }
//    }

//    public static List<Message> GetMessages()
//    {
//        List<Message> messages = new List<Message>();
//        using (SqliteConnection connection = Open())
//        {
//            string selectQuery = "SELECT * FROM Messages;";
//            using (SqliteCommand selectCommand = new SqliteCommand(selectQuery, connection))
//            using (SqliteDataReader reader = selectCommand.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    messages.Add(new Message
//                    {
//                        Content = reader["Content"].ToString(),
//                        RemetentID = Convert.ToInt32(reader["RemetentID"]),
//                        DestinataryId = Convert.ToInt32(reader["DestinataryId"])
//                    });
//                }
//            }
//        }
//        return messages;
//    }

//    public static void Close(SqliteConnection connection)
//    {
//        if (connection != null && connection.State == System.Data.ConnectionState.Open)
//        {
//            connection.Close();
//        }
//    }

//}

//public class Message
//{
//    public string? Content { get; set; }
//    public int RemetentID { get; set; }
//    public int DestinataryId { get; set; }
//}