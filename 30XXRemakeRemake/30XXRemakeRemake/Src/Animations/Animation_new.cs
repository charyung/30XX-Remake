using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake.Animations
{
	internal class Animation_new
	{
		private int _currentFrame = 0;
		private readonly List<Frame> _frames;
		private readonly bool _loops;
		private float _timer;
		internal bool Finished { get; private set; }

		internal Animation_new(List<Frame> frames, bool loops)
		{
			_frames = frames;
			_loops = loops;
		}

        internal void Animate(GameTime gt)
        {
            _timer += (float)gt.ElapsedGameTime.TotalMilliseconds;

            /*Basically what happens here is:
             * This function gets called every frame.
             * First, _timer adds the amount of time between the last calling and this calling.
             * Then, if _timer is greater than interval, ie it's time to switch frames, then it does so:
             * It looks to the next frame, and set the current frame to that.
             * If it's the last frame, then something different happens depending on if the animation is set to loop.
             * If it is set to loop, then the animation goes back to the first frame.
             * If it's set to not loop, then it flags that it is finished and stops animating.
             */
            if (_timer < _frames[_currentFrame].Duration || Finished)
                return;

            _currentFrame++;

            if (_currentFrame >= _frames.Count)
            {
                if (_loops)
                {
                    _currentFrame = 0;
                }
                else
                {
                    _currentFrame--;
                    Finished = true;
                }
            }
            _timer = 0f;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
	        _frames[_currentFrame].Draw(spriteBatch);
        }
    }
}
