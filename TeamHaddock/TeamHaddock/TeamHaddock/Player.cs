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
        private const float baseWalkingSpeed = 5f;

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
        private bool isJumping = false;

        /// <summary>
        /// Used to check if the player is currently falling 
        /// </summary>
        private bool isFalling = false;

        /// <summary>
        /// Used to determine if the player is standing on a drop-down platform
        /// </summary>
        private bool dropDownGrounded = false;

        /// <summary>
        /// Used to determine if the player is standing on a non-drop down platform 
        /// </summary>
        private bool grounded = false; 
       
        /// <summary>
        /// Used to stop the player from walking/falling/jumping out of the window 
        /// </summary>
        private bool wallCollisionUp = false, wallCollisionDown = false, wallCollisionLeft = false, wallCollisionRight = false;

        private bool rightKey, leftKey;

        /// <summary>
        /// Used to check if the player is colliding with drop-down platforms only when using the DropDown method 
        /// </summary>
        private bool collidingWithDroppablePlatforms = false; 
      
        /// <summary>
        /// Used to increase the players movement speed 
        /// </summary>
        private float movementSpeedUpgrade;

        /// <summary>
        /// The health for the player 
        /// </summary>
        public int Health { get; private set; } = 100;

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
            collidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/Player"), new Vector2(250, 200), new Rectangle(60, 0, 60, 120), 0);
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
        public void Attack()
        {
        }

        // Created by Alexander 11-22
        public void TakeDamage(InGame.DamageTypes damagetype)
        {
            switch (damagetype)
            {
                case InGame.DamageTypes.Pistol:
                    Health -= basePistolDamage + pistolDamageModifier * WaveManager.CurrentWave;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(damagetype), damagetype, null);
            }
        }

        // Created by Alexander 11-22
        /// <summary>
        /// Updates position while keeping player within the screen bounds.
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdatePosition(GameTime gameTime)
        {
            //// Clamp X position + velocity
            //collidableObject.Position.X = MathHelper.Clamp(
            //    collidableObject.Position.X + (velocity.X * gameTime.ElapsedGameTime.Milliseconds),
            //    0 + collidableObject.Origin.X,
            //    Game1.ScreenBounds.X - collidableObject.Origin.X);

            //// Clamp Y position + velocity
            //collidableObject.Position.Y = MathHelper.Clamp(
            //    collidableObject.Position.Y + (velocity.Y * gameTime.ElapsedGameTime.Milliseconds),
            //    0 + collidableObject.Origin.Y,
            //    Game1.ScreenBounds.Y - collidableObject.Origin.Y);
        }

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        public void MoveLeft(GameTime gameTime)
        {
            //leftKey = true;
            collidableObject.Position.X -= baseWalkingSpeed; //+ movementSpeedUpgrade;
        }

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        public void MoveRight(GameTime gameTime)
        {
            //leftKey = true;
            collidableObject.Position.X += baseWalkingSpeed; // + movementSpeedUpgrade;
        }

        // Created by Noble 11-21 
        public void DropDown()
        {
            // If the player is standing fully on a drop-down platform, and is not currently jumping nor falling...
            if (dropDownGrounded == true && grounded == false && isFalling == false && isJumping == false)   
            {
                isFalling = true;
                //// Fall through the platform by turning off the collision for all of the platforms on the screen...                   
                //if (collidableObject.IsColliding(theDropDownPlatforms))
                //{
                //    collidingWithDroppablePlatforms = true;
                //}
                //// Then turn the collision on again once the player is not colliding with the platforms anymore 
                //else
                //{
                //    collidingWithDroppablePlatforms = false;
                //}
            }
        }

        // Created by Noble 11-07
        /// <summary>
        ///     Debugging keyboard controls for InGame !!!ONLY USED EXPERIMENTALLY!!!
        private void Controls(GameTime gameTime)
        {
            // Edited by Alexander 11-21,  
            #region Alexanders debug controls
            //// If W or Up arrow key is pressed down 
            //if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            //{
            //    collidableObject.Position.Y--;
            //}

            //// If A or Left arrow key is pressed down
            //if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            //{
            //    collidableObject.Position.X--;
            //}

            //// If S or Down arrow key is pressed down
            //if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            //{
            //    collidableObject.Position.Y++;
            //}

            //// If D or Right arrow key is pressed down
            //if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            //{
            //    collidableObject.Position.X++;
            //}
            //// If Q key is pressed down then rotate counter-clockwise
            //if (keyboard.IsKeyDown(Keys.Q))
            //{
            //    collidableObject.Rotation -= MathHelper.TwoPi / 72;
            //}
            //// If E key is pressed down then rotate clockwise
            //if (keyboard.IsKeyDown(Keys.E))
            //{
            //    collidableObject.Rotation += MathHelper.TwoPi / 72;
            //}
            //// fire a bullet with space
            //if (UtilityClass.SingleActivationKey(Keys.Space))
            //{
            //    InGame.particles.Add(new PistolParticle(InGame.pistolParticle, collidableObject.Position + new Vector2(20f), 0.4f, 0.2f));
            //}
            #endregion

            // Edited by Noble 11-07, 11-21, 
            #region Controls
            if (keyboard.IsKeyDown(Keys.Left))
            {
                MoveLeft(gameTime);
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                MoveRight(gameTime);
            }

            if (UtilityClass.SingleActivationKey(Keys.Z))
            {
                Attack();
            }

            if (keyboard.IsKeyDown(Keys.Space))
            {
                isJumping = true;                 
            }
            
            if (isJumping == true)
            {
                Jump(gameTime);
            }

            if (UtilityClass.SingleActivationKey(Keys.Down) && grounded)
            {
                DropDown();
            }
            #endregion
        }

        #region Jump

        public void CheckWallCollision(GameTime gameTime)
        {
            //Används för att min karaktär inte ska ramla utanför skärmen
            int collidableXBounds = Game1.ScreenBounds.X - collidableObject.SourceRectangle.Width;
            int collidableYBounds = Game1.ScreenBounds.Y - collidableObject.SourceRectangle.Height;

            // If the player is to the edge of a screen with its texture in mind, stop the players movement 
            if (collidableObject.Position.X < collidableXBounds)
                wallCollisionLeft = true;
            else
                wallCollisionLeft = false;
        }

        // Created by Noble 11-21, Edited by Noble 11-28 
        public void Jump(GameTime gameTime)
        {


            // Acceleration 
            //if (baseWalkingSpeed <= 10)
            //{
            //    baseWalkingSpeed += gameTime.ElapsedGameTime.Milliseconds / 100;
            //}

            // Reset the acceleration 
            //if (keyboard.IsKeyUp(Keys.Left) && rightKey == false) baseWalkingSpeed = 1;

            // Detta gör så att jag kan resetta player_sprint_speed
            //if (keyboard.IsKeyUp(Keys.Right) && leftKey == false) baseWalkingSpeed = 1;

            if (collidableObject.IsColliding(GameObject.ground))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            // The gra
            if (isFalling == true && jumpAndFallSpeed <= 0)
            {
                collidableObject.Position.Y -= jumpAndFallSpeed / jumpAndFallSpeedModifier;
            }
            
            // If the speed of the player is less than or equal to zero, the player is falling 
            if (jumpAndFallSpeed <= 0)
            {
                isFalling = true;
            }

            // 
            if (isJumping == true && jumpAndFallSpeed > 0)
            {
                collidableObject.Position.Y -= jumpAndFallSpeed / jumpAndFallSpeedModifier;
            }

            if (grounded == true || dropDownGrounded == true)
            {
                isFalling = false;
                isJumping = false;
                jumpAndFallSpeed = originalJumpAndFallSpeed;
            }

            // If the player is jumping or falling then reduce jumpAndFallSpeed  
            if (isJumping || isFalling)
            {
                jumpAndFallSpeed -= gameTime.ElapsedGameTime.Milliseconds;
            }
                

        }
        #endregion

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