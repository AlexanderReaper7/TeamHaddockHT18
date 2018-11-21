using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using System.IO;

// Class created by Alexander 11-07
namespace TeamHaddock
{
    public class HighScore
    {

        // file variable
        public readonly string Filename = "saveFile.dat";

        int playerScore = 0;

        string playerName = "Player1";

        






        //parts that is going te used in SaveHighScore
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


        private void SaveHighScore()
        {
            // create the data to save
            SaveData data = LoadData(Filename);

            int ScoreIndex = -1;
            for (int i = 0; i < data.Count; i++)
            {
                if (playerScore > data.Score[i])
                {
                    ScoreIndex = i;
                    break;
                }
            }

            if (ScoreIndex > -1)
            {
                //new high score found... do swaps
                for (int i = data.Count - 1; i > ScoreIndex; i--)
                {
                    data.Score[i] = data.Score[i - 1];
                }


                

                data.Score[ScoreIndex] = playerScore;
                data.Playername[ScoreIndex] = playerName;
                DoSave(data, Filename);
            }
        }




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




        public static void LoadContent(ContentManager content)
        {

        }

        public static void Update()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
