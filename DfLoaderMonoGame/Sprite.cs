using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.DfLoader
{
	public class Sprite
	{
		private Spritesheet spritesheet;
		public Vector2 Pos { get; set; }
		public int Z { get; set; }
		public double Angle { get; set; }
		public Vector2 Center { get; set; }
		public bool FlipH { get; set; }
		public bool FlipV { get; set; }
		private SpriteDef def;

		public int Width
		{
			get { return def.Rect.Width; }
		}

		public int Height
		{
			get { return def.Rect.Height; }
		}

		public Sprite(string name, Spritesheet spritesheet, Vector2 pos = new Vector2())
		{
			this.spritesheet = spritesheet;
			this.Pos = pos;
			this.Center = new Vector2();
			this.Angle = 0.0f;
			FlipH = FlipV = false;
			Z = 0;

			if(spritesheet != null)
			{
				def = new SpriteDef(name, spritesheet);
				Center = new Vector2(def.Rect.Width / 2.0f, def.Rect.Height / 2.0f);
			}
		}

		public void Update(GameTime time)
		{

		}

		public void Draw( SpriteBatch batch)
		{
			if(spritesheet == null)
				return;
			if(spritesheet.Tex == null)
				return;

            var effects = SpriteEffects.None;
            if (FlipH) effects |= SpriteEffects.FlipHorizontally;
            if (FlipV) effects |= SpriteEffects.FlipVertically;

            batch.Begin();			
			batch.Draw(spritesheet.Tex, Pos, def.Rect, Color.White, (float)Angle, Center, 1.0f, effects, 0.0f);
			batch.End();
		}
	}
}
