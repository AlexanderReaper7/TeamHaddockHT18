using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Class created by Alexander 11-07
namespace TeamHaddock
{
    static class Credits
    {

        public static Texture2D Background;

        static float creditsTime = 0;


        public static void LoadContent(ContentManager content)
        {
            
        }

        public static void Update()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //spriteBatch.Draw(creditsBackground, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(Game1.BoldMenuFont, "Credits".ToString(), new Vector2(5, -5), Color.Black);



            spriteBatch.DrawString(Game1.BoldMenuFont, "Loading".ToString(), new Vector2(10, 30), Color.White);


            if (creditsTime > 200)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, ".".ToString(), new Vector2(85, 30), Color.White);
            }

            if (creditsTime > 500)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, ".".ToString(), new Vector2(92, 30), Color.White);
            }

            if (creditsTime > 800)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, ".".ToString(), new Vector2(92, 30), Color.White);
            }

            if (creditsTime > 1100)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, ".".ToString(), new Vector2(99, 30), Color.White);
            }

            if (creditsTime > 1500)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, "Graphics".ToString(), new Vector2(10, 100), Color.White);
            }

            if (creditsTime > 1550)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Stock graphic".ToString(), new Vector2(10, 130), Color.White);
            }

            if (creditsTime > 1600)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Stock graphic 2".ToString(), new Vector2(10, 150), Color.White);
            }

            if (creditsTime > 1650)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Stock graphic 3".ToString(), new Vector2(10, 170), Color.White);
            }

            if (creditsTime > 1700)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Stock Graphic 4".ToString(), new Vector2(10, 190), Color.White);
            }

            if (creditsTime > 1750)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, "Code".ToString(), new Vector2(10, 250), Color.White);
            }

            if (creditsTime > 1800)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Stock code ".ToString(), new Vector2(10, 280), Color.White);
            }

            if (creditsTime > 1850)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Stock code 2 ".ToString(), new Vector2(10, 300), Color.White);
            }

            if (creditsTime > 1900)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Stock code 3 ".ToString(), new Vector2(10, 320), Color.White);
            }

            if (creditsTime > 1950)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Stock code 4 ".ToString(), new Vector2(10, 340), Color.White);
            }


            spriteBatch.End();
        }
    
    }
}
