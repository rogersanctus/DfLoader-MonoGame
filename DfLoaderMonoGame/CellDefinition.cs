using System.Collections.Generic;

namespace DfLoader
{
    internal class CellDefinition : CellBase
    {
        public List<CellSpriteDefinition> CellSprites { get; set; }

        public CellDefinition(int delay) : base(delay)
        {
            CellSprites = new List<CellSpriteDefinition>();
        }

        public override void Dispose()
        {
            CellSprites.Clear();
            CellSprites = null;
        }
    }
}
