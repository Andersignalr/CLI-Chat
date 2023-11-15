// using Microsoft.Data.Sqlite => Este namespace fornece classes para trabalhar com o SQLite
// usando o provedor de dados SQLite da Microsoft.
using Microsoft.Data.Sqlite;
// using SQLitePCL => é necessário para usar o Batteries.Init() e é necessário para usar o SQLite
using SQLitePCL;


namespace Consolat
{
    public static class Database
    {
        private static SqliteConnection Open()
        {
            Batteries.Init();

            SqliteConnection connection = new SqliteConnection("Data Source=messages.db;");

            connection.Open();

            return connection;
        }

        public static void Initialize()
        {
            using (SqliteConnection connection = Open())
            {
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Messages(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Content TEXT, RemetentId TEXT, DestinataryId TEXT);";
                using (SqliteCommand createTableCommand = new SqliteCommand(createTableQuery, connection))
                {
                    createTableCommand.ExecuteNonQuery();
                }
            }
        }

        public static void SaveMessage(Message message)
        {
            using(SqliteConnection connection = Open())
            {
                string insertQuery = "INSERT INTO Messages (Content, RemetentId, DestinataryId) VALUES (@content, @remetentId, @destinataryId);";
                using(SqliteCommand insertCommand = new SqliteCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@content", message.Content);
                    insertCommand.Parameters.AddWithValue("@remetentId", message.RemetentId);
                    insertCommand.Parameters.AddWithValue("@destinataryId", message.DestinataryId);
                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public static List<Message> GetMessages()
        {
            List<Message> messages = new List<Message>();
            using(SqliteConnection connection = Open())
            {
                string selectQuery = "SELECT * FROM Messages;";
                using (SqliteCommand selectCommand = new SqliteCommand(selectQuery, connection))
                {
                    using (SqliteDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            messages.Add(new Message
                            {
                                Content = reader["Content"].ToString(),
                                RemetentId = reader["RemetentId"].ToString(),
                                DestinataryId = reader["DestinataryId"].ToString()
                            });
                        }
                    }
                }
            }

            return messages;
        }

        public static void Close(SqliteConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
