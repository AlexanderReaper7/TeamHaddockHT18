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
    public class Player
    {
        private KeyboardState keyboard;
        public CollidableObject CollidableObject;

        private Vector2 velocity, acceleration, direction;

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
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/Player"), new Vector2(250, 250),
                new Rectangle(60, 0, 60, 120), 0);
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
            }
        }

        /// <summary>
        ///     keyboard controls for InGame
        /// </summary>
        private void Controls(GameTime gameTime)
        {

            // If W or Up arrow key is pressed down 
            if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            {
                CollidableObject.Position.Y--;
            }

            // If A or Left arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            {
                CollidableObject.Position.X--;
            }

            // If S or Down arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            {
                CollidableObject.Position.Y++;
            }

            // If D or Right arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            {
                CollidableObject.Position.X++;
            }
            // If D or Right arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.Q))
            {
                CollidableObject.Rotation -= MathHelper.TwoPi / 72;
            }
            // If D or Right arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.E))
            {
                CollidableObject.Rotation += MathHelper.TwoPi / 72;
            }
        }

        /// <summary>
        ///     Draw Player
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw player
            spriteBatch.Draw(CollidableObject.Texture, CollidableObject.Position, CollidableObject.SourceRectangle, Color.White, CollidableObject.Rotation, CollidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);

        }
    }
}