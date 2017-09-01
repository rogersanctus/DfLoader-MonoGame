using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DfLoader
{
	public class Animation : SpriteBase
	{
		private AnimationDefinition current;
		private int currentFrame;
		private Animations animations;
		private bool playing;
		private double time;
		private double timeNext;
		private bool reverse;
		private bool pingpong;
		private int loops;        

        public const string NO_ANIM = "None";

		public Animation(Animations animations, Vector2 pos = new Vector2(), SpriteBase parent = null)
            : base(parent)
		{
			Name = NO_ANIM;
			this.animations = animations;
			currentFrame = 0;
			current = null;
			playing = false;
			Pos = pos;
			time = 0.0;
			timeNext = 0.0;
			pingpong = reverse = false;
			loops = 0;
        }

        protected override void OnNameChanged()
        {
            // Animation has not a animation name. Only return.
            if(Name.Equals(NO_ANIM))
            {
                return;
            }

            if (animations.Anims.ContainsKey(Name))
            {
                current = animations.Anims[Name];
                timeNext = current.Cells[currentFrame].Delay;
                loops = current.Loops;
            }
            else
            {
                Name = NO_ANIM;
                throw new Exception("DfLoader Error. No animation found with this name: " + Name);
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

		public override void Update(GameTime gameTime)
		{
            base.Update(gameTime);

			var frame = currentFrame;

            if (current == null)
            {
                return;
            }

            // Update the inner sprites for the current cell(frame)
            var cell = current.Cells[currentFrame];
            foreach (var spr in cell.CellSprites)
            {
                spr.Parent = this;
                spr.Update(gameTime);
            }

            // Not playing, just return
            if (!playing)
            {
                return;
            }

			time += gameTime.ElapsedGameTime.Milliseconds;

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

		public override void Draw(SpriteBatch batch)
		{
			// Do not try to draw a not found or not defined Name
			if (Name == NO_ANIM)
				return;

            var cell = current.Cells[currentFrame];
			foreach (var spr in cell.CellSprites)
			{
				spr.Draw(batch);
			}
		}

	}
}
