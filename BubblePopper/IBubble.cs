using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BubblePopper
{
    public interface IBubble
    {
        float X { get; }
        float Y { get; }
        float Radius { get; }
        float BubbleGrowthRate { get; set; }
        bool IsPopped { get; }
        bool IsPopAnimationFinished { get; }

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void Pop();
        void Update(GameTime gameTime);
    }
}