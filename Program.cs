// using Microsoft.Data.Sqlite => Este namespace fornece classes para trabalhar com o SQLite
// usando o provedor de dados SQLite da Microsoft.
using Microsoft.Data.Sqlite;
// using SQLitePCL => é necessário para usar o Batteries.Init() e é necessário para usar o SQLite
using SQLitePCL;


namespace Consolat
{
    class Program
    {
        static void Main()
        {
            // Método necessário para usar o SQLite, ele usa a abstração do SQLitePCL para fazer com que o banco de dados funcione
            // em diferentes plataformas, é necessário usar Batteries.Init() antes de fazer qualquer manipulação com o banco SQLite em um programa .NET
            Batteries.Init();

            // Criar uma conexão com o banco de dados SQLite (um arquivo será criado automaticamente se não existir)
            // o using serve para garantir a liberação de recursos
            using (SqliteConnection connection = new SqliteConnection("Data Source=messages.db;"))
            {
                connection.Open();

                // Criar uma tabela chamada 'Messages' se não existir
                // Pepara a Query a ser usada no banco de dados
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Messages (Id INTEGER PRIMARY KEY AUTOINCREMENT, Content TEXT);";
                using (SqliteCommand createTableCommand = new SqliteCommand(createTableQuery, connection))
                {
                    // Executa o comando SQLite usando a junção da query com a conexão criada
                    createTableCommand.ExecuteNonQuery();
                }

                // Inserir mensagens na tabela 'Messages'
                List<Message> messages = new List<Message>
                {
                    new Message { Content = "Primeira Mensagem" },
                    new Message { Content = "Segunda Mensagem" },
                    new Message { Content = "Terceira Mensagem" },
                    new Message { Content = "Quarta Mensagem" },
                };

                foreach (Message message in messages)
                {                                                     // VALUES (@content, @senderId, @receiverId, @something)
                    string insertQuery = "INSERT INTO Messages (Content) VALUES (@content);";
                    using (SqliteCommand insertCommand = new SqliteCommand(insertQuery, connection))
                    {
                        // insertCommand.Parameters.AddWithValue("@something", message.something);
                        insertCommand.Parameters.AddWithValue("@content", message.Content);
                        insertCommand.ExecuteNonQuery();
                    }
                }

                // Ler e exibir mensagens da tabela 'Messages'
                string selectQuery = "SELECT * FROM Messages;";
                using (SqliteCommand selectCommand = new SqliteCommand(selectQuery, connection))
                using (SqliteDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Content: {reader["Content"]}");
                    }
                }
            }
        }
    }
}
