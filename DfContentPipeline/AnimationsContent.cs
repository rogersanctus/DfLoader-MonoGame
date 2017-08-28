using DfLoader;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;

namespace DfContentPipeline
{
    public class AnimationsContent : ContentItem
    {
        private Dictionary<string, AnimationDefinition> anims;
        private string spritesheetFileName;

        internal Dictionary<string, AnimationDefinition> Anims
        {
            get { return anims; }
            set { anims = value; }
        }

        public string SpritesheetFileName
        {
            get { return spritesheetFileName; }
            set { spritesheetFileName = value; }
        }

        public AnimationsContent()
        {

        }
    }
}
