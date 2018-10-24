using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Missile_Master_2
{
    /// <summary>
    ///     Moves the InGame background to create the illusion that the Player is moving
    /// </summary>
    internal class MovableBackground // TODO : Make MovableBackground static
    {
        // TODO: Document me
        private readonly Point _maxSourceBounds;

        public bool IsSourceMaxX;
        public bool IsSourceMaxY;
        public bool IsSourceMinX;
        public bool IsSourceMinY;
        public Rectangle SourceRectangle;

        /// <summary>
        ///     Creates a new MovableBackground with a texture, sourceRectangle and a default destinationRectangle  
        /// </summary>
        /// <param name="texture">The texture associated with the object</param>
        /// <param name="sourceRectangle">The source rectangle used to pick what piece of the texture will be drawn</param>        
        public MovableBackground(Texture2D texture, Rectangle sourceRectangle) : this(texture, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), sourceRectangle)
        {
        }

        /// <summary>
        ///     Creates a new MovableBackground with a texture, destinationRectangle and a sourceRectangle
        /// </summary>
        /// <param name="texture">The texture associated with the object</param>
        /// <param name="destinationRectangle">The rectangle of the object in world space</param>
        /// <param name="sourceRectangle">The source rectangle used to pick what piece of the texture will be drawn</param>
        public MovableBackground(Texture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle)
        {
            Texture = texture;
            DestinationRectangle = destinationRectangle;
            SourceRectangle = sourceRectangle;

            _maxSourceBounds = new Point(Texture.Bounds.Width - Game1.ScreenBounds.X, Texture.Bounds.Height - Game1.ScreenBounds.Y);
        }

        /// <summary>
        ///     The currently loaded texture
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        ///     The rectangle of where to draw on-screen
        /// </summary>
        public Rectangle DestinationRectangle { get; set; }

        /// <summary>
        /// Update Bounds detection
        /// </summary>
        public void Update()
        {
            // Checks if _sourceRectangle is not outside texture
            IsSourceMaxX = SourceRectangle.X >= _maxSourceBounds.X;
            IsSourceMaxY = SourceRectangle.Y >= _maxSourceBounds.Y;
            IsSourceMinX = SourceRectangle.X <= 0;
            IsSourceMinY = SourceRectangle.Y <= 0;
        }

        /// <summary>
        /// Moves SourceRectangle by a point.
        /// </summary>
        /// <param name="moveByPoint">Point to move background by</param>
        public void MoveBackground(Point moveByPoint)
        {
            // TODO: Decide if to have error documentation
            if (moveByPoint == null)
            {
                throw new ArgumentNullException(nameof(moveByPoint));
            }


            // TODO : optimize Logic  
            // X-AXIS
            // If moveByPoint.X is positive,
            if (moveByPoint.X > 0)
            {
                // And moving will not exceed _maxSourceBounds.X,
                if (SourceRectangle.X + moveByPoint.X <= _maxSourceBounds.X)
                {
                    // Then add moveByPoint.X to _sourceRectangle.X
                    SourceRectangle.X += moveByPoint.X;
                }
                else
                {
                    // Else set _sourceRectangle.X to _maxSourceBounds.X
                    SourceRectangle.X = _maxSourceBounds.X;
                }
            }

            // Else moveByPoint.X must be negative or zero
            else
            {
                // And moving will not make _sourceRectangle.X negative,
                if (SourceRectangle.X + moveByPoint.X >= 0)
                {
                    // Then add moveByPoint.X to _sourceRectangle.X
                    SourceRectangle.X += moveByPoint.X;
                }
                else
                {
                    // Else set _sourceRectangle.X to 0
                    SourceRectangle.X = 0;
                }
            }

            // Y-AXIS
            // If moveByPoint.Y is positive,
            if (moveByPoint.Y > 0)
            {
                // And moving will not exceed _maxSourceBounds.X,
                if (SourceRectangle.Y + moveByPoint.Y <= _maxSourceBounds.Y)
                {
                    // Then add moveByPoint.X to _sourceRectangle.X
                    SourceRectangle.Y += moveByPoint.Y;
                }
                else
                {
                    // Else set _sourceRectangle.X to _maxSourceBounds.X
                    SourceRectangle.Y = _maxSourceBounds.Y;
                }
            }

            // Else moveByPoint.Y is negative or zero
            else
            {
                // And moving will not make _sourceRectangle.Y negative,
                if (SourceRectangle.Y + moveByPoint.Y >= 0)
                {
                    // Then add moveByPoint.Y to _sourceRectangle.Y
                    SourceRectangle.Y += moveByPoint.Y;
                }
                else
                {
                    // Else set _sourceRectangle.Y to 0
                    SourceRectangle.Y = 0;
                }
            }
        }

        /// <summary>
        /// Draws background with a texture, DestinationRectangle, SourceRectangle and a color
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Background
            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
        }
    }
}