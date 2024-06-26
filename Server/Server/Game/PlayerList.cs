﻿using System;
using System.Collections.Generic;

namespace Server
{
    public class PlayerList
    {
        private List<Player> players = new List<Player>();

        public int Count => players.Count;

        public Player this[int index] => players[index];

        public void AddPlayers(List<Player> players)
        {
            this.players.AddRange(players);
        }

        public void AddNewPlayer(PlayerInfo playerInfo)
        {
            if (!CheckPlayerExist(playerInfo))
            {
                players.Add(new Player(playerInfo));
                Console.WriteLine("Player Added");
            }
        }

        public bool CheckPlayerExist(PlayerInfo playerInfo)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Info.Name == playerInfo.Name && players[i].Info.PasswordHash == playerInfo.PasswordHash) return true;
            }

            return false;
        }

        public void UpdatePlayerStats()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Stats.Update();
            }
        }

        public PlayerStats GetPlayerStats(PlayerInfo playerInfo)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Info.Name == playerInfo.Name) return players[i].Stats;
            }

            return null;
        }
    }
}
