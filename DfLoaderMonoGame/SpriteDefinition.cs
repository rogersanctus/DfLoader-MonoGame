using Microsoft.Xna.Framework;

namespace DfLoader
{    
    public class SpriteDefinition
    {
        protected string name;
        protected Vector2 pos;
        protected bool flipH;
        protected bool flipV;
        protected float angle;

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
