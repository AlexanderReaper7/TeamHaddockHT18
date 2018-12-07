using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamHaddock
{
    public interface IEnemy
    {
        void DrawColorMap(SpriteBatch spriteBatch);
        void DrawNormalMap(SpriteBatch spriteBatch);
        void TakeDamage(int damageTaken);
        void Update(GameTime gameTime);
    }
}