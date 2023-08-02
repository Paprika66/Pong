using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Monogame.Source.Engine.Exceptions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Monogame.Source.Engine
{
    internal class Ball
    {
        protected Vector2 position;
        protected Texture2D texture;
        protected Vector2 speed = new Vector2(-150f,0f);
        public Vector2 Position { get { return position; } }
        public Rectangle BoundingBox { get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); } }
        Random random = new Random();
        public SoundEffect pongHit;

        public Ball(SpriteBatch s, Texture2D texture)
        {
            position = new Vector2(300, 300);
            this.texture = texture;
        }
        public void Move(GameTime gameTime, int screenW, int screenH, Rectangle player1, Rectangle player2)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!Global.ScreenColliderVertical(position.Y + speed.Y * dt, texture.Height,screenH))
                {position.Y = position.Y + speed.Y * dt;}
            else{speed.Y = speed.Y * -1;}

            PlayerBallCollision(player1, player2);

            if (!Global.ScreenColliderHorizontal(position.X + speed.X * dt, texture.Width, screenW))
            { position.X = position.X + speed.X * dt; }
            else
            {
                if (position.X + speed.X * dt < 0)
                {
                    throw new LeftScreenException();
                }
                else
                {
                    throw new RightScreenException();
                }
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Reset()
        {
            if(random.Next(0, 2)==0)
            {
                speed.X = -150f;
                speed.Y = random.Next(0, 50);
            }
            else
            {
                speed.X = 150f;
                speed.Y = random.Next(-50, 0);
            }
            position.X = random.Next(200,600);
            position.Y = random.Next(50, 400);
        }

        private void PlayerBallCollision(Rectangle player1, Rectangle player2)
        {
            if(player1.Intersects(this.BoundingBox) || player2.Intersects(this.BoundingBox))
            {
                pongHit.Play();
                if (Math.Abs(speed.X) < 1000)
                    speed.X *= -1.2f;
                else
                {
                    speed.X *= -1f;
                }
                speed.Y = BallVerticalSpeedCollision(player1, player2);
            }
        }
        private float BallVerticalSpeedCollision(Rectangle player1, Rectangle player2)
        {
            if (player1.Intersects(this.BoundingBox))
                return ((this.BoundingBox.Center.Y - player1.Center.Y) / 4) * 30;
            if (player2.Intersects(this.BoundingBox))
                return ((this.BoundingBox.Center.Y - player2.Center.Y) / 4) * 30;
            return 0;
        }
    }
}
