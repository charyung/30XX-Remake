using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake.Animations
{
    class Animation
    {
	    int _currentFrame = 1;
	    readonly float interval; //how long each frame lasts, in millseconds
	    readonly bool loop;
	    readonly string nextFrame = ""; //Whether the next frame is right of or below the current sprite frame on the spritesheet. So for example, whirlpool.png would be vertical.
	    readonly int numberOfFrames = 1;
	    readonly int sHeight = 0;
	    Rectangle sourceRect;
	    readonly int sWidth = 0;
	    readonly int sX = 0; //sX and Y are the X and Y coordinates (in pixel) in which the starting frame, on the spritesheet, is located.
	    readonly int sY = 0;
	    float timer = 0f; //time before next frame

	    ///<summary>
        ///A single animation.
        ///</summary>
        ///<param name="texture"> The path to this animation's texture. </param>
        ///<param name="sprite"> A rectangle that has information on this animation's sprite: X- and Y-coordinates, and its width and height. </param>
        ///<param name="numberOfFrames"> The number of frames the whole animation has. </param>
        ///<param name="nextFrame"> Whether the next frame is right of or below the current sprite frame on the spritesheet. Can be either "V" for vertical or "H" for horizontal. </param>
        ///<param name="interval"> The number of milliseconds between each frame. The higher the number, the slower the animation. </param>
        ///<param name="currentFrame"> The index of the frame for the animation to start on. </param>
        ///<param name="loop"> Indicates whether the animation loops or just plays once. </param>
        public Animation(Texture2D texture, Rectangle sprite, int numberOfFrames, string nextFrame, bool loop = false, float interval = 55f, int currentFrame = 0)
        {

            this.SpriteTexture = texture;
            (int x, int y, int width, int height) = sprite;
            sX = x;
            sY = y;
            this.sWidth = width;
            this.sHeight = height;
            this.numberOfFrames = numberOfFrames;
            this.nextFrame = nextFrame;
            this.interval = interval;
            this._currentFrame = currentFrame;
            this.loop = loop;

            sourceRect = new Rectangle(x, y, sWidth, sHeight);
            //origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }
	    //Vector2 origin; //the fuck is this for??

	    //Animation HAS NO POSITION property because a position is a property of an object itself, not of its animation.

	    public Texture2D SpriteTexture { get; set; }

	    public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { SetSourceBox(value); }
        }

	    /*public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }*/

	    public Vector2 SpritesheetLocation
        {
            get { return new Vector2(sX, sY); }
        }

	    public bool Finished { get; private set; } = false;

	    public void Animate(GameTime gt)
        {
            //Redefine the source rectangle such that it takes the correct part of the spritesheet for the current frame.
            int x;
            int y;

            int rem;
            if (nextFrame == "V")
            {
	            int rowNum = SpriteTexture.Height / sHeight;
	            x = Math.DivRem(_currentFrame, rowNum, out rem) * sHeight;
	            y = rem * sHeight;
            }
            else
            {
	            int colNum = SpriteTexture.Width / sWidth;
	            y = Math.DivRem(_currentFrame, colNum, out rem) * sWidth;
	            x = rem * sWidth;
            }

            x += sX;
            y += sY;
            sourceRect = new Rectangle(x, y, sWidth, sHeight);
            
            timer += (float)gt.ElapsedGameTime.TotalMilliseconds;

            /*Basically what happens here is:
             * This function gets called every frame.
             * First, timer adds the amount of time between the last calling and this calling.
             * Then, if timer is greater than interval, ie it's time to switch frames, then it does so:
             * It looks to the next frame, and set the current frame to that (that's the code block above this).
             * If it's the last frame, then something different happens depending on if the animation is set to loop.
             * If it is set to loop, then the animation goes back to the first frame.
             * If it's set to not loop, then it flags that it is finished and stops animating.
             */
	        if (timer < interval || Finished)
		        return;

            _currentFrame++;

            if (_currentFrame >= numberOfFrames)
            {
                if (loop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    _currentFrame--;
                    Finished = true;
                }
            }
            timer = 0f;

            //origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

	    /// <summary>
		/// Set the size of this animation's source rectangle.
		/// </summary>
		/// <param name="size"> How much to increase the box by. Use negative numbers to indicate a decrease in size. Because of some weird setter shenanigans, just set 0 for X and Y lul</param>
		private void SetSourceBox(Rectangle size)
		{
			(_, _, int width, int height) = size;
			sourceRect.Width += width;
			sourceRect.Height += height;
		}

	    internal void Reset()
	    {
		    _currentFrame = 0;
		    timer = 0f;
		    Finished = false;
	    }
    }
}
