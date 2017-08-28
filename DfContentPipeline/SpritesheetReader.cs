using DfLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace DfContentPipeline
{
    class SpritesheetReader : ContentTypeReader<Spritesheet>
    {

        private static string[] textureExtensions = 
        {
            // Add more if supported by DarkFunction and MonoGame texture importer
            ".png", ".tga", ".tiff", ".bmp"
        };

        public static string[] TextureExtensions {
            get
            {
                return textureExtensions;
            }
        }

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

            for ( int i = 0; i < rectsCount; ++i )
            {
                var rectName = input.ReadString();
                var x = input.ReadInt32();
                var y = input.ReadInt32();
                var width = input.ReadInt32();
                var height = input.ReadInt32();

                spritesheet.Rects.Add(rectName, new Rectangle(x, y, width, height));
            }

            var texFile = Path.GetFileNameWithoutExtension(texName);           
            var relativePath = Utils.MakeRelativePathToFile(input.AssetName, texName);

            spritesheet.Tex = input.ContentManager.Load<Texture2D>(relativePath + Path.DirectorySeparatorChar + texFile);

            return spritesheet;
        }
    }
}
