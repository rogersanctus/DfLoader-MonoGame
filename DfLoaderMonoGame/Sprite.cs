﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DfLoader
{
	public class Sprite : SpriteBase
	{
		private Spritesheet spritesheet;
		private SpriteRectangle def;

        private bool effectFlipH;
        private bool effectFlipV;

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
            Name = name;
			this.spritesheet = spritesheet;
			Pos = pos;
            Origin = Vector2.Zero;
			TextureCenter = new Vector2();
			Angle = 0.0f;
			effectFlipH = effectFlipV = FlipH = FlipV = false;
			Z = 0;

			if(spritesheet != null)
			{
				def = new SpriteRectangle(name, spritesheet);
				TextureCenter = new Vector2(def.Rect.Width / 2.0f, def.Rect.Height / 2.0f);
			}
		}


        protected override void OnFlipHChanged()
        {
            var scale = Scale;
            scale.X = Math.Abs(scale.X);

            if (FlipH)
            {
                scale.X = -1.0f * scale.X;
            }

            Scale = scale;
            effectFlipH = FlipH;
        }

        protected override void OnFlipVChanged()
        {
            var scale = Scale;
            scale.Y = Math.Abs(scale.Y);

            if (FlipV)
            {
                scale.Y = -1.0f * scale.Y;
            }

            Scale = scale;
            effectFlipV = FlipV;
        }

        public override void UpdateTransform()
        {
            base.UpdateTransform();

            if (Parent != null)
            {
                transform = Transformation.Compose(Parent.Transform, transform);
                // If parent is flipped, flip child
                effectFlipH = Parent.FlipH ^ FlipH;
                effectFlipV = Parent.FlipV ^ FlipV;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);            
        }

		public override void Draw( SpriteBatch batch)
		{
			if(spritesheet == null)
				return;
			if(spritesheet.Tex == null)
				return;

            SpriteEffects effects = SpriteEffects.None;

            if (effectFlipH) effects |= SpriteEffects.FlipHorizontally;
            if (effectFlipV) effects |= SpriteEffects.FlipVertically;

            var scale = new Vector2( Math.Abs(transform.Scale.X), Math.Abs(transform.Scale.Y) );

            batch.Draw(spritesheet.Tex, transform.Position, def.Rect, Color, transform.Rotation, TextureCenter, scale, effects, Z);
		}
	}
}
