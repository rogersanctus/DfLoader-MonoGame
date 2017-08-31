using System.Collections.Generic;

namespace DfLoader
{
    internal class CellDefinition
    {
        public List<Sprite> CellSprites { get; set; }
        public int Delay { get; set; }

        public CellDefinition(int delay)
        {
            Delay = delay;
            CellSprites = new List<Sprite>();
        }

        public void Dispose()
        {
            CellSprites.Clear();
            CellSprites = null;
        }
    }
}
