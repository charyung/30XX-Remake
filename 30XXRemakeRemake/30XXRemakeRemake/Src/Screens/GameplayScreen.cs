using System;
using System.Collections.Generic;
using System.Text;
using _30XXRemakeRemake.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace _30XXRemakeRemake.Screens
{
	class GameplayScreen : GameScreen
	{
		private new Game1 Game => (Game1)base.Game;

		private Fighter _fighter;
		private Fighter _fighter2;
		private readonly Type _selectedFighter;
		private Stage _stage;
		private DamageOverlay _damageOverlay;

		public GameplayScreen(Game1 game, Type selectedFighter) : base(game)
		{
			Game.IsMouseVisible = false;
			_selectedFighter = selectedFighter;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			_stage = new Stage(Content.Load<Texture2D>("Textures/temporalTower"), new Rectangle(38, 198, 947, 255));
			Physics.StageHitbox = _stage.hbRect;

			_fighter = (Fighter) Activator.CreateInstance(_selectedFighter, true, new Vector2(200, 100), Content);
			//_fighter = new Omastar(true, new Vector2(200, 100), Content);
			_fighter2 = new Ampharos(false, new Vector2(300, 100), Content);
			Physics.AddToCollisions(_fighter, _fighter.hitbox);
			Physics.AddToCollisions(_fighter2, _fighter2.hitbox);

			_damageOverlay = new DamageOverlay(Game.Content);
		}

		public override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Game.Exit();

			// TODO: Add your update logic here
			_fighter.Update(gameTime);
			_fighter2.Update(gameTime);

			Physics.Update(gameTime);

		}

		public override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			Game.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp);
			Game.SpriteBatch.Draw(_stage.Img, new Vector2(0, 0), new Rectangle(0, 0, 700, 500), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);

			//The special feature here is source rectangle, which basically specifies which part of the spritesheet to use foror the sprite.
			_fighter.Draw(Game.SpriteBatch, gameTime);
			_fighter2.Draw(Game.SpriteBatch, gameTime);

			Game.SpriteBatch.DrawString(Game1.GameFont, _fighter.hitbox.Intersects(_stage.hbRect).ToString() + ", (" + _fighter.Position.X + ", " + _fighter.Position.Y + ")", new Vector2(200, 10), Color.Black);

			Game.SpriteBatch.DrawString(Game1.GameFont, Drawer.m.ToString(), new Vector2(600, 10), Color.White);

			Drawer.Draw(Game.SpriteBatch, gameTime);
			_damageOverlay.Draw(Game.SpriteBatch);
			Game.SpriteBatch.End();
		}
	}
}
