using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TeamHaddock
{
    /// <summary>
    ///     Class responsible for Player movement, drawing etc.
    /// </summary>
    internal class Player
    {
        private KeyboardState keyboard;
        private Vector2 velocity; // TODO : Make air-resistance and more fluid movement
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
            // Create a new CollidableObject with starting position 100, 100
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/Player"), new Vector2(Game1.ScreenBounds.X / 2, Game1.ScreenBounds.Y / 2)); // TODO: Move player
            // Create a new particle for the main thruster on the player
        }

        public void Update(GameTime gameTime)
        {
            // Get keyboard state
            keyboard = Keyboard.GetState();

                // And R key is pressed down
                if (keyboard.IsKeyDown(Keys.R))
                {
                    // Then Reset
                    Reset();
                }

            // If player is not dead
            if (!IsPlayerDead)
            {
                // Update Player Controls
                Controls(gameTime);                
                // Gravity
                velocity.Y += 0.08f;
            }

                // Check for collisions to enemies
                _colliding = EnemyManager.CheckCollisionToPlayer();

            // If the player is colliding with the borders or enemies
            if (_colliding)
            {
                // Then explode player
                Explode();
            }

            // Update Background
            // Moves Background along the X-axis when player is within 30 px of the screen center
            if (CollidableObject.Position.X > Game1.ScreenBounds.X / 2 - 15 && CollidableObject.Position.X < Game1.ScreenBounds.X / 2 + 15)
            {
                InGame.MoveBackground(new Vector2(_velocity.X, 0));
            }

            // Moves Background along the Y-axis when player is within 30 px of the screen center
            if (CollidableObject.Position.Y > Game1.ScreenBounds.Y / 2 - 15 && CollidableObject.Position.Y < Game1.ScreenBounds.Y / 2 + 15)
            {
                InGame.MoveBackground(new Vector2(0, _velocity.Y));
            }


            // Update position
            // If player reaches a place where the background no longer can move any further along the X-axis,
            if (InGame.MovableBackground.IsSourceMaxX || InGame.MovableBackground.IsSourceMinX) // TODO : Make Movable background depend on current level
            {
                // Then move the player itself instead of the background,
                CollidableObject.Position.X += _velocity.X;
            }
            else
            {
                // Else move player to the center of the screen along the X-axis.
                CollidableObject.Position.X = Game1.ScreenBounds.X / 2;
            }

            // If player reaches a place where the background no longer can move any further along the Y-axis, 
            if (InGame.MovableBackground.IsSourceMaxY || InGame.MovableBackground.IsSourceMinY)
            {
                // Then move the player itself instead of the background,
                CollidableObject.Position.Y += _velocity.Y;
            }
            else
            {
                // Else move player to the center of the screen along the Y-axis.
                CollidableObject.Position.Y = Game1.ScreenBounds.Y / 2;
            }

            // Slowdown
            // Decrease player acceleration
            Acceleration *= 0.90f;
            // Decrease Player _velocity, acting as air resistance
            _velocity *= 0.95f;

            // TODO : change Acceleration stopping
            if (Acceleration < 0.01)
            {
                Acceleration = 0;
            }
        }

        /// <summary>
        /// Detonates player
        /// </summary>
        private void Explode()
        {
            // Set player dead to true
            IsPlayerDead = true;
            // Change texture to explosion
             CollidableObject.LoadTexture(_explosionTexture2D);

        }

        private void Reset()
        {

            // Change texture back to V-2 rocket
            CollidableObject.LoadTexture(_v2RocketTexture2D); 
            // Reset position
            InGame.MovableBackground.SourceRectangle.X = 0;
            InGame.MovableBackground.SourceRectangle.Y = 4000 - Game1.ScreenBounds.Y;
            // Reset fuel
            Fuel = MaxFuel;
            // set player to true
            IsPlayerDead = false;
        }

        /// <summary>
        /// Animates the flame on the main thruster
        /// </summary>
        /// <param name="gameTime"></param>
        private void AnimateMainThruster(GameTime gameTime)
        {
            // Set new position to _mainThrusterParticle
            _mainThrusterParticle.DestinationRectangle.X = (int)CollidableObject.Position.X - (int)CollidableObject.Origin.X;
            _mainThrusterParticle.DestinationRectangle.Y = (int)CollidableObject.Position.Y - (int)CollidableObject.Origin.Y;
            _mainThrusterParticle.Rotation = CollidableObject.Rotation;
            _mainThrusterParticle.Animate(gameTime);
        }


        /// <summary>
        ///     keyboard controls for InGame
        /// </summary>
        private void Controls(GameTime gameTime)
        {

            // If W or Up arrow key is pressed down and there is fuel remaining
            if ((_keyboard.IsKeyDown(Keys.W) || _keyboard.IsKeyDown(Keys.Up)) && Fuel > 0)
            {
                // Activate main thruster increasing acceleration
                Acceleration += 3;

                // Use fuel
                Fuel -= 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Activate thruster particles
                AnimateMainThruster(gameTime);
                _isMainThrusterActive = true;
            }
            else
            {
                _isMainThrusterActive = false;
            }

            // If A or Left arrow key is pressed down
            if (_keyboard.IsKeyDown(Keys.A) || _keyboard.IsKeyDown(Keys.Left))
            {
                // Activate right side thrusters or fins, rotating counter-clockwise
                CollidableObject.Rotation -= rotationRate * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // If S or Down arrow key is pressed down
            if (_keyboard.IsKeyDown(Keys.S) || _keyboard.IsKeyDown(Keys.Down))
            {
                // Activate counter thrusters decreasing acceleration
                
            }

            // If D or Right arrow key is pressed down
            if (_keyboard.IsKeyDown(Keys.D) || _keyboard.IsKeyDown(Keys.Right))
            {
                // Activate left side thrusters or fins, rotating clockwise
                CollidableObject.Rotation += rotationRate * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // If Space key is pressed down
            if (_keyboard.IsKeyDown(Keys.Space))
            {
                // Then Explode
                Explode();
            }
        }

        /// <summary>
        ///     Draw Player
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw player
            spriteBatch.Draw(CollidableObject.Texture, CollidableObject.Position, null, _colliding && !IsPlayerDead ? Color.Red : Color.White, CollidableObject.Rotation, CollidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);

            if (_isMainThrusterActive)
            {
                // Draw main thruster particle
                //_mainThrusterParticle.Draw(spriteBatch);
            }
        }
    }
}