using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Class created by Alexander 11-07
namespace TeamHaddock
{
    /// <summary>
    /// Class for InGame GameState
    /// </summary>
    static class InGame
    {

        public enum PlayStates
        {
            Tutorial,
            Normal
        }

        public static PlayStates PlayState = PlayStates.Normal;

        public static Player player;

        public static void LoadContent(ContentManager content)
        {
            player = new Player();
            player.LoadContent(content);
        }

        public static void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Draw background
            // TODO: Add background
            // Draw player
            player.Draw(spriteBatch);
        }
    }
}
