using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    class ProjectileAttack: Attack
    {
        Vector2 _vel;
        Vector2 _accel;

        //dem parameters
        public ProjectileAttack(Vector2 vel, Vector2 accel, string direction, Texture2D sprite, Rectangle position, Rectangle hitbox, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f) : base(direction, sprite, position, hitbox, frames, nextFrame, user, dmg, kb, kbAngle, pauseUser, interval)
        {
	        _vel = vel;
            _accel = accel;
        }

        public override void Update(GameTime gt)
        {
            _vel.X = Physics.CalcVel(_vel.X, _accel.X, 10, 1, gt);
            position.X += (int)_vel.X;
			hitbox.X += (int)_vel.X;
        }
    }
}
