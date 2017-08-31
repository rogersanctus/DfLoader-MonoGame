using System.Collections.Generic;

namespace DfLoader
{
    internal class AnimationDefinition
    {
        public int Loops { get; set; }
        public SortedList<int, CellDefinition> Cells { get; set; }

        public AnimationDefinition(int loops)
        {
            Cells = new SortedList<int, CellDefinition>();
            Loops = loops;
        }

        public void Dispose()
        {
            foreach (var cell in Cells)
            {
                cell.Value.Dispose();
            }

            Cells.Clear();
        }
    }
}
