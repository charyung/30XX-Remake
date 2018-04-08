using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    abstract class Move
    {
        protected string direction;
        protected Fighter user;
        protected double dmg;
        protected double kb;
        protected double kbAngle;
        protected bool pauseUser = false;
        private float interval = 0;
        protected Rectangle position;
        public Rectangle hitbox;
        protected Animation ani;

        //ok are you ready
        //for some spaghetti

        //Later when I figure out vectors more, I can use that to replace kb and kbAngle
        //Also maybe later I can separate hitbox and location rect

        /// <summary>
        /// A class for a single attack. This is a superclass and really never actually gets called anywhere directly.
        /// </summary>
        /// <param name="direction"> The "direction" of the attack. Can be either up, down, left, or right. </param>
        /// <param name="sprite"> The path of this attack's sprite. </param>
        /// <param name="position"> A rectangle that represents the actual position of this move: Its X- and Y- coords, and its width and height. </param>
        /// <param name="hitbox"> A rectangle that has the hitbox of this move: Its X- and Y- coords, and its width and height. </param>
        /// <param name="frames"> The number of frames this move's animation has. </param>
        /// <param name="nextFrame"> The position of the next frame in the spritesheet. Can be either "V" for vertical or "H" for horizontal. </param>
        /// <param name="user"> The user of this move. </param>
        /// <param name="dmg"> The amount of damage this attack does. </param>
        /// <param name="kb"> The amount of knockback this attack does. </param>
        /// <param name="kbAngle"> The angle, in radians, in which this attack sends the target. </param>
        /// <param name="pauseUser"> Whether the usage of this attack prevents the user from moving. </param>
        /// <param name="interval"> The number of milliseconds between each frame. The higher the number, the slower the animation. </param>
        public Move(string direction, Texture2D sprite, Rectangle position, Rectangle hitbox, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f)
        {
            this.direction = direction;
            this.position = position;
            this.hitbox = hitbox;
            this.user = user;
            this.dmg = dmg;
            this.kb = kb;
            this.kbAngle = kbAngle;
            this.pauseUser = pauseUser;
            this.interval = interval;

            //just make animation along with this class upon loading?
            ani = new Animation(sprite, new Rectangle(0, 0, hitbox.Width, hitbox.Height), frames, nextFrame, false, interval);
        }

        public Animation SpriteTexture
        {
            get { return ani; }
        }

        public Rectangle Hitbox
        {
            get { return hitbox; }
        }

        public Rectangle Position
        {
            get { return position; }
        }

    }
}
