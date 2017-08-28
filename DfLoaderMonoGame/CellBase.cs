
namespace DfLoader
{
    public abstract class CellBase
    {
        public int Delay { get; set; }

        public CellBase(int delay)
        {
            Delay = delay;
        }

        internal abstract void Dispose();
    }
}
