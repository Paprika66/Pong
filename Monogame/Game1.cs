using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Monogame;
using Monogame.Source.Engine;
using Monogame.Source.Engine.Exceptions;
using System.ComponentModel.Design;
using System.Threading.Tasks.Sources;

namespace Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        public int screenW;
        public int screenH;
        Player PlayerLeft;
        Bot bot;
        Ball ball;
        Buttons single;
        Buttons multi;
        bool singlePlayer = true;
        bool playing = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            screenW = GraphicsDevice.PresentationParameters.BackBufferWidth;
            screenH = GraphicsDevice.PresentationParameters.BackBufferHeight;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            PlayerLeft = new Player(screenW, screenH, _spriteBatch, Content.Load<Texture2D>("Player"));
            bot = new Bot(screenW, screenH, _spriteBatch, Content.Load<Texture2D>("Player"));
            ball = new Ball(_spriteBatch, Content.Load<Texture2D>("RedWhiteDottedBall"));
            font = Content.Load<SpriteFont>("PlayerScore");
            single = new Buttons(Content.Load<Texture2D>("SingleButton"));
            single.Position = new Vector2(screenW / 2 - 300, 100);
            multi = new Buttons(Content.Load<Texture2D>("MultiButton"));
            multi.Position = new Vector2(screenW / 2 + 100, 100);
            ball.pongHit = Content.Load<SoundEffect>("pong");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(multi.Update(gameTime))
            {
                playing = true;
                singlePlayer = false;
                PlayerLeft.score = 0;
                bot.score = 0;
                bot.speed = 400f;
                ball.Reset();
            }
            if(single.Update(gameTime))
            {
                playing = true;
                singlePlayer = true;
                PlayerLeft.score = 0;
                bot.score = 0;
                bot.speed = 250f;
                ball.Reset();
            }
          
            PlayerLeft.Move(screenW, screenH, ball.Position, gameTime, playing, singlePlayer);
            bot.Move(screenW, screenH, ball.Position, gameTime, playing, singlePlayer);

            try
            {
                ball.Move(gameTime, screenW, screenH, PlayerLeft.BoundingBox, bot.BoundingBox);
            }
            catch (LeftScreenException)
            {
                bot.score++;
                ball.Reset();
            }
            catch (RightScreenException)
            {
                PlayerLeft.score++;
                ball.Reset();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            PlayerLeft.Draw(_spriteBatch);
            bot.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);

            _spriteBatch.DrawString(font, PlayerLeft.score.ToString() , new Vector2(screenW/2-100, 20), Color.White);
            _spriteBatch.DrawString(font, bot.score.ToString(), new Vector2(screenW / 2 + 100, 20), Color.White);
            if (!playing)
            {
                multi.Draw(_spriteBatch);
                single.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}