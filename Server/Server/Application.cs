using System.Threading;

namespace Server
{
    class Application
    {
        static void Main(string[] args)
        {
            // Игровая логика
            PlayerList playerList = new PlayerList();
            playerList.AddNewPlayer(new PlayerInfo("TestNick", "73D5B2F4BA82D59C723C16A909524559D8F31E33C5D8FDCFC57065DCA5C9F189"));
            playerList.AddNewPlayer(new PlayerInfo("BigBoss", "C1C224B03CD9BC7B6A86D77F5DACE40191766C485CD55DC48CAF9AC873335D6F"));

            Game game = new Game(playerList);

            // Обработка запросов
            ResponseCollection responseCollection = new ResponseCollection(playerList);
            RequestListener requestListener = new RequestListener(playerList, responseCollection, "http://192.168.0.192:88/playerStats/");
            Thread requestThread = new Thread(requestListener.StartRequestListen);
            requestThread.Start();
            
            while(true)
            {
                game.UpdateGame();
            }

        }
    }
}
