using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Monogame
{
    internal class Bot : Player
    {
        public Bot(int screenW, int screenH, SpriteBatch s, Texture2D texture) : base(s, texture)
        {
            position = new Vector2(screenW - texture.Width - 20, screenH / 2);
            speed = 250f;
        }
        public override void Move(int screenW, int screenH, Vector2 ballPosition, GameTime gameTime, bool isPlaying, bool single)
        {

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!single)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (Global.ScreenCollider(new Vector2(position.X, position.Y - 3), texture.Width, texture.Height, screenW, screenH)) { position.Y = position.Y - speed * dt; }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (Global.ScreenCollider(new Vector2(position.X, position.Y + 3), texture.Width, texture.Height, screenW, screenH)) { position.Y = position.Y + speed * dt; }
                }
            }
            else
            {
                if (ballPosition.Y > position.Y + 3 + texture.Height / 2)
                {
                    if (Global.ScreenCollider(new Vector2(position.X, (int)position.Y + 3), texture.Width, texture.Height, screenW, screenH))
                    {
                        position.Y = position.Y + speed * dt;
                    }
                }
                else if (ballPosition.Y < position.Y - 3 + texture.Height / 2)
                {
                    if (Global.ScreenCollider(new Vector2(position.X, (int)position.Y - 3), texture.Width, texture.Height, screenW, screenH))
                    {
                        position.Y = position.Y - speed * dt;
                    }
                }
            }
        }
    }
}
