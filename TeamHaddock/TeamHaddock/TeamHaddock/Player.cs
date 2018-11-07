using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// Class created by Alexander 11-07
namespace TeamHaddock
{
    /// <summary>
    ///     Class responsible for Player movement, drawing etc.
    /// </summary>
    internal class Player
    {
        private KeyboardState keyboard;
        private Vector2 velocity;
        public CollidableObject CollidableObject;

        /// <summary>
        /// Is the player dead?
        /// </summary>
        public bool IsPlayerDead;

        /// <summary>
        ///     Called upon to load player textures etc.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            // Create a new CollidableObject
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/Player"), new Vector2(Game1.ScreenBounds.X / 2, Game1.ScreenBounds.Y / 2));
            // Create a new particle for the main thruster on the player
        }

        public void Update(GameTime gameTime)
        {
            // Get keyboard state
            keyboard = Keyboard.GetState();

            // If player is not dead
            if (!IsPlayerDead)
            {
                // Update Player Controls
                Controls(gameTime);                
                // Gravity
                velocity.Y += 0.08f;
            }
        }

        /// <summary>
        ///     keyboard controls for InGame
        /// </summary>
        private void Controls(GameTime gameTime)
        {

            // If W or Up arrow key is pressed down and there is fuel remaining
            if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            {
            }

            // If A or Left arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            {
            }

            // If S or Down arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            {                
            }

            // If D or Right arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            {
            }
        }

        /// <summary>
        ///     Draw Player
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw player
            spriteBatch.Draw(CollidableObject.Texture, CollidableObject.Position, null, Color.White, CollidableObject.Rotation, CollidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);

        }
    }
}