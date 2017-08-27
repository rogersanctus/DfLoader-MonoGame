using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.DfLoader
{
	public class Animations
	{
		public Dictionary<string, AnimationDef> anims;
		public Spritesheet spritesheet { get; internal set; }

		public Animations(string xmlStr, ContentManager content, Spritesheet spritesheet = null)
		{
			anims = new Dictionary<string, AnimationDef>();

			this.spritesheet = spritesheet;

			try
			{
				var xml = XDocument.Parse(xmlStr);

				var animationsEle = xml.Element("animations");

				if (animationsEle == null)
				{
                    throw new Exception("DfLoader Error. Invalid animation XML file. Expected animations tag.");
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
							System.IO.Stream s = TitleContainer.OpenStream(path);
							var reader = new System.IO.StreamReader(s);
							this.spritesheet = new Spritesheet(reader.ReadToEnd(), content);
						}
					}
					catch (System.IO.IOException e)
					{
						throw new Exception("DfLoader error. " + e.ToString());
					}
				}

				foreach (XElement anim in animationsEle.Elements("anim"))
				{
					var animName = (string)anim.Attribute("name");
					var loops = (int)anim.Attribute("loops");

					AnimationDef animDef = new AnimationDef(loops);
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

						Cell cell = new Cell(delay);
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

							var spr = new Sprite(sprName, this.spritesheet, new Vector2(x, y));
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
				throw new Exception("DfLoader Error. " + e.ToString());
			}
		}
	}

	public class AnimationDef
	{
		public int loops;
		public SortedList<int, Cell> cells;

		public AnimationDef(int loops)
		{
			cells = new SortedList<int, Cell>();
			this.loops = loops;
		}
	}

	public class Cell
	{
		public int delay;		
		public Dictionary<string, Sprite> cell_sprs;

		public Cell(int delay)
		{
			this.delay = delay;			
			cell_sprs = new Dictionary<string, Sprite>();
		}
	}	
}
