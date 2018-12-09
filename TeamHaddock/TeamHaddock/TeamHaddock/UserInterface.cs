using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamHaddock
{
    public static class UserInterface
    {
        public static void Update() { }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Draw health bar
            // Draw wave number
            spriteBatch.DrawString(Game1.NormalMenuFont, $"{InGame.player.velocity}", Vector2.One, Color.White);
            spriteBatch.End();
        }

    }
}
