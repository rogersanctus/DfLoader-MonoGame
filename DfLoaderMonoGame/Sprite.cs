using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.DfLoader
{
	public class Sprite
	{
		private Spritesheet spritesheet;
		//private string name;
		public Microsoft.Xna.Framework.Vector2 pos;
		public int z;
		public double angle;
		public Microsoft.Xna.Framework.Vector2 center;
		public bool flipH;
		public bool flipV;
		private SpriteDef _def;

		public int width
		{
			get { return _def.rect.Width; }
		}

		public int height
		{
			get { return _def.rect.Height; }
		}

		public Sprite(string name, Spritesheet spritesheet, Microsoft.Xna.Framework.Vector2 pos = new Microsoft.Xna.Framework.Vector2())
		{
			this.spritesheet = spritesheet;
			//this.name = name;
			this.pos = pos;
			this.center = new Microsoft.Xna.Framework.Vector2();
			this.angle = 0.0f;
			flipH = flipV = false;
			z = 0;

			if(spritesheet != null)
			{
				_def = new SpriteDef(name, spritesheet);
				center = new Microsoft.Xna.Framework.Vector2(_def.rect.Width / 2.0f, _def.rect.Height / 2.0f);
			}
		}

		public void Update(Microsoft.Xna.Framework.GameTime time)
		{

		}

		public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
		{
			if(spritesheet == null)
				return;
			if(spritesheet.tex == null)
				return;

			batch.Begin();
			var effects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
			if (flipH) effects |= Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
			if (flipV) effects |= Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipVertically;
			batch.Draw(spritesheet.tex, pos, _def.rect, Microsoft.Xna.Framework.Color.White, (float)angle, center, 1.0f, effects, 0.0f);
			batch.End();
		}
	}
}
