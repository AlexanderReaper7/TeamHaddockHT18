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

        public static jump()
        {
            if (player_sprint_speed <= 10) player_sprint_speed += gameTime.ElapsedGameTime.Milliseconds / 100;



            // Gör så att om min karaktär går till vänster sidan utav skärmen, att gå vänster koden slutar att fungera
            if (player_character_position.X < min_x_player)
                wall_collision_left = true;
            else
                wall_collision_left = false;

            // Gör så att min karaktär kan gå vänster
            if (keyboard.IsKeyDown(Keys.Left) && wall_collision_left == false)
            {
                player_character_position.X -= 5 * player_sprint_speed / 500;
                key_left = true;
                if (player_sprint_speed < 500) player_sprint_speed += gameTime.ElapsedGameTime.Milliseconds;
            }

            // Detta gör så att jag kan resetta player_sprint_speed 
            if (keyboard.IsKeyUp(Keys.Left) && key_right == false) player_sprint_speed = 1;
            // Gör så att min karaktär kan gå höger
            if (keyboard.IsKeyDown(Keys.Right))
            {
                player_character_position.X += 5 * player_sprint_speed / 500;
                key_right = true;
                if (player_sprint_speed < 500) player_sprint_speed += gameTime.ElapsedGameTime.Milliseconds;
            }

            // Detta gör så att jag kan resetta player_sprint_speed
            if (keyboard.IsKeyUp(Keys.Right) && key_left == false) player_sprint_speed = 1;


            // Detta är gravitationen för mitt spel
            if (player_is_falling && player_jump_and_fall_speed <= 0)
                player_character_position.Y -= 10 * player_jump_and_fall_speed / 285;
            if (player_jump_and_fall_speed <= 0)
            {
                player_is_falling = true;
                player_is_jumping = false;
            }

            // Gör så att min karaktär kan hoppa
            if (keyboard.IsKeyDown(Keys.Space)) player_is_jumping = true;
            // Om du är på plattformen och trycker på space så hoppar du i jump_duration sekunder 
            if (player_is_jumping && player_jump_and_fall_speed > 0)
                player_character_position.Y -= 10 * player_jump_and_fall_speed / 285;
            if (player_character_object.Intersects(ground_object))
            {
                player_jump_and_fall_speed = 750f;
                player_is_falling = false;
            }

            if (player_is_jumping || player_is_falling)
                player_jump_and_fall_speed -= gameTime.ElapsedGameTime.Milliseconds * 1.5f;
            // Detta används för att kameran för spelet ska flytta på sig om spelaren är i mitten av skärmen
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