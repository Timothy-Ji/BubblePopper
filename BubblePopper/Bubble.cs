using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BubblePopper
{
    public class Bubble : IBubble
    {
        public float X { get; }

        public float Y { get; }

        public float Radius { get; set; } = 1;
        public float MaxRadius { get; set; } = 100;

        public float BubbleGrowthRate { get; set; } = 1;

        public bool IsPopped { get; private set; }

        public bool IsPopAnimationFinished { get; private set; }

        private float _opacity = 1f;

        public Bubble(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Pop()
        {
            IsPopped = true;
            IsPopAnimationFinished = false;
            _opacity = 0.5f;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsPopped)
            {
                Radius = MathHelper.Clamp(Radius + BubbleGrowthRate * (float)gameTime.ElapsedGameTime.TotalSeconds, 0, MaxRadius);
            }
            else if (!IsPopAnimationFinished)
            {
                if (_opacity == 0)
                {
                    IsPopAnimationFinished = true;
                }
                else
                {
                    Radius += 256 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _opacity -= 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IsPopAnimationFinished)
                return;

            spriteBatch.DrawCircle(X, Y, Radius, 16, Color.White * _opacity);
        }
    }
}