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
        private Texture2D NormalMap;

        /// <summary>
        /// The base walking speed for the player 
        /// </summary>
        private const float baseWalkingSpeed = 0.1f, baseJumpStrength = -0.08f;
        private readonly Vector2 maxMovementSpeed = new Vector2(0.5f, 100f);
        private Vector2 velocity;
        private Point direction = new Point(1, 1);
        private const int maxJumpTime = 200;
        private int jumpTime;
        private bool jumpComplete, onGround;

        private int Health { get; set; } = 1000000;

        private Animation CurrentAnimation
        {
            get
            {
                // Attacking
                if (attacking)
                {
                    // X
                    switch (attackDirection.X)
                    {
                        // Left
                        case -1:
                            // Y
                            switch (attackDirection.Y)
                            {
                                // Jumping
                                case -1:
                                    return attackJumpingLeftAnimation;
                                // On ground
                                case 0:
                                    return attackGroundedLeftAnimation;
                                // Falling
                                case 1:
                                    return attackFallingLeftAnimation;
                                // Error
                                default: throw new ArgumentOutOfRangeException();
                            }
                        // Right
                        case 1:
                            // Y
                            switch (attackDirection.Y)
                            {
                                // Jumping
                                case -1:
                                    return attackJumpingRightAnimation;
                                // On ground
                                case 0:
                                    return attackGroundedRightAnimation;
                                // Falling
                                case 1:
                                    return attackFallingRightAnimation;
                                // Error
                                default: throw new ArgumentOutOfRangeException();
                            }
                        // Error
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                // Not attacking
                else
                {
                    // X
                    switch (direction.X)
                    {
                        // Left
                        case -1:
                            // Y
                            switch (direction.Y)
                            {
                                // Jumping
                                case -1:
                                    return jumpingLeftAnimation;
                                // On ground
                                case 0:
                                    return idleLeftAnimation;
                                // Falling
                                case 1:
                                    return fallingLeftAnimation;
                                // Error
                                default: throw new ArgumentOutOfRangeException();
                            }
                        // Right
                        case 1:
                            // Y
                            switch (direction.Y)
                            {
                                // Jumping
                                case -1:
                                    return jumpingRightAnimation;
                                // On ground
                                case 0:
                                    return idleRightAnimation;
                                // Falling
                                case 1:
                                    return fallingRightAnimation;
                                // Error
                                default: throw new ArgumentOutOfRangeException();
                            }
                        // Error
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            set
            {
                // Attacking
                if (attacking)
                {
                    // X
                    switch (attackDirection.X)
                    {
                        // Left
                        case -1:
                            // Y
                            switch (attackDirection.Y)
                            {
                                // Jumping
                                case -1:
                                    attackJumpingLeftAnimation = value;
                                    break;
                                // On ground
                                case 0:
                                    attackGroundedLeftAnimation = value;
                                    break;
                                // Falling
                                case 1:
                                    attackFallingLeftAnimation = value;
                                    break;
                                // Error
                                default: throw new ArgumentOutOfRangeException();
                            }
                            break;
                        // Right
                        case 1:
                            // Y
                            switch (attackDirection.Y)
                            {
                                // Jumping
                                case -1:
                                    attackJumpingRightAnimation = value;
                                break;
                                // On ground
                                case 0:
                                    attackGroundedRightAnimation = value;
                                    break;
                                // Falling
                                case 1:
                                    attackFallingRightAnimation = value;
                                    break;
                                // Error
                                default: throw new ArgumentOutOfRangeException();
                            }

                            break;
                        // Error
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                // Not attacking
                else
                {
                    // X
                    switch (direction.X)
                    {
                        // Left
                        case -1:
                            // Y
                            switch (direction.Y)
                            {
                                // Jumping
                                case -1:
                                    jumpingLeftAnimation = value;
                                    break;
                                // On ground
                                case 0:
                                    idleLeftAnimation = value;
                                    break;
                                // Falling
                                case 1:
                                    fallingLeftAnimation = value;
                                    break;
                                // Error
                                default: throw new ArgumentOutOfRangeException();
                            }
                            break;
                        // Right
                        case 1:
                            // Y
                            switch (direction.Y)
                            {
                                // Jumping
                                case -1:
                                    jumpingRightAnimation = value;
                                    break;
                                // On ground
                                case 0:
                                    idleRightAnimation = value;
                                    break;
                                // Falling
                                case 1:
                                    fallingRightAnimation = value;
                                    break;
                                // Error
                                default: throw new ArgumentOutOfRangeException();
                            }
                            break;
                        // Error
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private Animation idleRightAnimation;
        private Animation idleLeftAnimation;
        private Animation moveRightAnimation;
        private Animation moveLeftAnimation;
        private Animation jumpingRightAnimation;
        private Animation jumpingLeftAnimation;
        private Animation fallingRightAnimation;
        private Animation fallingLeftAnimation;

        private CollidableObject attackCollidableObject;
        private Animation attackJumpingRightAnimation;
        private Animation attackJumpingLeftAnimation;
        private Animation attackFallingRightAnimation;
        private Animation attackFallingLeftAnimation;
        private Animation attackGroundedRightAnimation;
        private Animation attackGroundedLeftAnimation;
        private bool attacking;
        private int timeAttacking;
        private int attackDamage = 10;
        private Point attackDirection;

        private class Ref<T> where T : Animation 
        {
            public T Value { get; set; }
        }

        List<Ref<Animation>> animations = new List<Ref<Animation>>();
        

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
                new Vector2(250), // The spawning position
                new Rectangle(0, 0, 79, 104), // Initial size and position of source rectangle
                0f // The rotation
                );

            // Create a new collidable object for attack collision map
            attackCollidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/PlayerCollisionMap"),
                collidableObject.Position,
                collidableObject.SourceRectangle,
                collidableObject.Rotation);

            // Load normal map texture
            NormalMap = content.Load<Texture2D>(@"Textures/PlayerNormalMap");

            LoadAnimations();
        }

        private void LoadAnimations()
        {
            // The constant animation time
            int walkingFrameTime = 125;

            // Load all frames into their animations

            idleRightAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(80, 0, 75, 104), walkingFrameTime), // TODO
            });
            animations.Add(new Ref<Animation> { Value = idleRightAnimation });

            idleLeftAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(80, 0, 75, 104), walkingFrameTime), // TODO
            });
            animations.Add(new Ref<Animation> { Value = idleLeftAnimation });

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
            });
            animations.Add(new Ref<Animation> {Value = moveRightAnimation});

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
            });
            animations.Add(new Ref<Animation> {Value = moveLeftAnimation});

            jumpingRightAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(0, 209, 72, 105), walkingFrameTime)
            });
            animations.Add(new Ref<Animation> { Value = jumpingRightAnimation });

            jumpingLeftAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(0, 324, 72, 105), walkingFrameTime)
            });
            animations.Add(new Ref<Animation> { Value = jumpingLeftAnimation });

            fallingRightAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(0, 440, 68, 107), walkingFrameTime)
            });
            animations.Add(new Ref<Animation> { Value = fallingRightAnimation });

            //
            fallingLeftAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(0, 547, 68, 107), walkingFrameTime)
            });
            animations.Add(new Ref<Animation> { Value = fallingLeftAnimation });

            // 
            attackJumpingRightAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(72, 209, 62, 113), walkingFrameTime),
                new Frame(new Rectangle(135, 209, 62, 113), walkingFrameTime)
            });
            animations.Add(new Ref<Animation> { Value = attackJumpingRightAnimation });

            attackJumpingLeftAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(72, 324, 62, 113), walkingFrameTime),
                new Frame(new Rectangle(135, 324, 62, 113), walkingFrameTime)
            });
            animations.Add(new Ref<Animation> { Value = attackJumpingLeftAnimation });

            attackGroundedRightAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(80, 0, 75, 104), walkingFrameTime), // TODO
            });
            animations.Add(new Ref<Animation> { Value = attackGroundedRightAnimation });

            attackGroundedLeftAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(80, 0, 75, 104), walkingFrameTime), // TODO
            });
            animations.Add(new Ref<Animation> { Value = attackGroundedLeftAnimation });

            attackFallingRightAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(70, 440, 61, 108), walkingFrameTime),
                new Frame(new Rectangle(133, 440, 146, 108), walkingFrameTime)
            });
            animations.Add(new Ref<Animation> { Value = attackFallingRightAnimation });

            attackFallingLeftAnimation = new Animation(new List<Frame>
            {
                new Frame(new Rectangle(70, 546, 61, 108), walkingFrameTime),
                new Frame(new Rectangle(132, 546, 146, 108), walkingFrameTime)
            });
            animations.Add(new Ref<Animation> { Value = attackFallingLeftAnimation });
        }

        /// <summary>
        /// Updates player logic
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            UpdateControls(gameTime);
            UpdateAnimation(gameTime);
            UpdatePosition(gameTime);
            if (attacking) { UpdateAttack(gameTime);}
            UpdateVelocity(gameTime);
            HealthDepletion(gameTime);
        }


        // Created by Noble 11-07, edited by Alexander 12-06
        private void UpdateControls(GameTime gameTime)
        {
            // Update keyboard
            keyboard = Keyboard.GetState();

            // Reset direction Y-Axis
            direction.Y = Point.Zero.Y;

            // if player hits the ground //or the top of a platform
            onGround = collidableObject.Position.Y >= Game1.ScreenBounds.Y - collidableObject.SourceRectangle.Y;
            
            // If W or Up arrow key is pressed down And jump is not complete TODO: Fix jumping
            if ((keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up)) && !jumpComplete)
            {
                    // Continue jump
                    // Jump has already started
                    if (jumpTime > 0)
                    {
                        Jump(gameTime);
                    }

                    // Start jump
                    // If jumpTime is reset and is on ground
                    if (jumpTime == 0 && onGround)
                    {
                        Jump(gameTime);
                    }
            }
            else
            {
                // key was released, therefore set jump to complete
                jumpComplete = true;
                // if both keys are up and player is on ground
                if (keyboard.IsKeyUp(Keys.W) && keyboard.IsKeyUp(Keys.Up) && onGround)
                {
                    // Reset jump
                    jumpTime = 0;
                    jumpComplete = false;
                }
                // Fall
                Fall(gameTime);
            }

            // If A or Left arrow key is pressed down AND right keys are not down as to prevent pressing both directions at once
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left) && !(keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right)))
            {
                // Move left
                MoveLeft();
            }

            // If D or Right arrow key is pressed down AND left keys are not down as to prevent pressing both directions at once
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right) && !(keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left)))
            {
                // Move Right
                MoveRight();
            }

            // If Space or Z key are pressed down
            if ((UtilityClass.SingleActivationKey(Keys.Space) && !UtilityClass.SingleActivationKey(Keys.Z)) || (UtilityClass.SingleActivationKey(Keys.Z) && !UtilityClass.SingleActivationKey(Keys.Space)))
            {
                // If not already attacking
                if (!attacking)
                {
                    // Start a new attack
                    StartAttack();
                }
            }

            #region Debug controls
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
            #endregion 
        }


        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        private void MoveLeft()
        {
            // Set direction to left
            direction.X = -1;
            AddForce(new Vector2(-baseWalkingSpeed, 0));
        }

        // Created by Noble 11-21 
        // Edited by Alexander 11-22
        private void MoveRight()
        {
            // Set direction to Right
            direction.X = 1;
            AddForce(new Vector2(baseWalkingSpeed, 0));
        }


        // Created by Noble 11-21, Edited by Noble 11-28, Edited by Alexander 12-06
        private void Jump(GameTime gameTime)
        {
            // Add elapsed time to timer
            jumpTime += gameTime.ElapsedGameTime.Milliseconds;
            // update direction to up
            direction.Y = -1;
            // if timer has not expired
            if (jumpTime < maxJumpTime)
            {
                // set velocity to jump
                velocity.Y = baseJumpStrength * gameTime.ElapsedGameTime.Milliseconds;
            }
            // Else timer has expired
            else
            {
                // Complete jump
                jumpComplete = true;
            }
        }

        /// <summary>
        /// Adds gravity to velocity if not onGround
        /// </summary>
        /// <param name="gameTime"></param>
        private void Fall(GameTime gameTime)
        {
            if (!onGround)
            {
                // Add gravity
                AddForce(new Vector2(0, 0.04f * gameTime.ElapsedGameTime.Milliseconds));
            }
        }

        /// <summary>
        /// Adds a force to velocity while clamping velocity to maxMovementSpeed
        /// </summary>
        /// <param name="force"></param>
        private void AddForce(Vector2 force)
        {
            velocity.X = MathHelper.Clamp(velocity.X + force.X, -maxMovementSpeed.X, maxMovementSpeed.X);
            velocity.Y = MathHelper.Clamp(velocity.Y + force.Y, -maxMovementSpeed.Y, maxMovementSpeed.Y);
        }


        private void StartAttack()
        {
            // Set attacking to active
            attacking = true;
            // Set attackDirection to current direction
            attackDirection = direction;
        }

        private void UpdateAttack(GameTime gameTime)
        {
            // Update attack collidable
            attackCollidableObject.Position = collidableObject.Position;
            attackCollidableObject.SourceRectangle = collidableObject.SourceRectangle;
            attackCollidableObject.Rotation = collidableObject.Rotation;

            // Check attack collision to every enemy
            foreach (IEnemy enemy in InGame.enemies)
            {
                if (enemy.CollidableObject.IsColliding(attackCollidableObject))
                {
                    enemy.TakeDamage(attackDamage);
                }
            }

            // Add elapsed time to timeAttacking
            timeAttacking += gameTime.ElapsedGameTime.Milliseconds;

            // Ends attack if it has been active for one animation loop
            if (timeAttacking >= CurrentAnimation.TotalFrameTime)
            {
                EndAttack();
            }
        }

        private void EndAttack()
        {
            // End attack
            attacking = false;
        }

        /// <summary>
        /// Depletes health over time
        /// </summary>
        /// <param name="gameTime"></param>
        private void HealthDepletion(GameTime gameTime)
        {
            Health -= gameTime.ElapsedGameTime.Milliseconds;
        }

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


        private void UpdateAnimation(GameTime gameTime)
        {
            CurrentAnimation.Animate(ref collidableObject.SourceRectangle, gameTime);

            // Reset all other animations except from the CurrentAnimation
            foreach (var @ref in animations)
            {
                if (ReferenceEquals(@ref.Value, CurrentAnimation)) { return;}
                @ref.Value.Reset();
            }
        }

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

        /// <summary>
        /// Prepares velocity for next update
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateVelocity(GameTime gameTime)
        {
            // Reduce velocity when player is not doing anything
            if (direction == Point.Zero)
            {
                velocity *= 0.055f * gameTime.ElapsedGameTime.Milliseconds;               
            }

            // Truncate velocity
            velocity.X = velocity.X.Truncate(3);
            velocity.Y = velocity.Y.Truncate(3);
        }


        /// <summary>
        ///     Draw player color map
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawColorMap(SpriteBatch spriteBatch)
        {
            // Draw player
            spriteBatch.Draw(collidableObject.Texture,
                collidableObject.Position,
                collidableObject.SourceRectangle,
                Color.White,
                collidableObject.Rotation,
                collidableObject.Origin,
                1.0f,
                SpriteEffects.None,
                0.0f);
        }


        public void DrawNormalMap(SpriteBatch spriteBatch)
        {
            // Draw player normal map
            spriteBatch.Draw(NormalMap,
                collidableObject.Position,
                collidableObject.SourceRectangle,
                Color.White,
                collidableObject.Rotation,
                collidableObject.Origin,
                1.0f,
                SpriteEffects.None,
                0.0f);
        }
    }
}