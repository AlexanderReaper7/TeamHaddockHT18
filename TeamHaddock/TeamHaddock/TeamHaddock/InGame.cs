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
            Pistol
            
        }

        public static PlayStates playState = PlayStates.Normal;

        public static Player player;

        public static List<Enemy> enemies = new List<Enemy>();

        public static List<PistolParticle> particles = new List<PistolParticle>();
        public static Texture2D pistolParticle;

        public static void LoadContent(ContentManager content)
        {
            player = new Player();
            player.LoadContent(content);

            Texture2D enemyTexture2D = content.Load<Texture2D>(@"Textures/Player");
            pistolParticle = content.Load<Texture2D>(@"Textures/PistolParticle");

            enemies.Add(new Enemy(enemyTexture2D, new Vector2(100)));
        }

        public static void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

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

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (PistolParticle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}
