using System;
using System.Threading;

namespace Server
{
    public class DBPlayerSynchronizer
    {
        private DBPlayerRequestCollection dBPlayerRequestCollection;
        private PlayerList playerList;
        private int timeOut;

        public DBPlayerSynchronizer(DBPlayerRequestCollection dBPlayerRequestCollection, PlayerList playerList, int timeOut)
        {
            this.dBPlayerRequestCollection = dBPlayerRequestCollection;
            this.playerList = playerList;
            this.timeOut = timeOut;
        }

        public void StartSynchronize()
        {
            while (true)
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    dBPlayerRequestCollection.SetPlayerStats(playerList[i]);
                }

                Console.WriteLine("Player stats synchonized with the database");

                Thread.Sleep(timeOut);
            }
        }
    }
}
