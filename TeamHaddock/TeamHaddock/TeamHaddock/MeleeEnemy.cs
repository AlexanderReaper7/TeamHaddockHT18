using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Created by Alexander 11-28
namespace TeamHaddock
{
    public class MeleeEnemy : IEnemy
    {
        private CollidableObject collidableObject;
        private Animation moveLeftAnimation;
        private Animation moveRightAnimation;
        private Vector2 velocity;
        private Vector2 direction;

        private int Health { get; set; } = 100;
        private Color color = Color.White;

        private CollidableObject attackCollidableObject;
        private Animation attackLeftAnimation;
        private Animation attackRightAnimation;
        private int attackOffSet;
        private int timeAttacking;
        private bool attacking;

        public MeleeEnemy(Texture2D texture, Vector2 position, Texture2D attackTexture2D) // TODO: Add Animation attackAnimation
        {
            collidableObject = new CollidableObject(texture, position, new Rectangle(120, 0, 60, 120), 0);
            attackCollidableObject = new CollidableObject(attackTexture2D, Vector2.Zero);

            // Load all frames into Animation
            moveRightAnimation = new Animation(new List<Frame>
                {
                    new Frame(new Rectangle(0, 0, 60, 120), 100),
                    new Frame(new Rectangle(60, 0, 60, 120), 100),
                    new Frame(new Rectangle(120, 0, 60, 120), 100)
                }
            );
            moveLeftAnimation = new Animation(new List<Frame>
                {
                    new Frame(new Rectangle(120, 0, 60, 120), 100),
                    new Frame(new Rectangle(60, 0, 60, 120), 100),
                    new Frame(new Rectangle(0, 0, 60, 120), 100)
                }
            );
            attackLeftAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(0,0,1,1), 1000)
            });
            attackRightAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(0,0,1,1), 1000)
            });

            attackOffSet = 8;
        }

        public void Update(GameTime gameTime)
        {
            UpdateAI(gameTime);
            UpdatePosition(gameTime);
            if (attacking)
            {
                //UpdateAttack(gameTime);
            }
            
        }

        /// <summary>
        /// Moves enemy closer to the player depending their position
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateAI(GameTime gameTime)
        {
            // Move left when player is to the left
            if (collidableObject.Position.X > InGame.player.collidableObject.Position.X - (InGame.player.collidableObject.Origin.X + collidableObject.Origin.X))
            {
                MoveLeft(gameTime);
            }
            // Move right when player is to the right
            if (collidableObject.Position.X < InGame.player.collidableObject.Position.X + (InGame.player.collidableObject.Origin.X + collidableObject.Origin.X))
            {
                MoveRight(gameTime);
            }
            // Stop when enemy is near the player 
            if (collidableObject.Position.X > InGame.player.collidableObject.Position.X - (InGame.player.collidableObject.Origin.X + collidableObject.Origin.X) && collidableObject.Position.X < InGame.player.collidableObject.Position.X + (InGame.player.collidableObject.Origin.X + collidableObject.Origin.X))
            {
                StopMoving();
                if (!attacking)
                {
                    StartAttack();
                }
            }
        }

        private void MoveLeft(GameTime gameTime)
        {
            // Animate left
            moveLeftAnimation.Animate(ref collidableObject.SourceRectangle, gameTime);
            // Set direction to left
            direction = -Vector2.UnitX;
            // Set velocity
            velocity.X = -0.3f; // TODO: Add acceleration
        }

        private void MoveRight(GameTime gameTime)
        {
            // Animate right
            moveRightAnimation.Animate(ref collidableObject.SourceRectangle, gameTime);
            // Set direction to right
            direction = Vector2.UnitX;
            // Set velocity
            velocity.X = 0.3f; // TODO: Add acceleration
        } 

        private void StopMoving()
        {
            // Set velocity to 0
            velocity.X = 0f;
            // If direction is right
            if (direction.X > 0)
            {
                // Set to idle frame of right animation
                moveRightAnimation.SetToFrame(ref collidableObject.SourceRectangle, 0);
            }
            // Else direction is left
            else
            {
                // Set to idle frame of left animation
                moveLeftAnimation.SetToFrame(ref collidableObject.SourceRectangle, 0);
            }
        }

        private void StartAttack()
        {
            // Set attacking to active
            attacking = true;
        }

        private void UpdateAttack(GameTime gameTime)
        {
            // If direction is right
            if (direction.X > 0)
            {
                attackRightAnimation.Animate(ref collidableObject.SourceRectangle, gameTime);
                attackCollidableObject.Position.X = collidableObject.Position.X + attackOffSet;
                attackCollidableObject.Position.Y = collidableObject.Position.Y;

                if (timeAttacking >= attackRightAnimation.TotalFrameTime)
                {
                   EndAttack();
                }
            }
            // Else direction is left
            else
            {
                attackLeftAnimation.Animate(ref attackCollidableObject.SourceRectangle, gameTime);
                attackCollidableObject.Position.X = collidableObject.Position.X - attackOffSet;
                attackCollidableObject.Position.Y = collidableObject.Position.Y;
            }
            // If attack is colliding with player
            if (attackCollidableObject.IsColliding(InGame.player.collidableObject))
            {
                // Deal damage to player
                InGame.player.TakeDamage(InGame.DamageTypes.Melee);
                // Deactivate attack
                EndAttack();
            }
        }

        private void EndAttack()
        {
            attacking = false;
            if (direction.X > 0)
            {
                attackRightAnimation.SetToFrame(ref collidableObject.SourceRectangle, 0);
            }
            else
            {
                attackLeftAnimation.SetToFrame(ref collidableObject.SourceRectangle, 0);
            }
        }

        public void TakeDamage(int damageTaken)
        {
            // Give invincibility frames

            // Deal damage
            Health -= damageTaken;
        }

        // Created by Alexander 11-22
        /// <summary>
        /// Updates position while limiting position to the floor and ceiling in Y and the window sides + origin. 
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdatePosition(GameTime gameTime)
        {
            // Clamp X position + velocity to not go beyond the window + texture
            collidableObject.Position.X = MathHelper.Clamp(
                collidableObject.Position.X + (velocity.X * gameTime.ElapsedGameTime.Milliseconds),
                0 - collidableObject.Origin.X,
                Game1.ScreenBounds.X + collidableObject.Origin.X);

            // Clamp Y position + velocity to not go beyond the window - texture
            collidableObject.Position.Y = MathHelper.Clamp(
                collidableObject.Position.Y + (velocity.Y * gameTime.ElapsedGameTime.Milliseconds),
                0 + collidableObject.Origin.Y,
                Game1.ScreenBounds.Y - collidableObject.Origin.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw enemy
            spriteBatch.Draw(collidableObject.Texture, collidableObject.Position, collidableObject.SourceRectangle, color, collidableObject.Rotation, collidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);
            // If this enemy is attacking
            if (attacking)
            {
                // Draw attack
                spriteBatch.Draw(attackCollidableObject.Texture, attackCollidableObject.Position, attackCollidableObject.SourceRectangle, Color.White, attackCollidableObject.Rotation, attackCollidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);
            }
        } 
    }
}
