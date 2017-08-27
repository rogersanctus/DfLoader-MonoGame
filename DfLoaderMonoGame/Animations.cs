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
		public Dictionary<string, AnimationDef> Anims { get; set; }
        private Spritesheet spritesheet;

        public Spritesheet GetSpritesheet()
        {
            return spritesheet;
        }

		public Animations(string xmlStr, ContentManager content, Spritesheet spritesheet = null)
		{
			Anims = new Dictionary<string, AnimationDef>();

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
					Anims.Add(animName, animDef);

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
						animDef.Cells.Add(index, cell);

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

                            angle *= (float)(Math.PI / 180); // Convert Angle from degrees to radians

                            var spr = new Sprite(sprName, this.spritesheet, new Vector2(x, y))
                            {
                                Z = z,
                                FlipH = flipH,
                                FlipV = flipV,
                                Angle = angle
                            };

							cell.CellSprites.Add(sprName, spr);							
						}

						//Order the sprite list for the current cell sprites by their Z order
						cell.CellSprites.OrderBy(x => x.Value.Z);
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
		public int Loops { get; set; }
		public SortedList<int, Cell> Cells { get; set; }

		public AnimationDef(int loops)
		{
			Cells = new SortedList<int, Cell>();
			this.Loops = loops;
		}
	}

	public class Cell
	{
		public int Delay { get; set; }		
		public Dictionary<string, Sprite> CellSprites { get; set; }

		public Cell(int delay)
		{
			Delay = delay;			
			CellSprites = new Dictionary<string, Sprite>();
		}
	}	
}
