using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace TeamHaddock
{
    // Created by Elias 11-29 // Edited By Noble 12-11 
    internal static class HighScore
    {
        // file variable
        public static readonly string Filename = "saveFile.dat";

        private static int playerScore = InGame.totalTimeElapsed; 

        private static Texture2D backGround; 

        static string playerName = "Player1";

        // This helps save the two variables PlayerName and Score to the file 
        [Serializable]
        public struct SaveData
        {
            public string[] Playername;
            public int[] Score;

            public int Count;

            public SaveData(int count)
            {
                Playername = new string[count];
                Score = new int[count];
                Count = count;
            }
        }

        // This loads the data of the "Filename" file 
        public static SaveData LoadData(string Filename)
        {
            SaveData data;

            // get path of the save game
            string fullpath = Filename;

            //open the file
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                // read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                data = (SaveData)serializer.Deserialize(stream);
            }
            finally
            {
                // close file
                stream.Close();
            }
            return (data);
        }

        // This saves the data and filename to the file
        public static void DoSave(SaveData data, String filename)
        {
            FileStream stream = File.Open(filename, FileMode.Create);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                serializer.Serialize(stream, data);
            }
            finally
            {
                stream.Close();
            }
        }



        // This saves the score and name to the file 
        private static void SaveHighScore()
        {
            SaveData data = LoadData(Filename);

            int scoreIndex = -1;

            for (int x = 0; x < data.Count; x++)
            {
                if (playerScore > data.Score[x])
                {
                    scoreIndex = x;
                    break;
                }
            }

            if (scoreIndex > -1)
            {
                for (int x = data.Count - 1; x > scoreIndex; x--)
                {
                    data.Score[x] = data.Score[x - 1];
                    
                }


                data.Score[scoreIndex] = playerScore;
                data.Playername[scoreIndex] = playerName;
                //if (playerCharacter == 0)
                //{
                //    data.PlayerName[scoreIndex] = names[0];
                //}
                //if (playerCharacter == 1)
                //{
                //    data.PlayerName[scoreIndex] = names[1];
                //}
                //if (playerCharacter == 2)
                //{
                //    data.PlayerName[scoreIndex] = names[2];
                //}


                DoSave(data, Filename);
            }
        }   


        public static void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            backGround = content.Load<Texture2D>(@"Textures/Backgrounds/HighScoreBackGround"); 
        }

        public static void Update(GameTime gameTime)
        {
            if (UtilityClass.SingleActivationKey(Keys.Escape))
            {
                Game1.GameState = Game1.GameStates.MainMenu; 
            }
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            // Draw 
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), Color.White);
            spriteBatch.DrawString(Game1.ScoreFont, playerScore.ToString(), new Vector2(Game1.ScreenBounds.X / 2 - 50, Game1.ScreenBounds.Y + 20), Color.White);
 
            spriteBatch.End();
            // Clear all render targets
            graphicsDevice.SetRenderTarget(null);
        }
    }
}
