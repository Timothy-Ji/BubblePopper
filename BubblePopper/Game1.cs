using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace BubblePopper
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly Random _rand = new Random();

        private List<IBubble> _bubbles = new List<IBubble>();
        private List<IBubble> _poppedBubbles = new List<IBubble>();

        private MouseState _oldMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private void GenerateRandomBubble()
        {
            int x = _rand.Next(0, Window.ClientBounds.Width);
            int y = _rand.Next(0, Window.ClientBounds.Height);

            _bubbles.Add(new Bubble(x, y) { BubbleGrowthRate = 16 });
        }

        protected override void Update(GameTime gameTime)
        {
            while (_bubbles.Count < 8)
                GenerateRandomBubble();

            UpdateBubbles(gameTime);
            HandleInput(gameTime);

            base.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if (_oldMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                TryPopAt(mouseState.X, mouseState.Y);
            }

            _oldMouseState = Mouse.GetState();


            // TODO: Test Touch Support
            var touch = TouchPanel.GetState();
            if (touch.Count > 0)
            {
                if (touch[0].State == TouchLocationState.Pressed)
                {
                    TryPopAt(touch[0].Position.X, touch[0].Position.Y);
                }
            }
        }

        private void UpdateBubbles(GameTime gameTime)
        {
            foreach (var bubble in _bubbles)
            {
                bubble.Update(gameTime);
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

        private void TryPopAt(float x, float y)
        {
            foreach (var bubble in _bubbles)
            {
                if (Vector2.Distance(new Vector2(x, y), new Vector2(bubble.X, bubble.Y)) < bubble.Radius)
                {
                    bubble.Pop();
                    _bubbles.Remove(bubble);
                    _poppedBubbles.Add(bubble);
                    return;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            DrawBubbles(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBubbles(SpriteBatch spriteBatch, GameTime gameTime)
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
