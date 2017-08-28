using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace DfContentPipeline
{
    [ContentTypeWriter]
    class SpritesheetWriter : ContentTypeWriter<SpritesheetContent>
    {
        protected override void Write( ContentWriter output, SpritesheetContent value )
        {
            if(value == null)
            {
                throw new ArgumentNullException("SpritesheetWriter: SpritesheetContent is null.");
            }

            output.Write(value.TexName);
            output.Write(value.Width);
            output.Write(value.Height);
            
            output.Write(value.Rects.Count);
            foreach( var r in value.Rects )
            {
                output.Write(r.Key);
                output.Write(r.Value.X);
                output.Write(r.Value.Y);
                output.Write(r.Value.Width);
                output.Write(r.Value.Height);
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(DfLoader.Spritesheet).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            
            // Change "Writer" in this class name to "Reader" and use the runtime type namespace and assembly
            var readerClassName = GetType().Name.Replace("Writer", "Reader");

            // From looking at XNA-produced XNBs, it appears built-in
            // type readers don't need assembly qualification.
            var readerNamespace = typeof(SpritesheetReader).Namespace;
            return readerNamespace + "." + readerClassName + ", " + readerNamespace;
        }
    }
}
