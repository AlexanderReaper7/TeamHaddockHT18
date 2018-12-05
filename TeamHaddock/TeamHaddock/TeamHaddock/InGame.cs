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

        public static Player player;

        public static List<IEnemy> enemies = new List<IEnemy>();
        public static List<PistolParticle> particles = new List<PistolParticle>();
        // Temporary location for pistolParticle location
        public static Texture2D pistolParticle;
        private static GameObjectManager gameObjectManager;

        public static void LoadContent(ContentManager content)
        {

            player = new Player();
            player.LoadContent(content);
            Texture2D platformTexture = content.Load<Texture2D>(@"Textures/Ground");
            gameObjectManager = new GameObjectManager(platformTexture, new Vector2(200));
            Texture2D enemyTexture2D = content.Load<Texture2D>(@"Textures/Player");
            pistolParticle = content.Load<Texture2D>(@"Textures/PistolParticle");

            enemies.Add(new MeleeEnemy(enemyTexture2D, new Vector2(100), enemyTexture2D));
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

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Draw background
            // TODO: Add background
            // Draw player
            player.Draw(spriteBatch);
            // Draw the enemies
            foreach (IEnemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            // Draw the particles
            foreach (PistolParticle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}
