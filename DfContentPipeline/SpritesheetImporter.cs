using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace DfContentPipeline
{
    [ContentImporter(".sprites", DisplayName = "dFSpritesheet Importer", DefaultProcessor = "SpritesheetProcessor")]
    public class SpritesheetImporter : ContentImporter<SpritesheetContent>
    {

        public override SpritesheetContent Import(string filename, ContentImporterContext context)
        {

            XDocument xml = null;

            try
            {
                xml = XDocument.Load(filename);
            }
            catch(Exception e)
            {
                throw new IOException("Could not load the Spritesheet file. " + e.ToString());
            }

            SpritesheetContent spritesheetContent = new SpritesheetContent();
            var rects = new Dictionary<string, Rectangle>();

            try
            {

                var imgEle = xml.Element("img");

                if (imgEle == null)
                {
                    throw new NullReferenceException();
                }

                var nameAtt = imgEle.Attribute("name");
                var widthAtt = imgEle.Attribute("w");
                var heightAtt = imgEle.Attribute("h");

                if (nameAtt == null)
                {
                    throw new NullReferenceException();
                }

                spritesheetContent.TexName = (string)nameAtt;

                if (widthAtt != null)
                    spritesheetContent.Width = (int)widthAtt;
                if (heightAtt != null)
                    spritesheetContent.Height = (int)heightAtt;

                var definitionsEle = imgEle.Element("definitions");

                if (definitionsEle == null)
                {
                    throw new NullReferenceException();
                }

                MakeSpriteList(ref rects, definitionsEle.Element("dir"), "/");
            }
            catch (XmlException e)
            {
                throw new Exception("DfLoader XML parsing error. " + e.ToString());
            }

            spritesheetContent.Rects = rects;
            return spritesheetContent;
        }

        private void MakeSpriteList(ref Dictionary<string, Rectangle> rects, XElement el, string path)
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
                        MakeSpriteList(ref rects, d, path);    // Recursively make sprite list
                        path = oldPath;
                    }
                    catch (Exception e)
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
                        rects.Add(sprName, new Rectangle(x, y, w, h));
                    }
                    catch (Exception e)
                    {
                        throw new Exception("DfLoader error in Xml attribute. " + e.ToString());
                    }

                }
            }
        }

    }

}
