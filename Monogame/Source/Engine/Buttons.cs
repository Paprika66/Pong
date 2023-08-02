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

namespace Monogame
{
    class Buttons
    {
        private Texture2D texture;
        private bool isHovering;

        private MouseState currentMouse;
        private MouseState previousMouse;
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }
        public Buttons(Texture2D texture)
        {
            this.texture = texture;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            if (isHovering)
            {
                color = Color.White;
            }
            else
            {
                color = Color.Gray;
            }
            spriteBatch.Draw(texture, Position, color);
        }
        public bool Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
