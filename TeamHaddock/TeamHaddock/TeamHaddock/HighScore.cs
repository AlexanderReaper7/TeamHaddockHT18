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
    [Serializable]
    public struct SaveData
    {
        public string[] PlayerName;
        public int[] Score;

        public int Count;

        public SaveData(int count)
        {
            PlayerName = new string[count];
            Score = new int[count];
            Count = count;
        }
    }

    // Created by Elias 11-29 // Edited By Noble 12-11 
    public static class HighScore
    {
        private static Texture2D background;

        private static readonly string FileName = "save.dat";

        private static SaveData currentData;
        private static SpriteFont scoreFont;

        public static void Initilize()
        {
            if (!File.Exists(FileName))
            {
                SaveData data = new SaveData(1);
                data.PlayerName[0] = "kalle";
                data.Score[0] = 0;

                DoSave(data, FileName);
            }

        }

        public static void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(@"Textures/Backgrounds/HighScoreBackGround");
            scoreFont = content.Load<SpriteFont>(@"Fonts/CreditsTitleFont");
        }

        /// <summary>
        /// Opens file and saves data
        /// </summary>
        /// <param name="data">data to write</param>
        /// <param name="filename">name of file to write to</param>
        private static void DoSave(SaveData data, string filename)
        {
            // Open or create file
            FileStream stream = File.Open(filename, FileMode.OpenOrCreate);
            try
            {
                // Make to XML and try to open filestream
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                serializer.Serialize(stream, data);
            }
            finally
            {
                // Close file
                stream.Close();
            }
        }

        private static SaveData LoadData(string FileName)
        {
            SaveData data;

            string fullpath = FileName;

            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                data = (SaveData)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }

            return data;
        }

        public static void SaveHighScore(string playerName, int score)
        {
            SaveData data = LoadData(FileName);

            int scoreIndex = -1;
            for (int i = 0; i < data.Count; i++)
            {
                if (score > data.Score[i])
                {
                    scoreIndex = i;
                    break;
                }
            }

            if (scoreIndex > -1)
            {
                for (int i = 0; i > scoreIndex; i--)
                {
                    data.Score[i] = data.Score[i - 1];
                }

                data.PlayerName[scoreIndex] = playerName;
                data.Score[scoreIndex] = score;
            }

            currentData = data;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Draw background
            spriteBatch.Draw(background, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), Color.White);
            // Draw score
            for (int i = 0; i < currentData.Count -1 && i < 10; i++)
            {
                // Draw Name
                spriteBatch.DrawString(scoreFont, currentData.PlayerName[i], new Vector2(Game1.ScreenBounds.X * 0.25f, i * 60 + 40), Color.White);
                // Draw score
                spriteBatch.DrawString(scoreFont, currentData.Score[i].ToString(), new Vector2(Game1.ScreenBounds.X * 0.75f, i * 60 + 40), Color.White);
            }
            spriteBatch.End();
        }
    }
}
