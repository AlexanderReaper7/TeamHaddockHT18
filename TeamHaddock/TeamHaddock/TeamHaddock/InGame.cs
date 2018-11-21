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
    static class InGame
    {
        public enum PlayStates : byte
        {
            Tutorial,
            Normal
        }

        public static PlayStates PlayState = PlayStates.Normal;

        public static Player player;

        private static List<Enemy> enemies = new List<Enemy>();

        private static List<Particle> particles = new List<Particle>();

        public static void LoadContent(ContentManager content)
        {
            player = new Player();
            player.LoadContent(content);

            Texture2D enemyTexture2D = content.Load<Texture2D>(@"Textures/Player");


            enemies.Add(new Enemy(enemyTexture2D, new Vector2(100)));

            particles.Add(new Particle(new Vector2(200), new Vector2(5, 20), Color.White, Color.Black, 700));
            particles.Add(new Particle(new Vector2(250), new Vector2(4, 2), Color.White, Color.Black, 600));
            particles.Add(new Particle(new Vector2(300), new Vector2(30, 20), Color.White, Color.Black, 500));
            particles.Add(new Particle(new Vector2(350), new Vector2(34, 20), Color.White, Color.Black, 400));
        }

        public static void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            foreach (Particle particle in particles)
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

            foreach (var particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}
