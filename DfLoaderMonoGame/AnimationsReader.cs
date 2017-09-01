using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;

namespace DfLoader
{
    public class AnimationsReader : ContentTypeReader<Animations>
    {
        protected override Animations Read(ContentReader input, Animations existingInstance)
        {
            var spritesheetFileName = input.ReadString();

            // Try to load the spritesheet
            Spritesheet spritesheet = null;
            try
            {
                var finalFileName = Path.GetFileNameWithoutExtension(spritesheetFileName);
                var relativePath = Utils.MakeRelativePathToFile(input.ContentManager.RootDirectory, input.AssetName, spritesheetFileName);

                if (relativePath.Length > 0)
                {
                    finalFileName = Path.Combine(relativePath, finalFileName);
                }

                spritesheet = input.ContentManager.Load<Spritesheet>(finalFileName);
            }
            catch (Exception e)
            {
                throw new Exception("DfLoader AnimationReader error. Coult not load spritesheet. " + e.ToString());
            }

            var animations = new Animations(spritesheet);

            var animsCount = input.ReadInt32();

            for (var i = 0; i < animsCount; i++)
            {
                var animName = input.ReadString();
                var animLoops = input.ReadInt32();

                var cellsCount = input.ReadInt32();

                var animation = new AnimationDefinition(animLoops);
                animations.Anims.Add(animName, animation);

                for (var j = 0; j < cellsCount; j++)
                {
                    var cellIndex = input.ReadInt32();
                    var cellDelay = input.ReadInt32();
                    var sprsCount = input.ReadInt32();

                    var cell = new CellDefinition(cellDelay);
                    animation.Cells.Add(cellIndex, cell);

                    for (var k = 0; k < sprsCount; k++)
                    {
                        var sprName = input.ReadString();
                        var sprX = input.ReadInt32();
                        var sprY = input.ReadInt32();
                        var sprZ = input.ReadInt32();
                        var sprFlipH = input.ReadBoolean();
                        var sprFlipV = input.ReadBoolean();
                        var sprAngle = input.ReadSingle();

                        cell.CellSprites.Add(new Sprite(sprName, spritesheet, new Vector2(sprX, sprY))
                        {
                            Z = sprZ,
                            FlipH = sprFlipH,
                            FlipV = sprFlipV,
                            Angle = sprAngle
                        }
                        );
                    }
                }
            }

            return animations;
        }
    }
}
