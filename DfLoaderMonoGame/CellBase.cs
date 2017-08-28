
namespace DfLoader
{
    internal abstract class CellBase
    {
        public int Delay { get; set; }

        public CellBase(int delay)
        {
            Delay = delay;
        }

        public abstract void Dispose();
    }
}
