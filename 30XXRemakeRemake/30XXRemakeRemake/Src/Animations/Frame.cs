using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake.Animations
{
	internal class Frame
	{
		internal float Duration { get; } //how long each frame lasts, in millseconds
		private readonly Texture2D _texture;
		private readonly Rectangle _sourceRect;
		private readonly Rectangle _destRect;
		private readonly bool _isFlipped;

		internal Frame(Texture2D texture, Rectangle sourceRect, Rectangle destRect, bool isFlipped, float duration = 55f)
		{
			_texture = texture;
			_sourceRect = sourceRect;
			_destRect = destRect;
			_isFlipped = isFlipped;
			Duration = duration;
		}

		internal void Draw(SpriteBatch spriteBatch)
		{
			SpriteEffects effect = _isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			spriteBatch.Draw(_texture, _destRect, _sourceRect, Color.White, 0, Vector2.Zero, effect, 0);
		}
	}
}
