using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Created by Alexander 11-28
namespace TeamHaddock
{
    public class MeleeEnemy
    {
        public CollidableObject collidableObject;
        private Vector2 velocity;

        private Color Color { get; set; } = Color.White;

        public MeleeEnemy(Texture2D texture, Vector2 position, Texture2D attackTexture2D) // TODO: Add Animation attackAnimation
        {
            collidableObject = new CollidableObject(texture, position, new Rectangle(120, 0, 60, 120), 0);

        }


        public void Update(GameTime gameTime)
        {
            UpdateMovement();
            UpdatePosition(gameTime);
            TestCollisionWithPlayer();

        }
        /// <summary>
        /// Moves enemy closer to the player depending their position
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateMovement()
        {
            // TODO: Add animation and acceleration
            // Move Enemy left when player is to the left
            if (collidableObject.Position.X > InGame.player.collidableObject.Position.X - (InGame.player.collidableObject.Origin.X + collidableObject.Origin.X))
            {
                velocity.X = -0.3f;
            }
            // Move Enemy right when player is to the right
            if (collidableObject.Position.X < InGame.player.collidableObject.Position.X + (InGame.player.collidableObject.Origin.X + collidableObject.Origin.X))
            {
                velocity.X = 0.3f;
            }
            // Stop Enemy when enemy is near the player 
            if (collidableObject.Position.X > InGame.player.collidableObject.Position.X - (InGame.player.collidableObject.Origin.X + collidableObject.Origin.X) && collidableObject.Position.X < InGame.player.collidableObject.Position.X + (InGame.player.collidableObject.Origin.X + collidableObject.Origin.X))
            {
                velocity.X = 0f;
            }
        }

        private void TestCollisionWithPlayer()
        {
            Color = collidableObject.IsColliding(InGame.player.collidableObject) ? Color.Red : Color.White;
        }

        private void Attack()
        {
            // Set atttacking to active
            // Play attack animation
            // Check attack collision with player
            // Damage player
            // 
        }

        // Created by Alexander 11-22
        /// <summary>
        /// Updates position while limiting position to the floor and ceiling in Y and the window sides + origin. 
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdatePosition(GameTime gameTime)
        {
            // Clamp X position + velocity to net go beyondthe window + texture
            collidableObject.Position.X = MathHelper.Clamp(
                collidableObject.Position.X + (velocity.X * gameTime.ElapsedGameTime.Milliseconds),
                0 - collidableObject.Origin.X,
                Game1.ScreenBounds.X + collidableObject.Origin.X);

            // Clamp Y position + velocity
            collidableObject.Position.Y = MathHelper.Clamp(
                collidableObject.Position.Y + (velocity.Y * gameTime.ElapsedGameTime.Milliseconds),
                0 + collidableObject.Origin.Y,
                Game1.ScreenBounds.Y - collidableObject.Origin.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(collidableObject.Texture, collidableObject.Position, collidableObject.SourceRectangle, Color, collidableObject.Rotation, collidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);
            // TODO: Add drawing of attack // If attack is active: Draw attack
        } 
    }
}
