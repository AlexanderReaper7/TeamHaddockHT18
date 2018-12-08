using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamHaddock
{
    public class LampPost
    {
        private static Texture2D texture, normalMap;
        private Vector2 position;

        public static void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Textures/LampPost");
            normalMap = content.Load<Texture2D>(@"Textures/LampPostNormalMap");
        }

        public LampPost(Vector2 position)
        {
            this.position = position;

            InGame.dynamicLight.lights.Add(new PointLight
            {
                IsEnabled = true,
                Color = new Vector4(1f, 1f, 1f, 1f),
                Power = .8f,
                LightDecay = 200,
                Position = new Vector3(position, 80)
            });
        }

        public void DrawColorMap(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height), Vector2.One, SpriteEffects.None, 0);
        }

        public void DrawNormalMap(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(normalMap, position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height), Vector2.One, SpriteEffects.None, 0);
        }
    }
}
