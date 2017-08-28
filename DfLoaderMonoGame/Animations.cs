using System;
using System.Collections.Generic;

namespace DfLoader
{
	public class Animations
	{
        private Dictionary<string, AnimationDefinition> anims;
        private Spritesheet spritesheet;

        internal Dictionary<string, AnimationDefinition> Anims
        {
            get
            {
                return anims;
            }

            set
            {
                if (anims != null)
                {
                    foreach (var anim in anims)
                    {
                        anim.Value.Dispose();
                    }

                    anims.Clear();
                }

                anims = value;
            }
        }

        public Spritesheet GetSpritesheet()
        {
            return spritesheet;
        }

		public Animations(Spritesheet spritesheet)
		{
            this.spritesheet = spritesheet;
            Anims = new Dictionary<string, AnimationDefinition>();

            if(spritesheet == null)
            {
                throw new ArgumentNullException("DfLoader Animations error. spritesheet can not be null.");
            }
        }        
	}
}
