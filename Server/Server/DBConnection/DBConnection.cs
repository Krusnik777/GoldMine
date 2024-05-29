using System.Data.SQLite;

namespace Server
{
    public class DBConnection
    {
        private const string DataSource = "database.db";

        private SQLiteConnection connection;

        public void Open()
        {
            connection = new SQLiteConnection($"Data Source = {DataSource}");
            connection.Open();
        }

        public void Close()
        { 
            connection.Close();
        }

        public void ExecuteCommand(string commandText)
        {
            SQLiteCommand command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
        }

        public SQLiteDataReader ExecuteCommandWithResult(string commandText)
        {
            SQLiteCommand command = new SQLiteCommand(commandText, connection);

            return command.ExecuteReader();
        }

        /*
        private void ExampleQuery()
        {
            SQLiteCommand command = new SQLiteCommand("Select * from player_info", connection);

            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Console.WriteLine(reader.GetName(0) + " " + reader.GetName(1) + " " + reader.GetName(2));

                while (reader.Read())
                {
                    Console.WriteLine(reader.GetValue(0) + " " + reader.GetValue(1) + " " + reader.GetValue(2));
                }
            }
        }*/
    }
}
