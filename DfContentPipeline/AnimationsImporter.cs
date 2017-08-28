
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace DfContentPipeline
{
    [ContentImporter(".anim", DisplayName = "dFAnimations Importer", DefaultProcessor = "AnimationsProcessor")]
    public class AnimationsImporter : ContentImporter<AnimationsContent>
    {        
        public override AnimationsContent Import(string filename, ContentImporterContext context)
        {

            var animationContent = new AnimationsContent();

            try
            {
                var xml = XDocument.Load(filename);

                processAnimXml(ref animationContent, xml);
            }
            catch( Exception e)
            {
                throw new IOException("Could not load the Animation file. " + e.ToString());
            }

            return animationContent;
        }

        private void processAnimXml( ref AnimationsContent input, XDocument xml )
        {
            input.Anims = new Dictionary<string, DfLoader.AnimationDefinition>();            

            try
            {
                var animationsEle = xml.Element("animations");

                if (animationsEle == null)
                {
                    throw new Exception("DfLoader AnimationImporter Error. Invalid animation XML file. Expected animations tag.");
                }

                
                if (animationsEle.Attribute("spriteSheet") == null)
                {
                    throw new Exception("DfLoader Error. Invalid animation XML file. Expected spritesheet attibute.");
                }

                input.SpritesheetFileName = (string)animationsEle.Attribute("spriteSheet");

                foreach (XElement anim in animationsEle.Elements("anim"))
                {
                    var animName = (string)anim.Attribute("name");
                    var loops = (int)anim.Attribute("loops");

                    var animDef = new DfLoader.AnimationDefinition(loops);
                    input.Anims.Add(animName, animDef);

                    foreach (XElement cellNode in anim.Elements("cell"))
                    {
                        int index = 0, delay = 0;
                        var indexAtt = cellNode.Attribute("index");
                        var delayAtt = cellNode.Attribute("delay");

                        if (indexAtt != null)
                            index = (int)indexAtt;
                        if (delayAtt != null)
                            delay = (int)delayAtt;

                        var cell = new DfLoader.CellDefinition(delay);
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
                            var cellspr = new DfLoader.CellSpriteDefinition()
                            {
                                Name = sprName,
                                X = x,
                                Y = y,
                                Z = z,
                                FlipH = flipH,
                                FlipV = flipV,
                                Angle = angle
                            };

                            cell.CellSprites.Add(cellspr);
                        }

                        //Order the sprite list for the current cell sprites by their Z order
                        cell.CellSprites.Sort( (a,b) => a.Z.CompareTo(b.Z) );
                        //cell.CellSprites.OrderBy(x => x.Z);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("DfLoader AnimationImporter error. " + e.ToString());
            }
        }
    }
}
