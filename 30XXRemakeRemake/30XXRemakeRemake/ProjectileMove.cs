using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    class ProjectileMove: Move, IUpdatable
    {
        Vector2 vel = new Vector2(1, 0);
        Vector2 accel;

        //dem parameters
        public ProjectileMove(Vector2 accel, string direction, Texture2D sprite, Rectangle position, Rectangle hitbox, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f) : base(direction, sprite, position, hitbox, frames, nextFrame, user, dmg, kb, kbAngle, pauseUser, interval = 55f)
        {
            this.accel = accel;
        }

        public void Update(GameTime gt)
        {
            vel.X = Physics.CalcVel(vel.X, accel.X, 10, gt);
            position.X += (int)vel.X;
			hitbox.X += (int)vel.X;
        }
    }
}
