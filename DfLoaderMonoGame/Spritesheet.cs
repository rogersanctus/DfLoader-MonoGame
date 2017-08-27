using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.DfLoader
{
	public class Spritesheet
	{
		public Dictionary<string, Rectangle> rects;
		public string texName;
		public Texture2D tex;
		public int width;
		public int height;

		public Spritesheet(string xmlStr, ContentManager content)
		{			
			rects = new Dictionary<string, Rectangle>();
			width = 0;
			height = 0;

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

				texName = (string)nameAtt;

				if(widthAtt != null)
					width = (int)widthAtt;
				if(heightAtt != null)
					height = (int)heightAtt;

				if(texName.Contains("."))
				{
					char[] sep = { '.' };
					texName = texName.Split(sep, StringSplitOptions.RemoveEmptyEntries)[0];
				}
				tex = content.Load<Texture2D>(texName);

				var definitionsEle = imgEle.Element("definitions");

				if(definitionsEle == null)
					throw new NullReferenceException();

				MakeSpriteList(definitionsEle.Element("dir"), "/");
			}
			catch (XmlException e)
			{
				System.Console.WriteLine(e.ToString());
				throw;
			}
		}

		private void MakeSpriteList(XElement e, string path)
		{
			foreach (XElement d in e.Nodes())
			{
				string oldPath;

				if (d.Name == "dir")
				{
					try
					{
						oldPath = path;
						path += ((string)d.Attribute("name")) + "/";
						MakeSpriteList(d, path);
						path = oldPath;
					}
					catch(Exception exception)
					{
						System.Console.WriteLine(exception.ToString());
						return;
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
						rects.Add(sprName, new Rectangle(x, y, w, h));
					}
					catch (Exception exception)
					{						
                        throw exception;						
					}

				}
			}
		}
	}

	public class SpriteDef
	{
		public Microsoft.Xna.Framework.Rectangle rect { get; set; }

		public SpriteDef(string name, Spritesheet spritesheet)
		{
			if (spritesheet.rects.ContainsKey(name))
			{
				rect = spritesheet.rects[name];
			}
			else
			{
				rect = new Microsoft.Xna.Framework.Rectangle();
			}
		}
	}
}
