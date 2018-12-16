using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    class MeleeMove : Move, IUpdatable
	{
        public MeleeMove(string direction, Texture2D sprite, Rectangle position, Rectangle hitbox, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f) : base(direction, sprite, position, hitbox, frames, nextFrame, user, dmg, kb, kbAngle, pauseUser, interval = 55f)
        {
			//not much I guess
        }

		public void Update(GameTime gt)
		{
			ani.SourceRect = new Rectangle(Point.Zero, new Point(3, 0));
			//update with kb logic	
			if (ani.Finished)
			{
				user.paused = false;
			}
		}
	}
}
