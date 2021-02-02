using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace _30XXRemakeRemake.Screens
{
	class CharSelectScreen : GameScreen
	{
		private new Game1 Game => (Game1)base.Game;

		private Texture2D _omastarButton;
		private Rectangle _omastarButtonLocation = new Rectangle(40, 110, 50, 50);

		private Texture2D _amphButton;
		private Rectangle _amphButtonLocation = new Rectangle(100, 110, 50, 50);

		public CharSelectScreen(Game1 game) : base(game)
		{
			Game.IsMouseVisible = true;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			_omastarButton = Game.Content.Load<Texture2D>("Textures/p1Oma");
			_amphButton = Game.Content.Load<Texture2D>("Textures/p1Amp");
		}

		public override void Update(GameTime gameTime)
		{
			MouseState mouseState = Mouse.GetState();

			Type selectedFighter = null;

			if (mouseState.LeftButton != ButtonState.Pressed) return;

			Point mouseLocation = new Point(mouseState.X, mouseState.Y);
			if (_omastarButtonLocation.Contains(mouseLocation))
			{
				selectedFighter = typeof(Omastar);
			}
			else if (_amphButtonLocation.Contains(mouseLocation))
			{
				selectedFighter = typeof(Ampharos);
			}

			if (selectedFighter != null)
			{
				ScreenManager.LoadScreen(new GameplayScreen(Game, selectedFighter), new FadeTransition(GraphicsDevice, Color.Black));
			}
		}

		public override void Draw(GameTime gameTime)
		{
			Game.GraphicsDevice.Clear(Color.CornflowerBlue);
			Game.SpriteBatch.Begin();
			Game.SpriteBatch.Draw(_omastarButton, new Vector2(_omastarButtonLocation.X, _omastarButtonLocation.Y), Color.White);
			Game.SpriteBatch.Draw(_amphButton, new Vector2(_amphButtonLocation.X, _amphButtonLocation.Y), Color.White);
			Game.SpriteBatch.End();
		}
	}
}
