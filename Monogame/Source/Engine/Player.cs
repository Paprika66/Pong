using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace Monogame
{
    internal class Player
    {
        protected Vector2 position;
        protected Texture2D texture;
        public float speed = 400f;
        public int score;
        public Vector2 Position { get { return position; } }
        public Texture2D Texture { get { return texture; } }
        public Rectangle BoundingBox { get { return new Rectangle((int)position.X, (int)position.Y,texture.Width,texture.Height); } }
        public Player(int screenW, int screenH, SpriteBatch s, Texture2D texture)
        {
            position = new Vector2(20, screenH / 2);
            this.texture = texture;
        }
        public Player(SpriteBatch s, Texture2D texture)
        {
            this.texture = texture;
        }
   

        public virtual void Move(int screenW, int screenH, Vector2 ballPosition, GameTime gameTime, bool isPlaying, bool single)
        {

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (isPlaying )
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (Global.ScreenCollider(new Vector2(position.X, position.Y - 3), texture.Width, texture.Height, screenW, screenH)) { position.Y = position.Y - speed * dt; }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
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
        public virtual void Update()
        {
            
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}

