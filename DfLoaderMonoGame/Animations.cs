using System;
using System.Collections.Generic;

namespace DfLoader
{
	public class Animations
	{
        private Dictionary<string, AnimationDefinition> anims;
        private Spritesheet spritesheet;

        public Dictionary<string, AnimationDefinition> Anims
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

        public Spritesheet Spritesheet
        {
            get
            {
                return spritesheet;
            }
        }

        public Spritesheet GetSpritesheet()
        {
            return spritesheet;
        }

		public Animations(Spritesheet spritesheet)
		{
            this.spritesheet = spritesheet;
            Anims = new Dictionary<string, DfLoader.AnimationDefinition>();

            if(spritesheet == null)
            {
                throw new ArgumentNullException("DfLoader Animations error. spritesheet can not be null.");
            }
        }        
	}

	public class AnimationDefinition
	{
		public int Loops { get; set; }
		public SortedList<int, CellBase> Cells { get; set; }

		public AnimationDefinition(int loops)
		{
			Cells = new SortedList<int, CellBase>();
			this.Loops = loops;
		}

        internal void Dispose()
        {
            foreach(var cell in Cells)
            {
                cell.Value.Dispose();
            }

            Cells.Clear();
        }
    }

    public abstract class CellBase
    {
        public int Delay { get; set; }

        public CellBase(int delay)
        {
            Delay = delay;
        }

        internal abstract void Dispose();
    }

	public class CellDefinition : CellBase
	{
        public List<CellSpriteDefinition> CellSprites { get; set; }

        public CellDefinition(int delay) : base(delay)
		{            
            CellSprites = new List<CellSpriteDefinition>();
		}

        internal override void Dispose()
        {
            CellSprites.Clear();
            CellSprites = null;
        }
    }

    public class Cell : CellBase
    {
        public Dictionary<string, Sprite> CellSprites { get; set; }        

        public Cell(int delay) : base(delay)
        {
            CellSprites = new Dictionary<string, Sprite>();
        }

        internal override void Dispose()
        {
            CellSprites.Clear();
            CellSprites = null;
        }
    }
}
