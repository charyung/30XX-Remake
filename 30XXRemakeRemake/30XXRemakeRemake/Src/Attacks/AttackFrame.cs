using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _30XXRemakeRemake.Src.Attacks
{
	class AttackFrame
	{
		internal float Duration { get; }
		internal Rectangle Hitbox;
		private double _dmg;
		private double _kb;
		private double _kbAngle;
		private bool _pauseUser;

		internal AttackFrame(Rectangle hitbox, float duration = 55f)
		{
			Hitbox = hitbox;
			Duration = duration;
		}
	}
}
