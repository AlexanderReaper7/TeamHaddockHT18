using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamHaddock
{
    class Enemy
    {
        public CollidableObject CollidableObject;

        private Color Color { get; set; } = Color.White;

        public Enemy(Texture2D texture, Vector2 position)
        {
            CollidableObject = new CollidableObject(texture, position, new Rectangle(120, 0, 60, 120), 0);
        }


        public void Update(GameTime gameTime)
        {
            if (CollidableObject.IsColliding(InGame.player.CollidableObject))
            {
                Color = Color.Red;
            }
            else
            {
                Color = Color.White;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CollidableObject.Texture, CollidableObject.Position, CollidableObject.SourceRectangle, Color, CollidableObject.Rotation, CollidableObject.Origin, 1.0f, SpriteEffects.None, 0.0f);
        } 
    }
}
