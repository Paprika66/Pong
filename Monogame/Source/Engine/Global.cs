using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame
{
    internal static class Global
    {
        public static bool ScreenCollider(Vector2 nextPosition, int targetWidth, int targetHeight, int screenW, int screenH)
        {
            if (nextPosition.X + targetWidth > screenW) { return false; }
            else if (nextPosition.X < 0) { return false; }
            else if (nextPosition.Y + targetHeight > screenH) { return false; }
            else if (nextPosition.Y < 0) { return false; }
            return true;
        }
        public static bool ScreenColliderVertical(float nextPosition, int targetHeight, int screenH)
        {
            if(nextPosition < 0 ) { return true; }
            else if (nextPosition + targetHeight > screenH) { return true; }
            return false;
        }
        public static bool ScreenColliderHorizontal(float nextPosition, int targetWidth, int screenW)
        {
            if (nextPosition < 0) { return true; }
            else if (nextPosition + targetWidth > screenW) { return true; }
            return false;
        }
    }
}
