using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Created by Alexander 11-22
namespace TeamHaddock
{
    public static class WaveManager
    {
        public static int CurrentWave { get; private set; }



        public static void Update()
        {
            // If there are no more enemies
            if (InGame.enemies.Count == 0)
            {
                // Start the next wave
                NextWave();   

            }
        }

        /// <summary>
        /// Starts the next wave
        /// </summary>
        private static void NextWave()
        {
            CurrentWave++;

        }
    }
}
