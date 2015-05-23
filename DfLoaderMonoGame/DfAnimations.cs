using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DfLoader
{
	class DfAnimations
	{
		public Dictionary<string, DfAnimationDef> anims;
		public DfSpritesheet spritesheet { get; internal set; }

		public DfAnimations(string xmlStr, Microsoft.Xna.Framework.Content.ContentManager content, DfSpritesheet spritesheet = null)
		{
			anims = new Dictionary<string, DfAnimationDef>();

			this.spritesheet = spritesheet;

			try
			{
				var xml = XDocument.Parse(xmlStr);

				var animationsEle = xml.Element("animations");

				if (animationsEle == null)
				{
					System.Console.WriteLine("Error. Invalid animation XML file. Expected animations tag.");
					return;
				}

				if (this.spritesheet == null)
				{
					/* Try to open the spritesheet file */
					try
					{
						if (animationsEle.Attribute("spriteSheet") != null)
						{
							var fileName = (string)animationsEle.Attribute("spriteSheet");
							var path = System.IO.Path.Combine(content.RootDirectory, fileName);
							System.IO.Stream s = Microsoft.Xna.Framework.TitleContainer.OpenStream(path);
							var reader = new System.IO.StreamReader(s);
							this.spritesheet = new DfSpritesheet(reader.ReadToEnd(), content);
						}
					}
					catch (System.IO.IOException e)
					{
						System.Console.WriteLine(e.ToString());
						return;
					}
				}

				foreach (XElement anim in animationsEle.Elements("anim"))
				{
					var animName = (string)anim.Attribute("name");
					var loops = (int)anim.Attribute("loops");

					DfAnimationDef animDef = new DfAnimationDef(loops);
					anims.Add(animName, animDef);

					foreach (XElement cellNode in anim.Elements("cell"))
					{
						int index = 0, delay = 0;
						var indexAtt = cellNode.Attribute("index");
						var delayAtt = cellNode.Attribute("delay");						

						if(indexAtt != null)
							index = (int)indexAtt;
						if(delayAtt != null)
							delay = (int)delayAtt;

						DfCell cell = new DfCell(delay);
						animDef.cells.Add(index, cell);

						foreach (XElement sprNode in cellNode.Elements("spr"))
						{
							var sprName = (string)sprNode.Attribute("name");
							var x = (int)sprNode.Attribute("x");
							var y = (int)sprNode.Attribute("y");
							var z = (int)sprNode.Attribute("z");

							bool flipH = false, flipV = false;
							float angle = 0.0f;

							if (sprNode.Attribute("flipH") != null)
								flipH = (bool)sprNode.Attribute("flipH");
							if (sprNode.Attribute("flipV") != null)
								flipV = (bool)sprNode.Attribute("flipV");
							if (sprNode.Attribute("angle") != null)
								angle = (float)sprNode.Attribute("angle");

							angle *= (float)(Math.PI / 180); // Convert angle from degrees to radians

							var spr = new DfSprite(sprName, this.spritesheet, new Microsoft.Xna.Framework.Vector2(x, y));
							spr.z = z;
							spr.flipH = flipH;
							spr.flipV = flipV;
							spr.angle = angle;
							cell.cell_sprs.Add(sprName, spr);							
						}

						//Order the sprite list for the current cell
						cell.cell_sprs.OrderBy(x => x.Value.z);
					}
				}

			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.ToString());
				return;
			}
		}
	}

	class DfAnimationDef
	{
		public int loops;
		public SortedList<int, DfCell> cells;

		public DfAnimationDef(int loops)
		{
			cells = new SortedList<int, DfCell>();
			this.loops = loops;
		}
	}

	class DfCell
	{
		public int delay;
		//public Dictionary<string, DfCellSprite> cell_sprs;
		public Dictionary<string, DfSprite> cell_sprs;

		public DfCell(int delay)
		{
			this.delay = delay;
			//cell_sprs = new Dictionary<string, DfCellSprite>();
			cell_sprs = new Dictionary<string, DfSprite>();
		}
	}	
}
