using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
	class DamageOverlay
	{

		internal void Draw(SpriteBatch spriteBatch)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < Physics.Fighters.Count; i++)
			{
				sb.AppendFormat("P{0}: {1}% ", i + 1, Physics.Fighters[i].Percent);
			}
			string damages = String.Join(" ", sb.ToString());
			spriteBatch.DrawString(Game1.GameFont, damages, new Vector2(50, 20), Color.Black);
		}
	}
}
