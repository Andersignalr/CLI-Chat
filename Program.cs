using System;

namespace Consolat
{
    class Program
    {
        static void Main()
        {
            ConsoleHelper ch = new();
            SeedDatabase();
            int line = ShowMessages();

            ch.gotoxy(0, 27);
            string entry = Console.ReadLine() ?? "";

            while (entry != ".quit")
            {
                ReceiveMessage(entry);
                ch.gotoxy(0, (short)line);
                Console.Write(entry);
                line++;
                ResetEntry(entry, ch);
                ch.gotoxy(0, 27);
                entry = Console.ReadLine() ?? "";
            }
        }

        public static int ShowMessages()
        {
            Database.Initialize();

            var messages = Database.GetMessages();

            foreach (var message in messages)
            {
                Console.WriteLine(message.Content);
            }

            return messages.Count;
        }

        public static void SeedDatabase()
        {
            Database.Initialize();

            try
            {
                var messages = Database.GetMessages();
            }
            catch
            {
                Database.SaveMessage(new Message
                {
                    Content = "Primeira mensagem",
                    RemetentId = "Anderson",
                    DestinataryId = "Arisu"
                });

                Database.SaveMessage(new Message
                {
                    Content = "Oi, deu certo?",
                    RemetentId = "Anderson",
                    DestinataryId = "Maça"
                });

                Database.SaveMessage(new Message
                {
                    Content = "Sim!! ksskks",
                    RemetentId = "Maça",
                    DestinataryId = "Anderson"
                });
            }

        }

        public static void ReceiveMessage(string entry)
        {
            Database.SaveMessage(new Message
            {
                Content = entry,
                RemetentId = "Anderson",
                DestinataryId = "Global"
            });
        }

        public static void ResetEntry(string entry, ConsoleHelper ch)
        {
            string voidContent = "";
            for (int i = 0; i < entry.Length; i++)
            {
                voidContent += " ";
            }

            ch.gotoxy(0, 27);

            Console.Write(voidContent);

            ch.gotoxy(0, 27);

        }
    }
}
