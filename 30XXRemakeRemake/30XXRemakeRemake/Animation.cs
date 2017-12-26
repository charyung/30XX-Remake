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
        Rectangle sprite;
        int sX = 0;
        int sY = 0;
        int sWidth = 0;
        int sHeight = 0;
        string nextFrame = ""; //Whether the next frame is right of or below the current sprite frame on the spritesheet. So for example, whirlpool.png would be vertical.
        Rectangle sourceRect;
        Vector2 origin;

        public Texture2D SpriteTexture
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
        ///<param name="sprite"> A rectangle that has information on this animation's sprite: X- and Y-coordinates, and its width and height. </param>
        ///<param name="numberOfFrames"> The number of frames the whole animation has. </param>
        ///<param name="nextFrame"> Whether the next frame is right of or below the current sprite frame on the spritesheet. Can be either "V" for vertical or "H" for horizontal. </param>
        ///<param name="interval"> The number of milliseconds between each frame. The higher the number, the slower the animation. </param>
        ///<param name="currentFrame"> The index of the frame for the animation to start on. </param>
        public Animation(Texture2D texture, Rectangle sprite, int numberOfFrames, string nextFrame, float interval = 55f, int currentFrame = 0)
        {

            this.spriteTexture = texture;
            this.sWidth = sprite.Width;
            this.sHeight = sprite.Height;
            this.numberOfFrames = numberOfFrames;
            this.nextFrame = nextFrame;
            this.interval = interval;
            this.currentFrame = currentFrame;

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
                    currentFrame = 0;
                }
                timer = 0f;
            }

            //origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }
    }
}
