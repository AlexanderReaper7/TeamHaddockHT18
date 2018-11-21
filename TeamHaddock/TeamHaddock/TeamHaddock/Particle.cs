using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamHaddock
{
    class Particle
    {
        private Texture2D texture;
        private Vector2 position, velocity;
        private Color color1, color2;
        private float duration;

        public Particle(Vector2 position, Vector2 velocity, Color color1, Color color2, float duration)
        {
            this.position = position;
            this.velocity = velocity;
            this.color1 = color1;
            this.color2 = color2;
            this.duration = duration;

        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            velocity = Vector2.Lerp(velocity, Vector2.Zero, duration);
            duration -= gameTime.ElapsedGameTime.Milliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Lerp(color1, color2, duration));
        }

    }
}
