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
	class SplashScreen : GameScreen
	{
		private new Game1 Game => (Game1) base.Game;

		private Texture2D _logo;
		private Texture2D _cssButton;
		private Rectangle _cssButtonLocation = new Rectangle(40, 110, 250, 110);

		public SplashScreen(Game1 game) : base(game)
		{
			Game.IsMouseVisible = true;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			_logo = Game.Content.Load<Texture2D>("Textures/new30xxLogo");
			_cssButton = Game.Content.Load<Texture2D>("Textures/CSSbutton");
		}

		public override void Update(GameTime gameTime)
		{
			MouseState mouseState = Mouse.GetState();

			if (mouseState.LeftButton != ButtonState.Pressed) return;

			Point mouseLocation = new Point(mouseState.X, mouseState.Y);
			if (_cssButtonLocation.Contains(mouseLocation))
			{
				ScreenManager.LoadScreen(new GameplayScreen(Game), new FadeTransition(GraphicsDevice, Color.Black));
			}
		}

		public override void Draw(GameTime gameTime)
		{
			Game.GraphicsDevice.Clear(Color.CornflowerBlue);
			Game.SpriteBatch.Begin();
			Game.SpriteBatch.Draw(_logo, new Vector2(40, 40), Color.White);
			Game.SpriteBatch.Draw(_cssButton, new Vector2(_cssButtonLocation.X, _cssButtonLocation.Y), Color.White);
			Game.SpriteBatch.End();
		}
	}
}
