using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace DfLoader
{
    public class SpritesheetReader : ContentTypeReader<Spritesheet>
    {

        protected override Spritesheet Read(ContentReader input, Spritesheet existingInstance)
        {
            var spritesheet = new Spritesheet();

            var texName = input.ReadString();
            var texWidth = input.ReadInt32();
            var texHeight = input.ReadInt32();

            spritesheet.TexName = texName;
            spritesheet.Width = texWidth;
            spritesheet.Height = texHeight;

            var rectsCount = input.ReadInt32();
            spritesheet.Rects = new Dictionary<string, Rectangle>();

            for (int i = 0; i < rectsCount; ++i)
            {
                var rectName = input.ReadString();
                var x = input.ReadInt32();
                var y = input.ReadInt32();
                var width = input.ReadInt32();
                var height = input.ReadInt32();

                spritesheet.Rects.Add(rectName, new Rectangle(x, y, width, height));
            }

            var finalFileName = Path.GetFileNameWithoutExtension(texName);
            var relativePath = Utils.MakeRelativePathToFile(input.ContentManager.RootDirectory, input.AssetName, texName);

            if (relativePath.Length > 0)
            {
                finalFileName = Path.Combine(relativePath, finalFileName);
            }

            spritesheet.Tex = input.ContentManager.Load<Texture2D>(finalFileName);

            return spritesheet;
        }
    }
}
