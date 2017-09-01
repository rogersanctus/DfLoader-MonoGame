using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DfLoader
{
    public abstract class SpriteBase : SpriteDefinition
    {
        private SpriteBase parent;
        private Vector2 origin;
        private Vector2 textureCenter;
        private Vector2 scale;
        private Color color;

        public SpriteBase Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                OnParentChanged();
            }
        }

        public Vector2 Origin
        {
            get { return origin;  }
            set
            {
                origin = value;
                OnOriginChanged();
            }
        }

        public Vector2 TextureCenter
        {
            get { return textureCenter; }
            set
            {
                textureCenter = value;
                OnTextureCenterChanged();
            }
        }

        public Vector2 Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                OnScaleChanged();
            }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                OnColorChanged();
            }
        }

        protected Transformation transform;

        public Transformation Transform
        {
            get { return transform; }
        }

        public SpriteBase( SpriteBase parent = null )
        {
            origin = Vector2.Zero;
            this.parent = parent;
            scale = Vector2.One;
            color = Color.White;

            transform = Transformation.Identity;
        }

        protected virtual void OnParentChanged()
        {

        }

        protected virtual void OnOriginChanged()
        {

        }

        protected virtual void OnTextureCenterChanged()
        {

        }

        protected virtual void OnScaleChanged()
        {

        }

        protected virtual void OnColorChanged()
        {

        }

        public virtual void Update(GameTime time)
        {
            var radAngle = MathHelper.ToRadians(angle);
            transform.Origin = origin;
            transform.Position = pos;
            transform.Scale = scale;
            transform.Rotation = radAngle;
            transform.ApplyMatrix();

            // Update the transform position with the new offseted by origin position
            transform.Position = Vector2.Transform(Vector2.Zero, transform.GetMatrix());
        }

        public virtual void Draw(SpriteBatch batch)
        {

        }
    }
}
