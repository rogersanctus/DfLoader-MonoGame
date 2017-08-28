using System.Collections.Generic;

namespace DfLoader
{
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
            foreach (var cell in Cells)
            {
                cell.Value.Dispose();
            }

            Cells.Clear();
        }
    }
}
