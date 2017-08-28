using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DfLoader
{
	public class Spritesheet
	{
		public Dictionary<string, Rectangle> Rects { get; set; }
		public string TexName { get; set; }
		public Texture2D Tex { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Spritesheet()
		{
		}
	}
}
