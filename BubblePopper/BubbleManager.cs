using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BubblePopper
{
    public class BubbleManager
    {
        private List<IBubble> _bubbles = new List<IBubble>();
        private List<IBubble> _poppedBubbles = new List<IBubble>();

        private readonly Random _rand = new Random();

        private BubblePopperGame _game;

        public int BubbleCount => _bubbles.Count;

        public BubbleManager(BubblePopperGame game)
        {
            _game = game;
        }

        public void GenerateNewBubble()
        {
            int x = _rand.Next(0, _game.Window.ClientBounds.Width);
            int y = _rand.Next(0, _game.Window.ClientBounds.Height);

            _bubbles.Add(new Bubble(x, y) { BubbleGrowthRate = 16 });
        }

        public IBubble GetBubble(float x, float y)
        {
            foreach (var bubble in _bubbles)
            {
                if (Vector2.Distance(new Vector2(x, y), new Vector2(bubble.X, bubble.Y)) < bubble.Radius)
                {
                    return bubble;
                }
            }
            return null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var bubble in _bubbles.ToArray())
            {
                if (bubble.IsPopped)
                {
                    _bubbles.Remove(bubble);
                    _poppedBubbles.Add(bubble);
                }
                else
                {
                    bubble.Update(gameTime);
                }
            }

            foreach (var bubble in _poppedBubbles)
            {
                bubble.Update(gameTime);

                if (bubble.IsPopAnimationFinished)
                {
                    _poppedBubbles.Remove(bubble);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var bubble in _bubbles)
            {
                bubble.Draw(spriteBatch, gameTime);
            }

            foreach (var bubble in _poppedBubbles)
            {
                bubble.Draw(spriteBatch, gameTime);
            }
        }
    }
}