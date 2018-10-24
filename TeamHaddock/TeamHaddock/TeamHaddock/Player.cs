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
        private KeyboardState _keyboard;
        private bool _isMainThrusterActive;
        private Particle _mainThrusterParticle;
        private bool _colliding;
        private Vector2 _velocity; // TODO : Make air-resistance and more fluid movement
        public float Acceleration;
        public Vector2 Direction;
        public CollidableObject CollidableObject;
        ///
        private readonly float rotationRate = MathHelper.Pi / 2f; // TODO : Add upgrade levels to rotationRate

        /// <summary>
        /// Amount of fuel to start level with
        /// </summary>
        public const float MaxFuel = 200;

        /// <summary>
        /// Fuel Remaining
        /// </summary>
        public float Fuel;

        /// <summary>
        /// Texture for explosion
        /// </summary>
        private Texture2D _explosionTexture2D;

        /// <summary>
        /// Texture for V-2 Rocket
        /// </summary>
        private Texture2D _v2RocketTexture2D;

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
            // Load textures
            _v2RocketTexture2D = content.Load<Texture2D>(@"images/V-2");
            _explosionTexture2D = content.Load<Texture2D>(@"images/Explosion1");
            // Create a new CollidableObject with starting position 100, 100
            CollidableObject = new CollidableObject(_v2RocketTexture2D, new Vector2(Game1.ScreenBounds.X / 2, Game1.ScreenBounds.Y / 2)); // TODO: Move player
            // Create a new particle for the main thruster on the player
            _mainThrusterParticle = new Particle(content.Load<Texture2D>(@"images/RocketFlameAnimationSpriteSheetCut"),
                new Rectangle((int) CollidableObject.Position.X - (int) CollidableObject.Origin.X,
                    (int) CollidableObject.Position.Y, 30, CollidableObject.Texture.Height),
                new Rectangle(0, 0, 26, 13), new Vector2(CollidableObject.Texture.Height, 0), CollidableObject.Rotation,
                150);
            // Set Fuel
            Fuel = MaxFuel;
            // Logging statement
            Console.WriteLine("Player Loaded");
        }

        public void Update(GameTime gameTime)
        {
            // Get keyboard state
            _keyboard = Keyboard.GetState();

                // And R key is pressed down
                if (_keyboard.IsKeyDown(Keys.R))
                {
                    // Then Reset
                    Reset();
                }

            // If player is dead
            if (!IsPlayerDead)
            {
                // Update Player Controls
                Controls(gameTime);                
                // Get new direction derived from Player Rotation TODO: Move direction to CollidableObject
                Direction = new Vector2((float)Math.Cos(CollidableObject.Rotation), (float)Math.Sin(CollidableObject.Rotation));
                // Get new velocity TODO: re-document and refine and add Mass
                _velocity += Direction * Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Gravity TODO: Make gravity more fluid
                _velocity.Y += 0.08f;
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