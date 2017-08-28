using Microsoft.Xna.Framework.Content.Pipeline;

namespace DfContentPipeline
{
    [ContentProcessor(DisplayName = "dFAnimations")]
    public class AnimationsProcessor : ContentProcessor<AnimationsContent, AnimationsContent>
    {
        public override AnimationsContent Process(AnimationsContent input, ContentProcessorContext context)
        {
            return input;
        }
    }
}
