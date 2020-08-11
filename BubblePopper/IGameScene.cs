using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BubblePopper
{
    public interface IGameScene
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void Update(GameTime gameTime);
    }
}