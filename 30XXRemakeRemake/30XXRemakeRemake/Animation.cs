using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace _30XXRemakeRemake
{
    class Animation
    {
        Texture2D spriteTexture;
        float timer = 0f; //time before next frame
        float interval = 55f; //how long each frame lasts, in millseconds
        int currentFrame = 1;
        int numberOfFrames = 1;
        int sX = 0; //sX and Y are the X and Y coordinates (in pixel) in which the starting frame, on the spritesheet, is located.
        int sY = 0;
        int sWidth = 0;
        int sHeight = 0;
        string nextFrame = ""; //Whether the next frame is right of or below the current sprite frame on the spritesheet. So for example, whirlpool.png would be vertical.
        Rectangle sourceRect;
        bool loop;
        bool finished = false; //Whether the animation has finished and is ready to be removed
        //Vector2 origin; //the fuck is this for??

        //Animation HAS NO POSITION property because a position is a property of an object itself, not of its animation.

        public Texture2D SpriteTexture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }

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

        public bool Finished
        {
            get { return finished; }
        }

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

            this.spriteTexture = texture;
            this.sWidth = sprite.Width;
            this.sHeight = sprite.Height;
            this.numberOfFrames = numberOfFrames;
            this.nextFrame = nextFrame;
            this.interval = interval;
            this.currentFrame = currentFrame;
            this.loop = loop;

            this.SourceRect = new Rectangle(0, 0, sWidth, sHeight);
            //origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        public void Animate(GameTime gt)
        {
            //Redefine the source rectangle such that it takes the correct part of the spritesheet for the current frame.
			 sourceRect = (nextFrame == "V") ? new Rectangle(sX, currentFrame * sHeight, sWidth, sHeight) : new Rectangle(currentFrame * sWidth, sY, sWidth, sHeight);
            
            timer += (float)gt.ElapsedGameTime.TotalMilliseconds;

            /*Basically what happens here is:
             * This function gets called every frame.
             * First, timer adds the amount of time between the last calling and this calling.
             * Then, if timer is greater than interval, ie it's time to switch frames, then it does so:
             * It looks to the next frame, and set the current frame to that (that's the code block above this).
             * If it's the last frame, then something different happens depending on if the animation is set to loop.
             * If it is set to loop, thene the animation goes back to the first frame.
             * If it's set to not loop, then it flags that it is finished and stops animating.
             */
            if (timer > interval && !finished)
            {
                currentFrame++;

                if (currentFrame >= numberOfFrames)
                {
                    if (loop)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame--;
                        finished = true;
                    }
                }
                timer = 0f;
            }

            //origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

		/// <summary>
		/// Set the size of this animation's source rectangle.
		/// </summary>
		/// <param name="size"> How much to increase the box by. Use negative numbers to indicate a decrease in size. Because of some weird setter shenanigans, just set 0 for X and Y lul</param>
		private void SetSourceBox(Rectangle size)
		{
			sourceRect.Width += (int)size.Width;
			sourceRect.Height += (int)size.Height;
		}
    }
}
