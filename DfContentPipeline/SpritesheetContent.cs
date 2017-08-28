using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;

namespace DfContentPipeline
{
    public class SpritesheetContent : ContentItem
    {
        public Dictionary<string, Rectangle> Rects { get; set; }
        public string TexName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public SpritesheetContent()
        {

        }

        ~SpritesheetContent()
        {
            Rects.Clear();
            Rects = null;
        }

    }
}
