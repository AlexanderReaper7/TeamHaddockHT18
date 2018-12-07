using System;
using System.Collections.Generic;
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
        public CollidableObject collidableObject;
        private KeyboardState keyboard;

        private Vector2 velocity;

        /// <summary>
        /// The base walking speed for the player 
        /// </summary>
        private const float baseWalkingSpeed = 0.1f, baseJumpStrength = -0.08f;

        private readonly Vector2 maxMovementSpeed = new Vector2(0.5f, 100f);
        private const int maxJumpTime = 200;
        private int jumpTime;
        private bool jumpComplete, onGround;

        public int Health { get; private set; } = 1000000;

        private Animation moveRightAnimation;
        private Animation moveLeftAnimation;

        /// <summary>
        /// The base damage for the enemies pistol 
        /// </summary>
        private const int basePistolDamage = 8;

        /// <summary>
        /// Called upon to load player textures etc.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            // Create a new collidableObject
            collidableObject = new CollidableObject(
                content.Load<Texture2D>(@"Textures/Player"), // The texture
                new Vector2(250, 250), // The spawning position
                new Rectangle(0, 0, 60, 120), // Initial size and position of source rectangle
                0f // The rotation
                );

            int walkingFrameTime = 125;
            
            // Load all frames into movingRightAnimation
            moveRightAnimation = new Animation(new List<Frame>
                {
                    
                    //new Frame(new Rectangle(0, 0, 79, 104), walkingFrameTime),
                    new Frame(new Rectangle(80, 0, 75, 104), walkingFrameTime),
                    new Frame(new Rectangle(156, 0, 74, 104), walkingFrameTime),
                    new Frame(new Rectangle(80, 0, 75, 104), walkingFrameTime),
                    //new Frame(new Rectangle(0, 0, 79, 104), walkingFrameTime),
                    new Frame(new Rectangle(231, 0, 80, 104), walkingFrameTime),
                    new Frame(new Rectangle(311, 0, 78, 104), walkingFrameTime),
                    new Frame(new Rectangle(231, 0, 80, 104), walkingFrameTime),
                }
            );

            moveLeftAnimation = new Animation(new List<Frame>
                {
                    //new Frame(new Rectangle(0, 104, 79, 104), walkingFrameTime),
                    new Frame(new Rectangle(80, 104, 75, 104), walkingFrameTime),
                    new Frame(new Rectangle(156, 104, 74, 104), walkingFrameTime),
                    new Frame(new Rectangle(80, 104, 75, 104), walkingFrameTime),
                    //new Frame(new Rectangle(0, 104, 100, 104), walkingFrameTime),
                    new Frame(new Rectangle(231, 104, 80, 104), walkingFrameTime),
                    new Frame(new Rectangle(311, 104, 78, 104), walkingFrameTime),
                    new Frame(new Rectangle(231, 104, 80, 104), walkingFrameTime),
                }
            );
        }

        /// <summary>
        /// Updates player logic
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            UpdateControls(gameTime);
            UpdatePosition(gameTime);
            UpdateVelocity(gameTime);

            HealthDepletion(gameTime);
        }

        // Created by Noble 11-07, edited by Alexander 12-06
        private void UpdateControls(GameTime gameTime)
        {
            // Update keyboards
            KeyboardState previousKeyboard = keyboard;
            keyboard = Keyboard.GetState();
            // Update ground
            onGround = collidableObject.Position.Y >= Game1.ScreenBounds.Y - collidableObject.SourceRectangle.Y - 3;

            // If W or Up arrow key is pressed down and jump is not complete
            if ((keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up)) && !jumpComplete)
            {
                // Start jump
                // If jumpTime is reset and is on ground
                if (jumpTime == 0 && onGround)
                {
                    Jump(gameTime);
                    return;
                }
                // Continue jump
                // Jump has already started
                if (jumpTime > 0)
                {
                    Jump(gameTime);
                }
            }
            else
            {
                // A key was released, therefore set jump to complete
                jumpComplete = true;
                // if both keys are up and player is on ground
                if (keyboard.IsKeyUp(Keys.W) && keyboard.IsKeyUp(Keys.Up) && onGround)
                {
                    // Reset jump
                    jumpTime = 0;
                    jumpComplete = false;
                }
                
                Fall(gameTime);
            }

            // If A or Left arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            {
                MoveLeft(gameTime);
            }
            // A or Left is not down
            else
            {
                // A or left was previously down but are now both up
                if ((previousKeyboard.IsKeyDown(Keys.A) || previousKeyboard.IsKeyDown(Keys.Left)) && keyboard.IsKeyUp(Keys.A) && keyboard.IsKeyUp(Keys.Left))
                {
                    // Reset animation
                    moveLeftAnimation.SetToFrame(ref collidableObject.SourceRectangle, 0);
                }
            }

            // If D or Right arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            {
                MoveRight(gameTime);
            }
            // D or Right is not down
            else
            {
                // D or Right was previously down but are now both up
                if ((previousKeyboard.IsKeyDown(Keys.D) || previousKeyboard.IsKeyDown(Keys.Right)) && keyboard.IsKeyUp(Keys.D) && keyboard.IsKeyUp(Keys.Right))
                {
                    // Reset animation
                    moveRightAnimation.SetToFrame(ref collidableObject.SourceRectangle, 0);
                }
            }

            // Debug controls
            #if DEBUG
                // If Q key is pressed down then rotate counter-clockwise
                if (keyboard.IsKeyDown(Keys.Q))
                {
                    collidableObject.Rotation -= MathHelper.TwoPi / 1000 * gameTime.ElapsedGameTime.Milliseconds;
                }
                // If E key is pressed down then rotate clockwise
                if (keyboard.IsKeyDown(Keys.E))
                {
                    collidableObject.Rotation += MathHelper.TwoPi / 1000 * gameTime.ElapsedGameTime.Milliseconds;
                }
                // fire a bullet with space
                if (UtilityClass.SingleActivationKey(Keys.Space) || UtilityClass.SingleActivationKey(Keys.Z))
                {
                    Attack();
                }
                // Go to next frame in moveLeftAnimation with V
                if (UtilityClass.SingleActivationKey(Keys.V))
                {
                    moveLeftAnimation.SetToFrame(ref collidableObject.SourceRectangle, moveLeftAnimation.CurrentFrame + 1);
                }
                // Go to next frame in moveRightAnimation with B
                if (UtilityClass.SingleActivationKey(Keys.B))
                {
                    moveRightAnimation.SetToFrame(ref collidableObject.SourceRectangle, moveRightAnimation.CurrentFrame + 1);
                }
            #endif
        }

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        private void MoveLeft(GameTime gameTime)
        {
            moveLeftAnimation.Animate(ref collidableObject.SourceRectangle, gameTime);
            AddForce(new Vector2(-baseWalkingSpeed, 0));
        }

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        private void MoveRight(GameTime gameTime)
        {
            moveRightAnimation.Animate(ref collidableObject.SourceRectangle, gameTime);
            AddForce(new Vector2(baseWalkingSpeed, 0));
        }


        // Created by Noble 11-21, Edited by Noble 11-28 , Edited by Alexander 12-06
        private void Jump(GameTime gameTime)
        {
            // Add elapsed time to timer
            jumpTime += gameTime.ElapsedGameTime.Milliseconds;
            if (jumpTime < maxJumpTime)
            {
                // set jump force
                velocity.Y = baseJumpStrength * gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                jumpComplete = true;
            }
        }

        private void Fall(GameTime gameTime)
        {
            if (!onGround)
            {
                // Add gravity
                AddForce(new Vector2(0, 0.04f * gameTime.ElapsedGameTime.Milliseconds));
            }
        }

        private void AddForce(Vector2 force)
        {
            velocity.X = MathHelper.Clamp(velocity.X + force.X, -maxMovementSpeed.X, maxMovementSpeed.X);
            velocity.Y = MathHelper.Clamp(velocity.Y + force.Y, -maxMovementSpeed.Y, maxMovementSpeed.Y);
        }

        // Created by Noble 11-21 
        // Edited by Alexander 12-05
        private void Attack()
        {
            InGame.particles.Add(new PistolParticle(InGame.pistolParticle, collidableObject.Position + new Vector2(30f), 0.4f, collidableObject.Rotation));
        }

        private void HealthDepletion(GameTime gameTime)
        {
            Health -= gameTime.ElapsedGameTime.Milliseconds;
        }

        // Created by Alexander 11-22
        public void TakeDamage(InGame.DamageTypes damageType)
        {
            switch (damageType)
            {
                case InGame.DamageTypes.Pistol:
                    Health -= basePistolDamage * WaveManager.CurrentWave; // TODO: REDO
                    break;
                case InGame.DamageTypes.Melee:
                    Health -= 100; // TODO: Change this
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
            }
        }

        // Created by Alexander 11-22
        /// <summary>
        /// Updates position while keeping player within the screen bounds.
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdatePosition(GameTime gameTime)
        {
            // Clamp X position + velocity
            collidableObject.Position.X = MathHelper.Clamp(
                collidableObject.Position.X + (velocity.X * gameTime.ElapsedGameTime.Milliseconds),
                0 + collidableObject.Origin.X,
                Game1.ScreenBounds.X - collidableObject.Origin.X);

            // Clamp Y position + velocity
            collidableObject.Position.Y = MathHelper.Clamp(
                collidableObject.Position.Y + (velocity.Y * gameTime.ElapsedGameTime.Milliseconds),
                0 + collidableObject.Origin.Y,
                Game1.ScreenBounds.Y - collidableObject.Origin.Y);
        }

        private void UpdateVelocity(GameTime gameTime)
        {
            // Reduce velocity
            velocity *= 0.055f * gameTime.ElapsedGameTime.Milliseconds;
            // Truncate velocity
            velocity.X = velocity.X.Truncate(3);
            velocity.Y = velocity.Y.Truncate(3);

        }

        /// <summary>
        ///     Draw Player
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw player
            spriteBatch.Draw(collidableObject.Texture, collidableObject.Position, collidableObject.SourceRectangle, Color.White, collidableObject.Rotation, collidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);
            #if DEBUG
                spriteBatch.DrawString(Game1.NormalMenuFont, $" {velocity}\n {collidableObject.Position}\n {jumpComplete}\n {jumpTime}\n {onGround}", Vector2.One, Color.Green);
            #endif
        }
    }
}