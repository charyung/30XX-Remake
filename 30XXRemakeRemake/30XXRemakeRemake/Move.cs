using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _30XXRemakeRemake
{
    class Move
    {
        private string direction;
        private double dmg;
        private double kb;
        private double kbAngle;
        private bool pauseUser = false;
        public Rectangle hitbox;
        private Animation ani;

        //ok are you ready
        //for some spaghetti
        
        /// <summary>
        /// A class for a single attack.
        /// </summary>
        /// <param name="direction"> The "direction" of the attack. Can be either up, down, left, or right. </param>
        /// <param name="x"> The X-coordinate of the sprite. </param>
        /// <param name="y"> The Y-coordinate of the sprite </param>
        /// <param name="sprite"> The path of this attack's sprite. </param>
        /// <param name="sWidth"> The sprite's width. </param>
        /// <param name="sHeight"> The sprite's height. </param>
        /// <param name="frames"> The number of frames this move's animation has. </param>
        /// <param name="nextFrame"> The position of the next frame in the spritesheet. Can be either "V" for vertical or "H" for horizontal. </param>
        /// <param name="user"> The user of this move. </param>
        /// <param name="dmg"> The amount of damage this attack does. </param>
        /// <param name="kb"> The amount of knockback this attack does. </param>
        /// <param name="kbAngle"> The angle in which this attack sends the target. </param>
        /// <param name="pauseUser"> Whether the usage of this attack prevents the user from moving. </param>
        public Move(string direction, float x, float y, string sprite, int sWidth, int sHeight, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser)
        {
            this.direction = direction;
            this.dmg = dmg;
            this.kb = kb;
            this.kbAngle = kbAngle;
            this.pauseUser = pauseUser;

            hitbox = new Rectangle((int)x, (int)y, sWidth, sHeight);

            //just make animation along with this class upon loading?
            ani = new Animation(sprite, 0, 0, 0, frames, sWidth, sHeight, nextFrame);
        }
    }
}
