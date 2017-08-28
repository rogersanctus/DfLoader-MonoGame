using Microsoft.Xna.Framework.Content.Pipeline;

namespace DfContentPipeline
{
    [ContentProcessor(DisplayName = "dFSpritesheet")]
    public class SpritesheetProcessor : ContentProcessor<SpritesheetContent, SpritesheetContent>
    {
        public override SpritesheetContent Process(SpritesheetContent input, ContentProcessorContext context)
        {
            return input;
        }
    }
}