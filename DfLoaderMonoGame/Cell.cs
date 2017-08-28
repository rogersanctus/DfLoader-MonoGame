using System.Collections.Generic;

namespace DfLoader
{
    internal class Cell : CellBase
    {
        public Dictionary<string, Sprite> CellSprites { get; set; }

        public Cell(int delay) : base(delay)
        {
            CellSprites = new Dictionary<string, Sprite>();
        }

        public override void Dispose()
        {
            CellSprites.Clear();
            CellSprites = null;
        }
    }
}
