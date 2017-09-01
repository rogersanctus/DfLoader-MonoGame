using Microsoft.Xna.Framework;

namespace DfLoader
{    
    public class SpriteDefinition
    {
        private string name;
        private Vector2 pos;
        private bool flipH;
        private bool flipV;
        private float angle;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnNameChanged();
            }
        }

        public Vector2 Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
                OnPosChanged();
            }
        }

        public int Z { get; set; }

        public bool FlipH
        {
            get
            {
                return flipH;
            }

            set
            {
                flipH = value;
                OnFlipHChanged();
            }
        }

        public bool FlipV
        {
            get
            {
                return flipV;
            }

            set
            {
                flipV = value;
                OnFlipVChanged();
            }
        }

        public float Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
                OnAngleChanged();
            }
        }

        protected virtual void OnNameChanged()
        {

        }

        protected virtual void OnPosChanged()
        {

        }

        protected virtual void OnFlipHChanged()
        {

        }

        protected virtual void OnFlipVChanged()
        {

        }

        protected virtual void OnAngleChanged()
        {

        }

        public SpriteDefinition()
        {

        }
    }
}
