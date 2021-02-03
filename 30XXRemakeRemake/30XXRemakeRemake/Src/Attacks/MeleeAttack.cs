using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using _30XXRemakeRemake.Src.Attacks;

namespace _30XXRemakeRemake
{
    class MeleeAttack : Attack, IUpdatable
	{
		/// <summary>
		/// A melee attack with a static hitbox
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="sprite"></param>
		/// <param name="position"></param>
		/// <param name="hitbox"></param>
		/// <param name="frames"></param>
		/// <param name="nextFrame"></param>
		/// <param name="user"></param>
		/// <param name="dmg"></param>
		/// <param name="kb"></param>
		/// <param name="kbAngle"></param>
		/// <param name="pauseUser"></param>
		/// <param name="interval"></param>
        public MeleeAttack(string direction, Texture2D sprite, Rectangle position, Rectangle hitbox, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f) : base(direction, sprite, position, hitbox, frames, nextFrame, user, dmg, kb, kbAngle, pauseUser, interval)
        {
			//not much I guess
        }

		/// <summary>
		/// A melee attack with a dynamic hitbox
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="sprite"></param>
		/// <param name="position"></param>
		/// <param name="attackFrames"></param>
		/// <param name="frames"></param>
		/// <param name="nextFrame"></param>
		/// <param name="user"></param>
		/// <param name="dmg"></param>
		/// <param name="kb"></param>
		/// <param name="kbAngle"></param>
		/// <param name="pauseUser"></param>
		/// <param name="interval"></param>
		public MeleeAttack(string direction, Texture2D sprite, Rectangle position, List<AttackFrame> attackFrames, int frames, string nextFrame, Fighter user, double dmg, double kb, double kbAngle, bool pauseUser, float interval = 55f) : base(direction, sprite, position, attackFrames[0].Hitbox, frames, nextFrame, user, dmg, kb, kbAngle, pauseUser, interval, attackFrames)
		{}

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
