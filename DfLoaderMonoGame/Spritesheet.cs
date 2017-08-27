using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.DfLoader
{
	public class Spritesheet
	{
		public Dictionary<string, Rectangle> Rects { get; set; }
		public string TexName { get; set; }
		public Texture2D Tex { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Spritesheet(string xmlStr, ContentManager content)
		{			
			Rects = new Dictionary<string, Rectangle>();
			Width = 0;
			Height = 0;

			try
			{
				var xml = XDocument.Parse(xmlStr);

				var imgEle = xml.Element("img");

				if (imgEle == null)
					throw new NullReferenceException();

				var nameAtt = imgEle.Attribute("name");
				var widthAtt = imgEle.Attribute("w");
				var heightAtt = imgEle.Attribute("h");

				if(nameAtt == null)
				{
					throw new NullReferenceException();
				}

				TexName = (string)nameAtt;

				if(widthAtt != null)
					Width = (int)widthAtt;
				if(heightAtt != null)
					Height = (int)heightAtt;

				if(TexName.Contains("."))
				{
					char[] sep = { '.' };
					TexName = TexName.Split(sep, StringSplitOptions.RemoveEmptyEntries)[0];
				}

				Tex = content.Load<Texture2D>(TexName);

				var definitionsEle = imgEle.Element("definitions");

                if (definitionsEle == null)
                {
                    throw new NullReferenceException();
                }

				MakeSpriteList(definitionsEle.Element("dir"), "/");
			}
			catch (XmlException e)
			{
				throw new Exception("DfLoader XML parsing error. " + e.ToString());
			}
		}

		private void MakeSpriteList(XElement el, string path)
		{
			foreach (XElement d in el.Nodes())
			{
				string oldPath;

				if (d.Name == "dir")
				{
					try
					{
						oldPath = path;
						path += ((string)d.Attribute("name")) + "/";
						MakeSpriteList(d, path);    // Recursively make sprite list
						path = oldPath;
					}
					catch(Exception e)
					{
                        throw new Exception("DfLoader error in Xml attribute. " + e.ToString());
					}
				}
				else if (d.Name == "spr")
				{
					try
					{
						var sprName = path + (string)d.Attribute("name");
						var x = (int)d.Attribute("x");
						var y = (int)d.Attribute("y");
						var w = (int)d.Attribute("w");
						var h = (int)d.Attribute("h");

						//SpriteDef spr = new SpriteDef(new Rectangle(x, y, w, h));
						Rects.Add(sprName, new Rectangle(x, y, w, h));
					}
					catch (Exception e)
					{						
                        throw new Exception("DfLoader error in Xml attribute. " + e.ToString());
					}

				}
			}
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
