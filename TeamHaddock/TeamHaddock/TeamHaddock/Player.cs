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
        public CollidableObject collidableObject;

        private Vector2 velocity, acceleration, direction;

        /// <summary>
        /// Is the player dead?
        /// </summary>
        public bool IsPlayerDead;

        /// <summary>
        /// 
        /// </summary>
        private const int baseWalkingSpeed = 1;

        /// <summary>
        /// 
        /// </summary>
        private float jumpAndFallSpeed = 750f;
              

        /// <summary>
        /// 
        /// </summary>
        private bool isJumping = false;

        /// <summary>
        /// 
        /// </summary>
        private bool isFalling = false;

        // dropDownGrounded is if the player is if the player is standing on a drop-down platform 
        private bool dropDownGrounded = false;

        // grounded is if the player is standing on a non drop-down platform
        private bool grounded = false; 
       
        private bool wallCollisionUp = false;
        private bool wallCollisionDown = false;
        private bool wallCollisionLeft = false;
        private bool wallCollisionRight = false;

        private bool rightKey = false;
        private bool leftKey = false; 

        /// <summary>
        ///     Called upon to load player textures etc.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            // Create a new CollidableObject
            CollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/Player"), new Vector2(250, 250), new Rectangle(60, 0, 60, 120), 0);
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
        // Created by Noble 11-21 
        public void Attack()
        {

        public void TakeDamage(InGame.DamageTypes)
        {

        }

        }
        // Created by Noble 11-21 
        public void MoveLeft(GameTime gameTime)
        {
            if (wallCollisionLeft == false)
            collidableObject.Position.X -= 5 * baseWalkingSpeed / 500;
            leftKey = true;
            if (baseWalkingSpeed < 500) baseWalkingSpeed += gameTime.ElapsedGameTime.Milliseconds;
        }
        // Created by Noble 11-21 
        public void MoveRight(GameTime gameTime)
        {
            if (wallCollisionRight == false)
            collidableObject.Position.X += 5 * baseWalkingSpeed / 500;
            leftKey = true;
            if (baseWalkingSpeed < 500) baseWalkingSpeed += gameTime.ElapsedGameTime.Milliseconds;
        }

        // Created by Noble 11-21 
        public void DropDown()
        {
            if (dropDownGrounded == true && isFalling == false && isJumping == false)  // dropDownGrounded is when the player is standing on a drop-down platform 
            {
                isFalling = true; // Fall igenom plattformen med att stänga av collisionen för den för spelaren när spelaren fortfarande colliderar med den, och sätt på collision för plattformar när den har passerat förbi den
                if (!collidableObject.IsColliding()) 
                    dropDownGrounded = false;
                grounded = false; 
            }
            else if ()
            {

            }
        }

        // Created by Noble 11-07
        /// <summary>
        ///     keyboard controls for InGame
        /// </summary>
        private void Controls(GameTime gameTime)
        {
            // Edited by Alexander 11-21,  
            #region Alexanders test controls
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
            //// If D or Right arrow key is pressed down
            //if (keyboard.IsKeyDown(Keys.Q))
            //{
            //    collidableObject.Rotation -= MathHelper.TwoPi / 72;
            //}
            //// If D or Right arrow key is pressed down
            //if (keyboard.IsKeyDown(Keys.E))
            //{
            //    collidableObject.Rotation += MathHelper.TwoPi / 72;
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

            if (UtilityClass.SingleActivationKey(Keys.X))
            {
                Jump(gameTime);
            }

            if (UtilityClass.SingleActivationKey(Keys.Down) && grounded == true)
            {
                DropDown(); 
            }
            #endregion
        }

        #region Jump och allt som behövs 

        // Created by Noble 11-21 
        public void Jump(GameTime gameTime)
        {



            //Används för att min karaktär inte ska ramla utanför skärmen
            int collidableXBounds = Game1.ScreenBounds.X - collidableObject.SourceRectangle.Width;
            int collidableYBounds = Game1.ScreenBounds.Y - collidableObject.SourceRectangle.Height;

            if (baseWalkingSpeed <= 10)
            {
                baseWalkingSpeed += gameTime.ElapsedGameTime.Milliseconds / 100;
            }


            // Gör så att om min karaktär går till vänster sidan utav skärmen, att gå vänster koden slutar att fungera
            if (collidableObject.Position.X < collidableXBounds)
                wallCollisionLeft = true;
            else
                wallCollisionLeft = false;



            //Detta gör så att jag kan resetta player_sprint_speed
            if (keyboard.IsKeyUp(Keys.Left) && rightKey == false) baseWalkingSpeed = 1;

            //// Detta gör så att jag kan resetta player_sprint_speed
            if (keyboard.IsKeyUp(Keys.Right) && leftKey == false) baseWalkingSpeed = 1;


            //// Detta är gravitationen för mitt spel
            if (isFalling && jumpAndFallSpeed <= 0)
                collidableObject.Position.Y -= 10 * jumpAndFallSpeed / 285;

            if (jumpAndFallSpeed <= 0)
            {
                isFalling = true;
                isJumping = false;
            }

            // Gör så att min karaktär kan hoppa
            if (keyboard.IsKeyDown(Keys.Space)) isJumping = true;

            // Om du är på plattformen och trycker på space så hoppar du i jump_duration sekunder 
            if (isJumping && jumpAndFallSpeed > 0)
                collidableObject.Position.Y -= 10 * jumpAndFallSpeed / 285;

            if (grounded == true || dropDownGrounded == true)
            {
                jumpAndFallSpeed = 750f;
                isFalling = false;
            }

            if (isJumping || isFalling)
                jumpAndFallSpeed -= gameTime.ElapsedGameTime.Milliseconds * 1.5f;

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