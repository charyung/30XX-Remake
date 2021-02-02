using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake.UI
{
	internal class DamageCounter
	{
		private static readonly Dictionary<Type, string> typeToTexture = new Dictionary<Type, string>()
		{
			{ typeof(Omastar), "Textures/p1Oma" },
			{ typeof(Ampharos), "Textures/p1Amp" }
		};

		private readonly Type _fighterType;
		internal double Percent { get; set; }
		internal int LivesLeft { get; set; }

		private readonly Texture2D _lifeTexture;
		private readonly Vector2 _position;

		private readonly SpriteFont _gameFont;

		/// <summary>
		/// The counter of a single fighter's % and stocks left
		/// </summary>
		/// <param name="x">The X value of this counter's position</param>
		/// <param name="y">The Y value of this counter's position</param>
		/// <param name="fighterType">The type of fighter to represent</param>
		/// <param name="percent">The perecent the fighter is at</param>
		/// <param name="livesLeft">The amount of lives the figher has left</param>
		/// <param name="content">The ContentManager</param>
		/// <param name="gameFont">The SpriteFont</param>
		internal DamageCounter(int x, int y, Type fighterType, double percent, int livesLeft, ContentManager content, SpriteFont gameFont)
		{
			_fighterType = fighterType;
			_lifeTexture = content.Load<Texture2D>(typeToTexture[fighterType]);
			Percent = percent;
			LivesLeft = livesLeft;
			_position = new Vector2(x, y);
			_gameFont = gameFont;
		}

		internal void Draw(SpriteBatch spriteBatch)
		{
			float livesX = _position.X;
			for (int i = 0; i < LivesLeft; i++)
			{
				spriteBatch.Draw(_lifeTexture, new Vector2(livesX, _position.Y), new Rectangle(0, 0, 50, 50), Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f);

				livesX += 30;
			}

			spriteBatch.DrawString(_gameFont, Percent + "%", new Vector2(_position.X, _position.Y + 30), Color.Black, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
		}
	}
}
