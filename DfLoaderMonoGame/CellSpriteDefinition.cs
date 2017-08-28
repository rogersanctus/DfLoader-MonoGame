using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfLoader
{    
    public class CellSpriteDefinition
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public bool FlipH { get; set; }
        public bool FlipV { get; set; }
        public float Angle { get; set; }

        public CellSpriteDefinition()
        {
        }
    }
}
