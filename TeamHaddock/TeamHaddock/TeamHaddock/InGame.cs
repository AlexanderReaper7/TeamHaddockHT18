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

        public enum PlayStates : byte
        {
            Tutorial,
            Normal
        }

        public enum DamageTypes : byte
        {
            Pistol,
            Melee
        }

        public static PlayStates playState = PlayStates.Normal;

        private static Texture2D Background;
        private static Texture2D BackgroundNormalMap;

        public static Player player;

        public static List<IEnemy> enemies = new List<IEnemy>();
        public static List<PistolParticle> particles = new List<PistolParticle>();
        private static List<LampPost> lampPosts = new List<LampPost>();
        // Temporary location for pistolParticle location
        public static Texture2D pistolParticle;

        public static void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {

            dynamicLight = new DynamicLight();
            dynamicLight.LoadContent(content, graphicsDevice);

            Background = content.Load<Texture2D>(@"Textures/InGameBackground");
            BackgroundNormalMap = content.Load<Texture2D>(@"Textures/InGameBackgroundNormalMap");
            UserInterface.LoadContent(content);

            player = new Player();
            player.LoadContent(content);

            LampPost.LoadContent(content);

            Texture2D enemyTexture2D = content.Load<Texture2D>(@"Textures/Player");
            pistolParticle = content.Load<Texture2D>(@"Textures/PistolParticle");

            enemies.Add(new MeleeEnemy(enemyTexture2D, new Vector2(100), enemyTexture2D));

            lampPosts.Add(new LampPost(new Vector2(Game1.ScreenBounds.X - 60, Game1.ScreenBounds.Y)));
            lampPosts.Add(new LampPost(new Vector2(60, Game1.ScreenBounds.Y)));
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

            // Update the particles logic
            foreach (PistolParticle particle in particles)
            {
                particle.Update(gameTime);
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
            spriteBatch.Draw(Background, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), Color.White);
            // Draw Platforms
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
            // Draw the particles
            foreach (PistolParticle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
            // Draw player
            player.DrawColorMap(spriteBatch);

            spriteBatch.End();
        }

        private static void DrawNormalMap(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw Background
            spriteBatch.Draw(BackgroundNormalMap, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), Color.White);
            // Draw Platforms
            // Draw LampPosts
            foreach (LampPost lampPost in lampPosts)
            {
                lampPost.DrawNormalMap(spriteBatch);
            }

            // Draw the enemies
            //foreach (IEnemy enemy in enemies)
            //{
            //    enemy.DrawNormalMap(spriteBatch);
            //}
            // Draw player
            player.DrawNormalMap(spriteBatch);


            spriteBatch.End();
        }
    }
}
