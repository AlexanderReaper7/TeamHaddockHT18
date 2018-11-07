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
            // Create a new CollidableObject with starting position 100, 100
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
        #region PlayerControls.cs 
        //        using System;
        //using System.Collections.Generic;
        //using System.Linq;
        //using System.Runtime.CompilerServices;
        //using System.Text;
        //using Microsoft.Xna.Framework;
        //using Microsoft.Xna.Framework.Input;

        //namespace Lbs.groupproject._2018_2019
        //    {
        //        /// <summary>
        //        /// This class is used to check the player 1's keyboard input 
        //        /// </summary>
        //        static class PlayerControls
        //        {

        //            /// <summary>
        //            /// A bool that is used to check if any player is holding down the "F" button. Used to fullscreen the window when the button is pressed, and window the fullscreen when pressed again 
        //            /// </summary>
        //            //private static bool toggleFullscreen = false;

        //            /// <summary>
        //            /// CurrentKBState is a KeyboardState used to check the players input on their keyboard.
        //            /// </summary>
        //            private static KeyboardState currentKBState;

        //            /// <summary>
        //            /// previousKBState is a KeyboardState that is equal to currentKBState one update ago. 
        //            /// </summary>
        //            private static KeyboardState previousKBState;

        //            public static bool Exit
        //            {
        //                get;
        //                private set;
        //            }

        //            /// <summary>
        //            /// A bool that is used to check if any player is holding down the "F" button. Used to fullscreen the window when the button is pressed, and window the fullscreen when pressed again 
        //            /// </summary>
        //            public static bool ToggleFullscreen
        //            {
        //                get;
        //                private set;
        //            }

        //            /// <summary>
        //            /// Check basic input ex. fullscreen, exit.
        //            /// </summary>
        //            public static void CheckUniversalInput()
        //            {
        //                // set previousKBState to currentKBState
        //                previousKBState = currentKBState;

        //                // Update currentKBState
        //                currentKBState = Keyboard.GetState();

        //                // If F key is pressed and was not pressed last update then set ToggleFullscreen to true
        //                ToggleFullscreen = currentKBState.IsKeyDown(Keys.F) && previousKBState.IsKeyUp(Keys.F);
        //                // If End key is pressed, exit the game.
        //                Exit = currentKBState.IsKeyDown(Keys.End);
        //            }


        //            /// <summary>
        //            /// Check player 1's keyboard input
        //            /// </summary>
        //            public static void CheckPlayer1Input()
        //            {
        //                // Defines gamePad as a GamePadState so you can use it to map controls on a controller    
        //                GamePadState gamePad1 = GamePad.GetState(PlayerIndex.One);



        //                // If the player is holding down W, then the player 1's character goes upward
        //                if (currentKBState.IsKeyDown(Keys.W))
        //                {
        //                    PlayerManager.player1.MoveUp();
        //                }

        //                // If the player is holding down A, then the player 1's character goes left
        //                if (currentKBState.IsKeyDown(Keys.A))
        //                {
        //                    PlayerManager.player1.MoveLeft();
        //                }

        //                // If the player is holding down S, then the player 1's character goes down
        //                if (currentKBState.IsKeyDown(Keys.S))
        //                {
        //                    PlayerManager.player1.MoveDown();
        //                }

        //                // If the player is holding down D, then the player 1's character goes right
        //                if (currentKBState.IsKeyDown(Keys.D))
        //                {
        //                    PlayerManager.player1.MoveRight();
        //                }

        //                // Since previousKBState is one update after currentKBState this will make it a single press.
        //                if (currentKBState.IsKeyDown(Keys.E) && previousKBState.IsKeyUp(Keys.E))
        //                {
        //                    //Attack function
        //                }
        //            }

        //            /// <summary>
        //            /// Check player 2's keyboard input
        //            /// </summary>
        //            public static void CheckPlayer2Input()
        //            {
        //                // Defines gamePad as a GamePadState so you can use it to map controls on a controller    
        //                GamePadState gamePad2 = GamePad.GetState(PlayerIndex.One);



        //                // If the player is holding down W, then the player 1's character goes upward
        //                if (currentKBState.IsKeyDown(Keys.Up))
        //                {
        //                    PlayerManager.player2.MoveUp();
        //                }

        //                // If the player is holding down A, then the player 1's character goes left
        //                if (currentKBState.IsKeyDown(Keys.Left))
        //                {
        //                    PlayerManager.player2.MoveLeft();
        //                }

        //                // If the player is holding down S, then the player 1's character goes down
        //                if (currentKBState.IsKeyDown(Keys.Down))
        //                {
        //                    PlayerManager.player2.MoveDown();
        //                }

        //                // If the player is holding down D, then the player 1's character goes right
        //                if (currentKBState.IsKeyDown(Keys.Right))
        //                {
        //                    PlayerManager.player2.MoveRight();
        //                }

        //                // Since previousKBState is one update after currentKBState this will make it a single press.
        //                if (currentKBState.IsKeyDown(Keys.RightControl) && previousKBState.IsKeyUp(Keys.RightControl))
        //                {
        //                    PlayerManager.player2.MoveUp();
        //                }

        //            }
        //        }
        //    }
        #endregion

        #region Min Spelkaraktär

                    //// Gör så att om min karaktär går till vänster sidan utav skärmen, att gå vänster koden slutar att fungera
                    //if (player_character_position.X<min_x_player)
                    //    wall_collision_left = true;
                    //else
                    //    wall_collision_left = false;

                    //// Gör så att min karaktär kan gå vänster
                    //if (keyboard.IsKeyDown(Keys.Left) && wall_collision_left == false)
                    //{
                    //    player_character_position.X -= 5 * player_sprint_speed / 500;
                    //    key_left = true;
                    //    if (player_sprint_speed< 500) player_sprint_speed += gameTime.ElapsedGameTime.Milliseconds;
                    //}

                    //// Detta gör så att jag kan resetta player_sprint_speed 
                    //if (keyboard.IsKeyUp(Keys.Left) && key_right == false) player_sprint_speed = 1;
                    //// Gör så att min karaktär kan gå höger
                    //if (keyboard.IsKeyDown(Keys.Right))
                    //{
                    //    player_character_position.X += 5 * player_sprint_speed / 500;
                    //    key_right = true;
                    //    if (player_sprint_speed< 500) player_sprint_speed += gameTime.ElapsedGameTime.Milliseconds;
                    //}

                    //// Detta gör så att jag kan resetta player_sprint_speed
                    //if (keyboard.IsKeyUp(Keys.Right) && key_left == false) player_sprint_speed = 1;


                    //// Detta är gravitationen för mitt spel
                    //if (player_is_falling && player_jump_and_fall_speed <= 0)
                    //    player_character_position.Y -= 10 * player_jump_and_fall_speed / 285;
                    //if (player_jump_and_fall_speed <= 0)
                    //{
                    //    player_is_falling = true;
                    //    player_is_jumping = false;
                    //}

                    //// Gör så att min karaktär kan hoppa
                    //if (keyboard.IsKeyDown(Keys.Space)) player_is_jumping = true;
                    //// Om du är på plattformen och trycker på space så hoppar du i jump_duration sekunder 
                    //if (player_is_jumping && player_jump_and_fall_speed > 0)
                    //    player_character_position.Y -= 10 * player_jump_and_fall_speed / 285;
                    //if (player_character_object.Intersects(ground_object))
                    //{
                    //    player_jump_and_fall_speed = 750f;
                    //    player_is_falling = false;
                    //}

                    //if (player_is_jumping || player_is_falling)
                    //    player_jump_and_fall_speed -= gameTime.ElapsedGameTime.Milliseconds* 1.5f;
                    //// Detta används för att kameran för spelet ska flytta på sig om spelaren är i mitten av skärmen
                    ////if (player_character_position.X >= max_x_player)

                    #endregion

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