using DfLoader;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;

namespace DfContentPipeline
{
    [ContentTypeWriter]
    public class AnimationsWriter : ContentTypeWriter<AnimationsContent>
    {

        protected override void Write(ContentWriter output, AnimationsContent value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("AnimationWriter: AnimationContent is null.");
            }

            output.Write(value.SpritesheetFileName);
            output.Write(value.Anims.Count);

            foreach(var anim in value.Anims)
            {
                output.Write(anim.Key);
                output.Write(anim.Value.Loops);
                output.Write(anim.Value.Cells.Count);

                foreach (var cell in anim.Value.Cells)
                {
                    output.Write(cell.Key);
                    output.Write(cell.Value.Delay);                    

                    var cellDef = (CellDefinition)cell.Value;
                    output.Write(cellDef.CellSprites.Count);

                    foreach(var spr in cellDef.CellSprites)
                    {
                        output.Write(spr.Name);
                        output.Write(spr.X);
                        output.Write(spr.Y);
                        output.Write(spr.Z);
                        output.Write(spr.FlipH);
                        output.Write(spr.FlipV);
                        output.Write(spr.Angle);
                    }
                }
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(Animations).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "DfLoader.AnimationsReader, DfLoader";
        }
    }
}
