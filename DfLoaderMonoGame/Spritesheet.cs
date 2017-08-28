using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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

	public class SpriteDef
	{
		public Rectangle Rect { get; set; }

		public SpriteDef(string name, Spritesheet spritesheet)
		{
			if (spritesheet.Rects.ContainsKey(name))
			{
				Rect = spritesheet.Rects[name];
			}
			else
			{
				Rect = new Rectangle();
			}
		}
	}
}
