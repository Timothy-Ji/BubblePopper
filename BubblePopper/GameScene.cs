using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace BubblePopper
{
    public class GameScene : IGameScene
    {
        private readonly Random _rand = new Random();

        private MouseState _oldMouseState;

        private BubblePopperGame _game;

        private BubbleManager _bubbleManager;

        public GameScene(BubblePopperGame game)
        {
            _game = game;
            _bubbleManager = new BubbleManager(game);
        }

        public void Update(GameTime gameTime)
        {
            while (_bubbleManager.BubbleCount < 8)
                _bubbleManager.GenerateNewBubble();

            _bubbleManager.Update(gameTime);
            HandleInput(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if (_oldMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                _bubbleManager.GetBubble(mouseState.X, mouseState.Y)?.Pop();
            }

            _oldMouseState = Mouse.GetState();


            // TODO: Test Touch Support
            var touch = TouchPanel.GetState();
            if (touch.Count > 0)
            {
                if (touch[0].State == TouchLocationState.Pressed)
                {
                    _bubbleManager.GetBubble(touch[0].Position.X, touch[0].Position.Y)?.Pop();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _bubbleManager.Draw(spriteBatch, gameTime);
        }
    }
}