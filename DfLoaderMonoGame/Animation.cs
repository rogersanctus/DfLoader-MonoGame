using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DfLoader
{
	public class Animation
	{
		private AnimationDefinition current;
		private int currentFrame;
		private Animations animations;
		private bool playing;
		private string name;
		private double time;
		private double timeNext;
		private bool reverse;
		private bool pingpong;
		private int loops;

		public Vector2 Pos { get; set; }

		public const string NO_ANIM = "None";

		public Animation(Animations animations, Vector2 pos = new Vector2())
		{
			name = NO_ANIM;				
			this.animations = animations;
			currentFrame = 0;
			current = null;
			playing = false;
			this.Pos = pos;
			time = 0.0;
			timeNext = 0.0;
			pingpong = reverse = false;
			loops = 0;
		}

		public string Name
		{
			get { return name; }

			set
			{
				name = value;

				if(animations.Anims.ContainsKey(name))
				{
					current = animations.Anims[name];
					timeNext = current.Cells[currentFrame].Delay;
					loops = current.Loops;
				}
                else
				{
                    name = NO_ANIM;
                    throw new Exception("DfLoader Error. No animation found with this name: " + name);
				}
			}
		}

		public void Play()
		{
			playing = true;
		}

		public void Stop()
		{
			playing = false;
		}

		public void Restart()
		{
			currentFrame = 1;
			Play();
		}

		public void Update(GameTime dt)
		{
			var frame = currentFrame;

            if (current == null)
            {
                return;
            }

            if (!playing)
            {
                return;
            }

			time += dt.ElapsedGameTime.Milliseconds;

			if(time >= timeNext)
			{
				timeNext = time + current.Cells[frame].Delay;

				if (!reverse)
				{
					frame++;

					if(frame >= current.Cells.Count)
					{
						if(loops > 0)
						{
							frame = 0;
							loops--;
						}

						// Loops done. Time to stop
						if(loops == 0)
						{
                            if (current.Loops != loops)
                            {
                                Stop();
                                frame = current.Cells.Count - 1;
                            }
                            else
                            {
                                frame = 0;  // Infinite Loops
                            }
						}
					}
				}
				else
				{
					frame--;

					if (frame < 0)
					{
						if (loops > 0)
						{
							frame = current.Cells.Count - 1;
							loops--;
						}

						// Loops done. Time to stop
						if (loops == 0)
						{
                            if (current.Loops != loops)
                            {
                                Stop();
                                frame = 0;
                            }
                            else
                            {
                                frame = current.Cells.Count - 1;    // Infinite Loops
                            }
						}
					}
				}

                if (pingpong)
                {
                    reverse = !reverse;
                }

				currentFrame = frame;
			}

		}

		public void Draw(SpriteBatch batch)
		{
			// Do not try to draw a not found or not defined Name
			if (name == NO_ANIM)
				return;

            var cell = (Cell)current.Cells[currentFrame];
			foreach (KeyValuePair<string, Sprite> pair in cell.CellSprites)
			{
				pair.Value.Pos = Pos;
				pair.Value.Draw(batch);
			}
		}

	}
}
