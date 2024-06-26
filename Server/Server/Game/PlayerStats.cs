﻿using System;

namespace Server
{
    [Serializable]
    public class PlayerStats
    {
        public int Gold { get; set; }
        public int Level { get; set; }

        public PlayerStats(int gold, int level)
        {
            Gold = gold;
            Level = level;
        }

        public void Update()
        {
            Gold += Level;
        }

        public void NextLevel()
        {
            if (Gold >= 10 * Level)
            {
                Gold -= 10 * Level;
                Level++;
            }
        }

        public void GetBonusGold()
        {
            Gold += 5 * Level;
        }
    }
}
