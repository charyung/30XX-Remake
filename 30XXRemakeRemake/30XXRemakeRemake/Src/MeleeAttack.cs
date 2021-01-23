using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace _30XXRemakeRemake
{
    class MeleeAttack : Attack, IUpdatable
	{
        public MeleeAttack(string direction, Texture2D sprite, Rectangle position, Rectangle hitbox, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f) : base(direction, sprite, position, hitbox, frames, nextFrame, user, dmg, kb, kbAngle, pauseUser, interval)
        {
			//not much I guess
        }

		public override void Update(GameTime gt)
		{
			spriteTexture.SourceRect = new Rectangle(Point.Zero, new Point(3, 0));
			HashSet<Fighter> fightersWithoutUser = new HashSet<Fighter>(Physics.Fighters);
			fightersWithoutUser.Remove(user);
			foreach (Fighter fighter in fightersWithoutUser.Where(fighter => hitbox.Intersects(fighter.hitbox)))
			{
				fighter.TakeKnockback(kbAngle, kb);
			}
		}

		internal override void Cleanup()
		{
			user.Unpause();
		}
	}
}
