using Microsoft.Xna.Framework;

namespace DfLoader
{
    internal class SpriteRectangle
    {
        public Rectangle Rect { get; set; }

        public SpriteRectangle(string name, Spritesheet spritesheet)
        {
            if (spritesheet.Rects.ContainsKey(name))
            {
                Rect = spritesheet.Rects[name];
            }
            else
            {
                Rect = new Rectangle();
            }
        }
    }
}
