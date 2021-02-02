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

		private readonly Fighter _fighter;

		private readonly Texture2D _lifeTexture;
		private readonly Vector2 _position;

		private readonly SpriteFont _gameFont;

		/// <summary>
		/// The counter of a single fighter's % and stocks left
		/// </summary>
		/// <param name="x">The X value of this counter's position</param>
		/// <param name="y">The Y value of this counter's position</param>
		/// <param name="fighter">The fighter this counter represents</param>
		/// <param name="content">The ContentManager</param>
		/// <param name="gameFont">The SpriteFont</param>
		internal DamageCounter(int x, int y, Fighter fighter, ContentManager content, SpriteFont gameFont)
		{
			_fighter = fighter;
			_lifeTexture = content.Load<Texture2D>(typeToTexture[fighter.GetType()]);
			_position = new Vector2(x, y);
			_gameFont = gameFont;
		}

		internal void Draw(SpriteBatch spriteBatch)
		{
			float livesX = _position.X;
			for (int i = 0; i < _fighter.LivesLeft; i++)
			{
				spriteBatch.Draw(_lifeTexture, new Vector2(livesX, _position.Y), new Rectangle(0, 0, 50, 50), Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f);

				livesX += 30;
			}

			spriteBatch.DrawString(_gameFont, _fighter.Percent + "%", new Vector2(_position.X, _position.Y + 30), Color.Black, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
		}
	}
}
