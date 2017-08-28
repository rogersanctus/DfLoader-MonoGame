using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;

namespace DfContentPipeline
{
    public class AnimationsContent : ContentItem
    {
        private Dictionary<string, DfLoader.AnimationDefinition> anims;
        private string spritesheetFileName;

        public Dictionary<string, DfLoader.AnimationDefinition> Anims
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
