using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DfLoader
{
	public class Sprite : SpriteBase
	{
		private Spritesheet spritesheet;
		private SpriteRectangle def;

		public int Width
		{
			get
            {
                if (spritesheet == null)
                    return -1;
                return def.Rect.Width;
            }
		}

		public int Height
		{
			get
            {
                if (spritesheet == null)
                    return -1;
                return def.Rect.Height;
            }
		}

		public Sprite(string name, Spritesheet spritesheet, Vector2 pos = new Vector2(), SpriteBase parent = null)
            : base(parent)
		{
            this.name = name;
			this.spritesheet = spritesheet;
			this.pos = pos;
            origin = Vector2.Zero;
			textureCenter = new Vector2();
			angle = 0.0f;
			flipH = flipV = false;
			Z = 0;

			if(spritesheet != null)
			{
				def = new SpriteRectangle(name, spritesheet);
				textureCenter = new Vector2(def.Rect.Width / 2.0f, def.Rect.Height / 2.0f);
			}
		}


        protected override void OnFlipHChanged()
        {
            scale.X = Math.Abs(scale.X);

            if (flipH)
            {
                scale.X = -1.0f * scale.X;
            }
        }

        protected override void OnFlipVChanged()
        {
            scale.Y = Math.Abs(scale.Y);

            if (flipV)
            {
                scale.Y = -1.0f * scale.Y;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (parent != null)
            {
                transform = Transformation.Compose(parent.Transform, transform);
            }
        }

		public override void Draw( SpriteBatch batch)
		{
			if(spritesheet == null)
				return;
			if(spritesheet.Tex == null)
				return;

            SpriteEffects effects = SpriteEffects.None;

            if (FlipH) effects |= SpriteEffects.FlipHorizontally;
            if (FlipV) effects |= SpriteEffects.FlipVertically;

            var scale = new Vector2( Math.Abs(transform.Scale.X), Math.Abs(transform.Scale.Y) );

            batch.Draw(spritesheet.Tex, transform.Position, def.Rect, color, transform.Rotation, textureCenter, scale, effects, Z);
			//batch.Draw(spritesheet.Tex, Pos, def.Rect, color, (float)Angle, TextureCenter, new Vector2(ScaleX, ScaleY), effects, (float)Z);
		}
	}
}
