using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Class created by Alexander 11-07
namespace TeamHaddock
{
    /// <summary>
    /// Class for InGame GameState
    /// </summary>
    public static class InGame 
    {
        public static DynamicLight dynamicLight;

        public static Player player;

        private static Texture2D backgroundColorMap;
        private static Texture2D backgroundNormalMap;

        private static Texture2D groundColorMap;
        private static Texture2D groundNormalMap;
        public static Rectangle groundRectangle;

        public static int time;

        public static List<IEnemy> enemies = new List<IEnemy>();
        private static List<LampPost> lampPosts = new List<LampPost>();

        //Edited by Noble 12-10, Alexander 12-11
        public static void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {

            dynamicLight = new DynamicLight();
            dynamicLight.LoadContent(content, graphicsDevice);

            backgroundColorMap = content.Load<Texture2D>(@"Textures/Backgrounds/InGameBackground");
            backgroundNormalMap = content.Load<Texture2D>(@"Textures/Backgrounds/InGameBackgroundNormalMap");

            groundColorMap = content.Load<Texture2D>(@"Textures/ActiveObjects/Ground");
            groundNormalMap = content.Load<Texture2D>(@"Textures/ActiveObjects/GroundNormalMap");
            groundRectangle = new Rectangle(0, Game1.ScreenBounds.Y - groundColorMap.Height, Game1.ScreenBounds.X, groundColorMap.Height);

            UserInterface.LoadContent(content);

            player = new Player();
            player.LoadContent(content);

            MeleeEnemy.LoadContent(content);

            LampPost.LoadContent(content);

            lampPosts.Add(new LampPost(new Vector2(Game1.ScreenBounds.X - 250, groundRectangle.Top)));
            lampPosts.Add(new LampPost(new Vector2(250, groundRectangle.Top)));
        }

        public static void Update(GameTime gameTime)
        {
            // Update player logic
            player.Update(gameTime);

            // Update enemy logic
            foreach (IEnemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            // Draw color map
            dynamicLight.DrawColorMap(graphicsDevice);
            DrawColorMap(spriteBatch);

            // Clear all render targets
            graphicsDevice.SetRenderTarget(null);

            // Draw normals
            dynamicLight.DrawNormalMap(graphicsDevice);
            DrawNormalMap(spriteBatch);

            // Clear all render targets
            graphicsDevice.SetRenderTarget(null);

            dynamicLight.GenerateShadowMap(graphicsDevice);

            graphicsDevice.Clear(Color.Black);

            dynamicLight.DrawCombinedMaps(spriteBatch);
            // Draw UI
            UserInterface.Draw(spriteBatch);
        }

        private static void DrawColorMap(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Draw Background
            spriteBatch.Draw(backgroundColorMap, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), Color.White);
            // Draw Platforms and ground
            spriteBatch.Draw(groundColorMap, groundRectangle, Color.White);
            // Draw LampPosts
            foreach (LampPost lampPost in lampPosts)
            {
                lampPost.DrawColorMap(spriteBatch);
            }

            // Draw the enemies
            foreach (IEnemy enemy in enemies)
            {
                enemy.DrawColorMap(spriteBatch);
            }
            // Draw player
            player.DrawColorMap(spriteBatch);

            spriteBatch.End();
        }

        private static void DrawNormalMap(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw Background
            spriteBatch.Draw(backgroundNormalMap, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), Color.White);
            // Draw Platforms and ground
            spriteBatch.Draw(groundNormalMap, groundRectangle, Color.White);
            // Draw LampPosts
            foreach (LampPost lampPost in lampPosts)
            {
                lampPost.DrawNormalMap(spriteBatch);
            }

            // Draw the enemies
            foreach (IEnemy enemy in enemies)
            {
                enemy.DrawNormalMap(spriteBatch);
            }
            // Draw player
            player.DrawNormalMap(spriteBatch);


            spriteBatch.End();
        }
    }
}
