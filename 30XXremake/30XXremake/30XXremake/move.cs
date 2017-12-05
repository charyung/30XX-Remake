using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _30XXremake
{
    class move
    {
        private string direction;
        private double dmg;
        private double kb;
        private double kbAngle;
        private bool pauseUser = false;
        public Rectangle hitbox;
        private animation ani;

        //X: x-coord
		//Y: y-coord
		//sWidth: the width of each frame of the image
		//sHeight, same as sWidth, but height
		//img: Attack's spritesheet
		//frames: The amount of frames the sprite has
		//user: Who used the attack
		//dmg: The amount of damage the attack deals
		//kb: The amount of knockback the attack deals
		//kbAngle: The angle the attack sends opponents back at
		//fps: The framerate of the animation
		//pauseUser: Whether the attack pauses the user

        public move(string direction, float x, float y, int sWidth, int sHeight, string img, int frames, fighter user, double dmg, double kb, double kbAngle, bool pauseUser)
        {
            this.direction = direction;
            this.dmg = dmg;
            this.kb = kb;
            this.kbAngle = kbAngle;
            this.pauseUser = pauseUser;

            hitbox = new Rectangle((int)x, (int)y, sWidth, sHeight);

            //just make animation along with this class upon loading?
            
        }
    }
}
