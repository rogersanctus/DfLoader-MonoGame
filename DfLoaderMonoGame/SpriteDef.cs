using Microsoft.Xna.Framework;

namespace DfLoader
{
    internal class SpriteDef
    {
        public Rectangle Rect { get; set; }

        public SpriteDef(string name, Spritesheet spritesheet)
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
