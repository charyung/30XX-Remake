using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake.UI
{
	class DamageOverlay
	{
		private readonly List<DamageCounter> _damageCounters = new List<DamageCounter>();

		internal DamageOverlay(ContentManager content)
		{
			int unitWidth = (Game1.SCREEN_WIDTH - 100) / Physics.Fighters.Count;

			for (int i = 0; i < Physics.Fighters.Count; i++)
			{
				Fighter fighter = Physics.Fighters[i];
				_damageCounters.Add(new DamageCounter(i * unitWidth + 50, Game1.SCREEN_HEIGHT - 150, fighter, content, Game1.GameFont));
			}
		}

		internal void Draw(SpriteBatch spriteBatch)
		{
			foreach (DamageCounter counter in _damageCounters)
			{
				counter.Draw(spriteBatch);
			}
		}
	}
}
