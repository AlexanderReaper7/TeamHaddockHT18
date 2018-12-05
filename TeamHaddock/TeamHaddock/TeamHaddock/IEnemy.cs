using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamHaddock
{
    public interface IEnemy
    {
        void Draw(SpriteBatch spriteBatch);
        void TakeDamage(int damageTaken);
        void Update(GameTime gameTime);
    }
}