using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXremake
{
    class animation
    {
        Texture2D spriteTexture;
        float timer = 0f; //time before next frame
        float interval = 55f; //how long each frame lasts, in millseconds
        int currentFrame = 1;
        int numberOfFrames = 1;
        int sWidth = 0; //60
        int sHeight = 0; //10
        Rectangle sourceRect;
        Vector2 position;
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

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public animation(Texture2D texture, int currentFrame, int numberOfFrames, int sWidth, int sHeight)
        {
            this.spriteTexture = texture;
            this.currentFrame = currentFrame - 1;
            this.numberOfFrames = numberOfFrames - 1;
            this.sWidth = sWidth;
            this.sHeight = sHeight;

            //origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        public void animate(GameTime gt)
        {
            sourceRect = new Rectangle(0, currentFrame * sHeight, sWidth, sHeight);
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
