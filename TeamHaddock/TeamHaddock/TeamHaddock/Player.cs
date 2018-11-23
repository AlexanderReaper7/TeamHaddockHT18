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

        private const int baseWalkingSpeed = 1;

        private float jumpAndFallSpeed = 750f;
              
        private bool isJumping = false;
        private bool isFalling = false;

        // dropDownGrounded is if the player is if the player is standing on a drop-down platform 
        private bool dropDownGrounded = false;

        // grounded is if the player is standing on a non drop-down platform
        private bool grounded = false; 
       
        private bool wallCollisionUp = false, wallCollisionDown = false, wallCollisionLeft = false, wallCollisionRight = false;

        private bool rightKey, leftKey; 

        // Temp variable
        private float movementSpeedUpgrade;

        public int Health { get; private set; } = 100;

        /// <summary>
        ///     Called upon to load player textures etc.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            // Create a new collidableObject
            collidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/Player"), new Vector2(250, 250), new Rectangle(60, 0, 60, 120), 0);
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
                    Health -= 8 + 2 * WaveManager.CurrentWave;
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

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        public void MoveLeft(GameTime gameTime)
        {
            //leftKey = true;
            velocity.X -= baseWalkingSpeed + movementSpeedUpgrade;
        }

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        public void MoveRight(GameTime gameTime)
        {
            //leftKey = true;
            velocity.X += baseWalkingSpeed + movementSpeedUpgrade;
        }

        // Created by Noble 11-21 
        public void DropDown()
        {
            // dropDownGrounded is when the player is standing on a drop-down platform
            if (dropDownGrounded && !isFalling && !isJumping)   
            {
                isFalling = true; 
                // Fall igenom plattformen med att stänga av collisionen för spelaren när spelaren fortfarande colliderar med den, och sätt på collision för plattformar när den har passerat förbi den
                //if (!collidableObject.IsColliding()) dropDownGrounded = false;
                //grounded = false; 
            }
        }

        // Created by Noble 11-07
        /// <summary>
        ///     Debugging keyboard controls for InGame !!!ONLY USED EXPERIMENTALLY!!!
        private void Controls(GameTime gameTime)
        {
            // Edited by Alexander 11-21,  
            #region Alexanders debug controls
            // If W or Up arrow key is pressed down 
            if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            {
                collidableObject.Position.Y--;
            }

            // If A or Left arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            {
                collidableObject.Position.X--;
            }

            // If S or Down arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            {
                collidableObject.Position.Y++;
            }

            // If D or Right arrow key is pressed down
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            {
                collidableObject.Position.X++;
            }
            // If Q key is pressed down then rotate counter-clockwise
            if (keyboard.IsKeyDown(Keys.Q))
            {
                collidableObject.Rotation -= MathHelper.TwoPi / 72;
            }
            // If E key is pressed down then rotate clockwise
            if (keyboard.IsKeyDown(Keys.E))
            {
                collidableObject.Rotation += MathHelper.TwoPi / 72;
            }
            // fire a bullet with space
            if (UtilityClass.SingleActivationKey(Keys.Space))
            {
                InGame.particles.Add(new PistolParticle(InGame.pistolParticle, collidableObject.Position + new Vector2(20f), 0.4f, 0.2f));
            }
            #endregion

            // Edited by Noble 11-07, 11-21, 
            //#region Controls
            //if (keyboard.IsKeyDown(Keys.Left))
            //{
            //    MoveLeft(gameTime);
            //}

            //if (keyboard.IsKeyDown(Keys.Right))
            //{
            //    MoveRight(gameTime);
            //}

            //if (UtilityClass.SingleActivationKey(Keys.Z))
            //{
            //    Attack(); 
            //}

            //if (UtilityClass.SingleActivationKey(Keys.Up))
            //{
            //    //Jump(gameTime);
            //}

            //if (UtilityClass.SingleActivationKey(Keys.Down) && grounded)
            //{
            //    DropDown(); 
            //}
            //#endregion
        }

        //  #region Jump

        //// Created by Noble 11-21 
        //public void Jump(GameTime gameTime)
        //{



        //    //Används för att min karaktär inte ska ramla utanför skärmen
        //    int collidableXBounds = Game1.ScreenBounds.X - collidableObject.SourceRectangle.Width;
        //    int collidableYBounds = Game1.ScreenBounds.Y - collidableObject.SourceRectangle.Height;

        //    if (baseWalkingSpeed <= 10)
        //    {
        //        baseWalkingSpeed += gameTime.ElapsedGameTime.Milliseconds / 100;
        //    }


        //    // Gör så att om min karaktär går till vänster sidan utav skärmen, att gå vänster koden slutar att fungera
        //    if (collidableObject.Position.X < collidableXBounds)
        //        wallCollisionLeft = true;
        //    else
        //        wallCollisionLeft = false;



        //    //Detta gör så att jag kan resetta player_sprint_speed
        //    if (keyboard.IsKeyUp(Keys.Left) && rightKey == false) baseWalkingSpeed = 1;

        //    //// Detta gör så att jag kan resetta player_sprint_speed
        //    if (keyboard.IsKeyUp(Keys.Right) && leftKey == false) baseWalkingSpeed = 1;


        //    //// Detta är gravitationen för mitt spel
        //    if (isFalling && jumpAndFallSpeed <= 0)
        //        collidableObject.Position.Y -= 10 * jumpAndFallSpeed / 285;

        //    if (jumpAndFallSpeed <= 0)
        //    {
        //        isFalling = true;
        //        isJumping = false;
        //    }

        //    // Gör så att min karaktär kan hoppa
        //    if (keyboard.IsKeyDown(Keys.Space)) isJumping = true;

        //    // Om du är på plattformen och trycker på space så hoppar du i jump_duration sekunder 
        //    if (isJumping && jumpAndFallSpeed > 0)
        //        collidableObject.Position.Y -= 10 * jumpAndFallSpeed / 285;

        //    if (grounded == true || dropDownGrounded == true)
        //    {
        //        jumpAndFallSpeed = 750f;
        //        isFalling = false;
        //    }

        //    if (isJumping || isFalling)
        //        jumpAndFallSpeed -= gameTime.ElapsedGameTime.Milliseconds * 1.5f;

        //}
        //#endregion

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