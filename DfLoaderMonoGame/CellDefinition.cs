using System.Collections.Generic;

namespace DfLoader
{
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
}
