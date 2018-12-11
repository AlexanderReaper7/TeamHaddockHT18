using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Class created by Alexander 11-07 // Class edited by Adam 12-10 // Class edited by Noble 12-11
namespace TeamHaddock
{
    internal static class Credits
    {

        public static Texture2D Background;

        static float creditsTime = 0;

        // Edited by Noble 12-11 
        public static void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Background = content.Load<Texture2D>(@"Textures/Backgrounds/Credits");
        }
        
        // Edited by Noble 12-11 
        public static void Update(GameTime gameTime)
        {
            if (UtilityClass.SingleActivationKey(Keys.Escape))
            {
                Game1.GameState = Game1.GameStates.MainMenu; 
            }

            creditsTime += gameTime.ElapsedGameTime.Milliseconds  / 2; 
        }
        
        // Edited by Noble 12-11 
        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();

            //spriteBatch.Draw(creditsBackground, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(Background, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), Color.White);


            spriteBatch.DrawString(Game1.BoldMenuFont, "Credits", new Vector2(5, -5), Color.Black);



            spriteBatch.DrawString(Game1.BoldMenuFont, "Loading", new Vector2(10, 30), Color.White);


            if (creditsTime > 200)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, ".", new Vector2(85, 30), Color.White);
            }

            if (creditsTime > 500)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, ".", new Vector2(92, 30), Color.White);
            }

            if (creditsTime > 800)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, ".", new Vector2(92, 30), Color.White);
            }

            if (creditsTime > 1100)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, ".", new Vector2(99, 30), Color.White);
            }

            if (creditsTime > 1600)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, "Graphics", new Vector2(10, 100), Color.White);
            }

            if (creditsTime > 1800)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Adam ", new Vector2(10, 130), Color.White);
            }

            if (creditsTime > 2000)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Alexander ", new Vector2(10, 150), Color.White);
            }

            if (creditsTime > 2200)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Noble ", new Vector2(10, 170), Color.White);
            }

            if (creditsTime > 2400)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Elias ", new Vector2(10, 190), Color.White);
            }

            if (creditsTime > 2600)
            {
                spriteBatch.DrawString(Game1.BoldMenuFont, "Code", new Vector2(10, 250), Color.White);
            }

            if (creditsTime > 2800)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Adam ", new Vector2(10, 280), Color.White);
            }

            if (creditsTime > 3000)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Alexander ", new Vector2(10, 300), Color.White);
            }

            if (creditsTime > 3200)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Other guy ", new Vector2(10, 320), Color.White);
            }

            if (creditsTime > 3400)
            {
                spriteBatch.DrawString(Game1.NormalMenuFont, "Noble ", new Vector2(10, 340), Color.White);
            }


            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
        }
    
    }
}
