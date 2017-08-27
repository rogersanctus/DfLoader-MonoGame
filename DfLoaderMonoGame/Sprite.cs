using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.DfLoader
{
	public class Sprite
	{
		private Spritesheet spritesheet;
		public Vector2 pos;
		public int z;
		public double angle;
		public Vector2 center;
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

		public Sprite(string name, Spritesheet spritesheet, Vector2 pos = new Vector2())
		{
			this.spritesheet = spritesheet;
			this.pos = pos;
			this.center = new Vector2();
			this.angle = 0.0f;
			flipH = flipV = false;
			z = 0;

			if(spritesheet != null)
			{
				_def = new SpriteDef(name, spritesheet);
				center = new Vector2(_def.rect.Width / 2.0f, _def.rect.Height / 2.0f);
			}
		}

		public void Update(GameTime time)
		{

		}

		public void Draw( SpriteBatch batch)
		{
			if(spritesheet == null)
				return;
			if(spritesheet.tex == null)
				return;

            var effects = SpriteEffects.None;
            if (flipH) effects |= SpriteEffects.FlipHorizontally;
            if (flipV) effects |= SpriteEffects.FlipVertically;

            batch.Begin();			
			batch.Draw(spritesheet.tex, pos, _def.rect, Color.White, (float)angle, center, 1.0f, effects, 0.0f);
			batch.End();
		}
	}
}
