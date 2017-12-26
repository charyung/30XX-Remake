using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    class ProjectileMove: Move
    {
        //dem parameters
        public ProjectileMove(float accel, string direction, string sprite, Rectangle hitbox, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f) : base(direction, sprite, hitbox, frames, nextFrame, user, dmg, kb, kbAngle, pauseUser, interval = 55f)
        {

        }
    }
}
