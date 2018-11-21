using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Created by Alexander 11-21
namespace TeamHaddock
{
    class PistolParticle
    {
        public CollidableObject collidableObject;
        private float velocity, direction;

        private bool isAlive = true;
        /// <summary>
        /// Type of damage 
        /// </summary>
        private const InGame.DamageTypes damagetype = InGame.DamageTypes.Pistol;

        public PistolParticle(Texture2D texture, Vector2 position, float velocity, float direction)
        {
            collidableObject = new CollidableObject(texture, position) {Rotation = direction};
            
            this.velocity = velocity;
        }

        public void Update(GameTime gameTime)
        {
            // If particle is outside of screen
            if (collidableObject.Position.X < 0 || collidableObject.Position.Y < 0 || collidableObject.Position.X > Game1.ScreenBounds.X)
            {
                
            }
            // Update position
            collidableObject.Position += new Vector2( );

            if (collidableObject.IsColliding(InGame.player.CollidableObject))
            {
                // Deal damage to player
                InGame.player.TakeDamage(damagetype);
                // Kill this particle
                RemoveFromList();
            }
        }

        private void RemoveFromList()
        {
            isAlive = false;
            foreach (PistolParticle particle in InGame.particles)
            {
                if (!isAlive)
                {
                    InGame.particles.Remove(particle);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(collidableObject.Texture, collidableObject.Position, collidableObject.SourceRectangle, Color.White, collidableObject.Rotation, collidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);
        }

    }
}
