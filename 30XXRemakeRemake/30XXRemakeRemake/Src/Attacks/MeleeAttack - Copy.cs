using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using _30XXRemakeRemake.Src.Attacks;

namespace _30XXRemakeRemake
{
    class MeleeAttack_new : Attack, IUpdatable
	{
        public MeleeAttack_new(string direction, Texture2D sprite, Rectangle position, List<AttackFrame> attackFrames, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f) : base(direction, sprite, position, attackFrames[0].Hitbox, frames, nextFrame, user, dmg, kb, kbAngle, pauseUser, interval, attackFrames)
        {
			//not much I guess
        }

		public override void Update(GameTime gt)
		{
			base.Update(gt);

			spriteTexture.SourceRect = new Rectangle(Point.Zero, new Point(3, 0));
			HashSet<Fighter> fightersWithoutUser = new HashSet<Fighter>(Physics.Fighters);
			fightersWithoutUser.Remove(user);
			foreach (Fighter fighter in fightersWithoutUser.Where(fighter => hitbox.Intersects(fighter.hitbox)))
			{
				fighter.TakeKnockback(kbAngle, kb);
				fighter.Percent += dmg;
			}
		}

		internal override void Cleanup()
		{
			user.Unpause();
		}
	}
}
