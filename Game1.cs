using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Emit;
using System.Threading;

namespace Monogame_lesson_5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Tribbles
        Texture2D greyTribbleTexture, brownTribbleTexture, creamTribbleTexture, orangeTribbleTexture, quitButtonTexture,
            tribbleYardTexture, starTrekTexture;
        Rectangle greyTribbleRect, creamTribbleRect, brownTribbleRect, orangeTribbleRect, quitRect;
        Vector2 greyTribbleSpeed, creamTribbleSpeed, brownTribbleSpeed, orangeTribbleSpeed;
        SpriteFont instructionsFont;

        bool stopped = false;
        Random generator = new Random();


        Screen screen; // This variable will keep track of what screen/level we are on
        enum Screen
        {
            Intro,
            TribbleYard
        }
        
        MouseState mouseState, prevMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Intro Screen program";
            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 500; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions

            greyTribbleRect = new Rectangle(generator.Next(100, 700), generator.Next(100, 400), 100, 100);
            greyTribbleSpeed = new Vector2(2, 2);
            creamTribbleRect = new Rectangle(generator.Next(100, 700), generator.Next(100, 400), 100, 100);
            creamTribbleSpeed = new Vector2(0, 2);
            brownTribbleRect = new Rectangle(generator.Next(100, 700), generator.Next(100, 400), 100, 100);
            brownTribbleSpeed = new Vector2(2, 10);
            orangeTribbleRect = new Rectangle(generator.Next(100, 700), generator.Next(100, 400), 100, 100);
            orangeTribbleSpeed = new Vector2(5, 0);

            quitRect = new Rectangle(590, 10, 200, 80);

            screen = Screen.Intro;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            greyTribbleTexture = Content.Load<Texture2D>("tribbleGrey");
            brownTribbleTexture = Content.Load<Texture2D>("tribbleBrown");
            creamTribbleTexture = Content.Load<Texture2D>("tribbleCream");
            orangeTribbleTexture = Content.Load<Texture2D>("tribbleOrange");
            tribbleYardTexture = Content.Load<Texture2D>("TribbleYard");
            starTrekTexture = Content.Load<Texture2D>("StarTrek");

            quitButtonTexture = Content.Load<Texture2D>("QuitButton");

            instructionsFont = Content.Load<SpriteFont>("NormalFont");
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;

            }
            else if (screen == Screen.TribbleYard)
            {
                
                
                if (mouseState.LeftButton == ButtonState.Pressed &&
                         prevMouseState.LeftButton == ButtonState.Released)
                {   
                    //Start/stop the tribbles from moving
                    if (!stopped)
                    {
                        greyTribbleSpeed = new Vector2(0, 0);
                        creamTribbleSpeed = new Vector2(0, 0);
                        orangeTribbleSpeed = new Vector2(0, 0);
                        brownTribbleSpeed = new Vector2(0, 0);
                        stopped = true;
                    }
                    else if (stopped)
                    {
                        greyTribbleSpeed = new Vector2(2, 2);
                        creamTribbleSpeed = new Vector2(0, 2);
                        brownTribbleSpeed = new Vector2(2, 10);
                        orangeTribbleSpeed = new Vector2(5, 0);
                        stopped = false;
                    }
                    //Quit button
                    if (quitRect.Contains(mouseState.Position))
                    { Exit(); }
                }

                // Your previous tribble moving code should go here
                greyTribbleRect.X += (int)greyTribbleSpeed.X;
                greyTribbleRect.Y += (int)greyTribbleSpeed.Y;

                creamTribbleRect.X += (int)creamTribbleSpeed.X;
                creamTribbleRect.Y += (int)creamTribbleSpeed.Y;

                orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
                orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;

                brownTribbleRect.X += (int)brownTribbleSpeed.X;
                brownTribbleRect.Y += (int)brownTribbleSpeed.Y;


                if (greyTribbleRect.Right > _graphics.PreferredBackBufferWidth + 100)
                {
                    greyTribbleRect.X = -100;
                }
                if (greyTribbleRect.Bottom > _graphics.PreferredBackBufferHeight + 100)
                {
                    greyTribbleRect.Y = -100;
                }
                if (brownTribbleRect.Right > _graphics.PreferredBackBufferWidth || brownTribbleRect.Left < 0)
                {
                    brownTribbleSpeed.X *= -1;
                }
                if (brownTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || brownTribbleRect.Top < 0)
                {
                    brownTribbleSpeed.Y *= -1;
                }

                if (orangeTribbleRect.Right > _graphics.PreferredBackBufferWidth || orangeTribbleRect.Left < 0)
                {
                    orangeTribbleSpeed.X *= -1;
                }
                if (orangeTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || orangeTribbleRect.Top < 0)
                {
                    orangeTribbleSpeed.Y *= -1;
                }

                if (creamTribbleRect.Right > _graphics.PreferredBackBufferWidth || creamTribbleRect.Left < 0)
                {
                    creamTribbleSpeed.X *= -1;
                }
                if (creamTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || creamTribbleRect.Top < 0)
                {
                    creamTribbleSpeed.Y *= -1;
                }


                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(starTrekTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(instructionsFont, "Click anywhere to begin", new Vector2(250, 0), Color.White);
            }
            if (screen == Screen.TribbleYard)
            {
                _spriteBatch.Draw(tribbleYardTexture, new Rectangle(0,0, 800,500), Color.White);
                _spriteBatch.Draw(quitButtonTexture, quitRect, Color.Green);
                _spriteBatch.Draw(greyTribbleTexture, greyTribbleRect, Color.White);
                _spriteBatch.Draw(brownTribbleTexture, creamTribbleRect, Color.White);
                _spriteBatch.Draw(creamTribbleTexture, brownTribbleRect, Color.White);
                _spriteBatch.Draw(orangeTribbleTexture, orangeTribbleRect, Color.White);
                _spriteBatch.DrawString(instructionsFont, "Click anywhere to start/stop the tribbles", new Vector2(0, 0), Color.White);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}