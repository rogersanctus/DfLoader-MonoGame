using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DfLoaderMonoGame
{
	public class DfSpritesheet
	{
		public Dictionary<string, Microsoft.Xna.Framework.Rectangle> rects;
		public string texName;
		public Microsoft.Xna.Framework.Graphics.Texture2D tex;
		public int width;
		public int height;

		public DfSpritesheet(string xmlStr, Microsoft.Xna.Framework.Content.ContentManager content)
		{			
			rects = new Dictionary<string, Microsoft.Xna.Framework.Rectangle>();
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
				tex = content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(texName);

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

						//DfSpriteDef spr = new DfSpriteDef(new Microsoft.Xna.Framework.Rectangle(x, y, w, h));
						rects.Add(sprName, new Microsoft.Xna.Framework.Rectangle(x, y, w, h));
					}
					catch (Exception exception)
					{
						System.Console.WriteLine(exception.ToString());
						return;
					}

				}
			}
		}
	}

	public class DfSpriteDef
	{
		public Microsoft.Xna.Framework.Rectangle rect { get; set; }

		public DfSpriteDef(string name, DfSpritesheet spritesheet)
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
