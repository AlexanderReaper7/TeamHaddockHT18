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
        /// Is the player dead?
        /// </summary>
        public bool IsPlayerDead;

        /// <summary>
        /// The base walking speed for the player 
        /// </summary>
        private const float baseWalkingSpeed = 0.1f;

        private float maxMovementSpeed = 0.5f;

        /// <summary>
        /// Used to determine which direction the player is falling, and how fast they are falling 
        /// </summary>
        private float jumpAndFallSpeed = 750f;

        /// <summary>
        /// Used to modify JumpAndFallSpeed 
        /// </summary>
        private float jumpAndFallSpeedModifier = 28f; 
        /// <summary>
        /// Used to reset jumpAndFallSpeed to its original value 
        /// </summary>
        private const float originalJumpAndFallSpeed = 750f;
        
        /// <summary>
        /// Used to check if the player is currently jumping
        /// </summary>                    
        private bool isJumping;

        /// <summary>
        /// Used to check if the player is currently falling 
        /// </summary>
        private bool isFalling;

        /// <summary>
        /// Used to determine if the player is standing on a drop-down platform
        /// </summary>
        private bool dropDownGrounded;

        /// <summary>
        /// Used to determine if the player is standing on a non-drop down platform 
        /// </summary>
        private bool grounded; 
       
        /// <summary>
        /// Used to check if the player is colliding with drop-down platforms only when using the DropDown method 
        /// </summary>
        private bool collidingWithDroppablePlatforms; 
      
        /// <summary>
        /// Used to increase the players movement speed 
        /// </summary>
        private float movementSpeedUpgrade;

        /// <summary>
        /// The health for the player 
        /// </summary>
        public int Health { get; private set; } = 100;

        private Animation moveRightAnimation;
        private Animation moveLeftAnimation;

        /// <summary>
        /// The base damage for the enemies pistol 
        /// </summary>
        private const int basePistolDamage = 8;

        /// <summary>
        /// How much additional damage the pistol enemies will deal based on this modifier 
        /// </summary>
        public int pistolDamageModifier = 2;

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

            int walkingFrameTime = 200;
            
            // Load all frames into movingRightAnimation
            moveRightAnimation = new Animation(new List<Frame>
                {
                    
                    new Frame(new Rectangle(0, 0, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(100, 0, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(200, 0, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(100, 0, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(0, 0, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(400, 0, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(500, 0, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(400, 0, 100, 105), walkingFrameTime),
                }
            );

            moveLeftAnimation = new Animation(new List<Frame>
                {
                    new Frame(new Rectangle(0, 105, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(100, 105, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(200, 105, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(100, 105, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(0, 105, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(400, 105, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(500, 105, 100, 105), walkingFrameTime),
                    new Frame(new Rectangle(400, 105, 100, 105), walkingFrameTime),
                }
            );
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
            UpdatePosition(gameTime);
        }

        // Created by Noble 11-21 
        // Edited by Alexander 12-05
        private void Attack()
        {
            InGame.particles.Add(new PistolParticle(InGame.pistolParticle, collidableObject.Position + new Vector2(30f), 0.4f, collidableObject.Rotation));
        }

        // Created by Alexander 11-22
        public void TakeDamage(InGame.DamageTypes damageType)
        {
            switch (damageType)
            {
                case InGame.DamageTypes.Pistol:
                    Health -= basePistolDamage + pistolDamageModifier * WaveManager.CurrentWave; // TODO: REDO
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

            velocity *= new Vector2(0.8f);
        }

        // Created by Noble 11-07
        private void Controls(GameTime gameTime)
        {
            // Edited by Alexander 11-21,  

            // If W or Up arrow key is pressed down 
            if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            {
                Jump(gameTime);
            }

            // If A or Left arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            {
                MoveLeft(gameTime);
            }

            // If S or Down arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            {
                DropDown();
            }

            // If D or Right arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            {
                MoveRight(gameTime);
            }
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

            if (keyboard.IsKeyDown(Keys.Space))
            {
                Jump(gameTime);
            }


            if (UtilityClass.SingleActivationKey(Keys.Down) && grounded)
            {
                DropDown();
            }
        }

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        private void MoveLeft(GameTime gameTime)
        {
            moveLeftAnimation.Animate(ref collidableObject.SourceRectangle, gameTime);
            velocity.X -= MathHelper.Clamp(baseWalkingSpeed + movementSpeedUpgrade, -maxMovementSpeed, maxMovementSpeed) ;
        }

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        private void MoveRight(GameTime gameTime)
        {
            moveRightAnimation.Animate(ref collidableObject.SourceRectangle, gameTime);
            velocity.X += MathHelper.Clamp(baseWalkingSpeed + movementSpeedUpgrade, -maxMovementSpeed, maxMovementSpeed);
        }


        // Created by Noble 11-21, Edited by Noble 11-28 , Edited by Alexander 12-06
        private void Jump(GameTime gameTime)
        {
            // If not in air
            if (collidableObject.Position)
            {
                // jump upwards
                velocity.Y -= 50f;
            }

            //// The gravity
            //if (isFalling && jumpAndFallSpeed <= 0)
            //{
            //    velocity.Y -= jumpAndFallSpeed / jumpAndFallSpeedModifier;
            //}

            //// If the speed of the player is less than or equal to zero, the player is falling 
            //if (jumpAndFallSpeed <= 0)
            //{
            //    isFalling = true;
            //}

            //// 
            //if (isJumping && jumpAndFallSpeed > 0)
            //{
            //    velocity.Y -= jumpAndFallSpeed / jumpAndFallSpeedModifier;
            //}

            //if (grounded || dropDownGrounded)
            //{
            //    isFalling = false;
            //    isJumping = false;
            //    jumpAndFallSpeed = originalJumpAndFallSpeed;
            //}

            //// If the player is jumping or falling then reduce jumpAndFallSpeed  
            //if (isJumping || isFalling)
            //{
            //    jumpAndFallSpeed -= gameTime.ElapsedGameTime.Milliseconds;
            //}


        }

        // Created by Noble 11-21 
        private void DropDown()
        {
            // If the player is standing fully on a drop-down platform, and is not currently jumping nor falling...
            if (dropDownGrounded && !grounded && !isFalling && !isJumping)
            {
                isFalling = true;
                // Fall through the platform by turning off the collision for all of the platforms on the screen...                   
                //collidingWithDroppablePlatforms = collidableObject.IsColliding(theDropDownPlatforms);
            }
        }

        /// <summary>
        ///     Draw Player
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            
            // Draw player
            spriteBatch.Draw(collidableObject.Texture, collidableObject.Position, collidableObject.SourceRectangle, Color.White, collidableObject.Rotation, collidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}