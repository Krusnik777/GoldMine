using System;
using System.Threading;

namespace Server
{
    class Application
    {
        public const string PREFIX = "http://192.168.0.192:88/playerStats/";

        static void Main(string[] args)
        {
            // Подключение к базе данных
            DBConnection dBConnection = new DBConnection();
            DBPlayerRequestCollection dBPlayerRequestCollection = new DBPlayerRequestCollection(dBConnection);
            dBConnection.Open();

            // Получение игроков
            PlayerList playerList = new PlayerList();
            playerList.AddPlayers(dBPlayerRequestCollection.GetAllPlayers());
            Console.WriteLine($"List of players loaded from database");

            // Обработка запросов
            ResponseCollection responseCollection = new ResponseCollection(playerList);
            RequestListener requestListener = new RequestListener(playerList, responseCollection, PREFIX);
            Thread requestThread = new Thread(requestListener.StartRequestListen);
            requestThread.Start();

            // Синхронизация с базой данных
            DBPlayerSynchronizer dBPlayerSynchronizer = new DBPlayerSynchronizer(dBPlayerRequestCollection, playerList, 5000);
            Thread dbSynchonizeThread = new Thread(dBPlayerSynchronizer.StartSynchronize);
            dbSynchonizeThread.Start();

            // Игровая логика
            Game game = new Game(playerList);
            while (true)
            {
                game.UpdateGame();
            }

        }
    }
}
