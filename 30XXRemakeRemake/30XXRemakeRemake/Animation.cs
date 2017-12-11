using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    class Animation
    {
        string spriteTexture;
        float timer = 0f; //time before next frame
        float interval = 55f; //how long each frame lasts, in millseconds
        int currentFrame = 1;
        int numberOfFrames = 1;
        int sX = 0;
        int sY = 0;
        int sWidth = 0; //60
        int sHeight = 0; //10
        string nextFrame = ""; //Whether the next frame is right of or below the current sprite frame on the spritesheet. So for example, whirlpool.png would be vertical.
        Rectangle sourceRect;
        Vector2 origin;

        public string SpriteTexture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        ///<summary>
        ///A single animation.
        ///</summary>
        ///<param name="texture"> The path to this animation's texture. </param>
        ///<param name="sX"> The X-coordinate, in pixels, of the top left corner of the first frame of the sprite. </param>
        ///<param name="sY"> The Y-coordinate, in pixels, of the top left corner of the first frame of the sprite. </param>
        ///<param name="currentFrame"> The index of the frame for the animation to start on. </param>
        ///<param name="numberOfFrames"> The number of frames the whole animation has. </param>
        ///<param name="sWidth"> The width of the sprite. </param>
        ///<param name="sHeight"> The height of the sprite. </param>
        ///<param name="nextFrame"> Whether the next frame is right of or below the current sprite frame on the spritesheet. Can be either "V" for vertical or "H" for horizontal. </param>
        public Animation(string texture, int sX, int sY, int currentFrame, int numberOfFrames, int sWidth, int sHeight, string nextFrame)
        {

            this.spriteTexture = texture;
            this.currentFrame = currentFrame - 1;
            this.numberOfFrames = numberOfFrames - 1;
            this.sWidth = sWidth;
            this.sHeight = sHeight;
            this.nextFrame = nextFrame;

            this.SourceRect = new Rectangle(0, 0, sWidth, sHeight);
            //origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        public void Animate(GameTime gt)
        {
            //Redefine the source rectangle such that it takes the correct part of the spritesheet for the current frame.
            if (nextFrame == "V")
            {
                sourceRect = new Rectangle(sX, currentFrame * sHeight, sWidth, sHeight);
            }
            else
            {
                sourceRect = new Rectangle(currentFrame * sWidth, sY, sWidth, sHeight);
            }
            
            timer += (float)gt.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame >= numberOfFrames)
                {
                    currentFrame = 1;
                }
                timer = 0f;
            }

            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }
    }
}
