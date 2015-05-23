using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DfLoaderMonoGame
{
	public class DfAnimation
	{
		private DfAnimationDef current;
		private int currentFrame;
		private DfAnimations animations;
		private bool playing;
		private string _animation;
		private double _time;
		private double _timeNext;
		private bool _reverse;
		private bool _pingpong;
		private int loops;

		public Microsoft.Xna.Framework.Vector2 pos;

		public const string NO_ANIM = "None";

		public DfAnimation(DfAnimations animations, Microsoft.Xna.Framework.Vector2 pos = new Microsoft.Xna.Framework.Vector2())
		{
			_animation = NO_ANIM;				
			this.animations = animations;
			currentFrame = 0;
			current = null;
			playing = false;
			this.pos = pos;
			_time = 0.0;
			_timeNext = 0.0;
			_pingpong = _reverse = false;
			loops = 0;
		}

		public string animation
		{
			get { return _animation; }

			set
			{
				_animation = value;
				if(animations.anims.ContainsKey(_animation))
				{
					current = animations.anims[_animation];
					_timeNext = current.cells[currentFrame].delay;
					loops = current.loops;
				}else
				{
					System.Console.WriteLine("Error. No animation found with this name: " + _animation);
					_animation = NO_ANIM;
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

		public void Update(Microsoft.Xna.Framework.GameTime dt)
		{
			var frame = currentFrame;
			if (current == null)
				return;

			if (!playing)
				return;

			_time += dt.ElapsedGameTime.Milliseconds;
			//_time /= 1000;

			if(_time >= _timeNext)
			{
				_timeNext = _time + current.cells[frame].delay;

				if (!_reverse)
				{
					frame++;

					if(frame >= current.cells.Count)
					{
						if(loops > 0)
						{
							frame = 0;
							loops--;
						}

						// Loops done. Time to stop
						if(loops == 0)
						{
							if (current.loops != loops)
							{
								Stop();
								frame = current.cells.Count - 1;
							}
							else
								frame = 0;	// Infinite loops
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
							frame = current.cells.Count - 1;
							loops--;
						}

						// Loops done. Time to stop
						if (loops == 0)
						{
							if (current.loops != loops)
							{
								Stop();
								frame = 0;
							}
							else
								frame = current.cells.Count - 1;	// Infinite loops
						}
					}
				}

				if (_pingpong)
					_reverse = !_reverse;

				currentFrame = frame;
			}

		}

		public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
		{
			// Do not try to draw a not found or not defined animation
			if (_animation == NO_ANIM)
				return;

			foreach (KeyValuePair<string, DfSprite> pair in current.cells[currentFrame].cell_sprs)
			{
				pair.Value.pos = pos;
				pair.Value.Draw(batch);
			}
		}

	}
}
