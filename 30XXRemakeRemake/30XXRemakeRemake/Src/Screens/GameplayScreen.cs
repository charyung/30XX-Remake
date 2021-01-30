using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace _30XXRemakeRemake.Screens
{
	class GameplayScreen : GameScreen
	{
		private new Game1 Game => (Game1)base.Game;

		//private ProjectileAttack atk;
		Fighter ftr;
		private Fighter ftr2;
		Stage tt;
		DamageOverlay damageOverlay;
		//Animation a;
		Vector2 b = new Vector2(10, 100);

		public GameplayScreen(Game1 game) : base(game)
		{
			Game.IsMouseVisible = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			//a = new Animation(Content.Load<Texture2D>("Textures/omastar2"), new Rectangle(0, 0, 51, 44), 2, "H", true);
			//Texture2D atkTexture = Content.Load<Texture2D>("Textures/rockBlast");
			//atk = new ProjectileAttack(new Vector2(-1, 0), new Vector2(-0.01f, 0), "Left", atkTexture, new Rectangle((int)100, 150, 30, 30), new Rectangle(100, 150, 30, 30), 6, "H", ftr2, 20, 10, Math.PI - Math.PI / 6, true);

			//tt = new Stage(Content.Load<Texture2D>("textures/temporalTower"), new Rectangle(27, 132, 637, 144));
			tt = new Stage(Content.Load<Texture2D>("Textures/temporalTower"), new Rectangle(38, 198, 947, 255));
			Physics.StageHitbox = tt.hbRect;

			ftr = new Omastar(true, new Vector2(200, 100), Content);
			ftr2 = new Ampharos(false, new Vector2(300, 100), Content);
			Physics.AddToCollisions(ftr, ftr.hitbox);
			Physics.AddToCollisions(ftr2, ftr2.hitbox);

			damageOverlay = new DamageOverlay();
		}

		public override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Game.Exit();

			// TODO: Add your update logic here
			ftr.Update(gameTime);
			ftr2.Update(gameTime);
			//a.Animate(gameTime);
			b.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

			Physics.Update(gameTime);

		}

		public override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			Game.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp);
			Game.SpriteBatch.Draw(tt.Img, new Vector2(0, 0), new Rectangle(0, 0, 700, 500), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);

			//Game.SpriteBatch.Draw(a.SpriteTexture, b, a.SourceRect, Color.White);

			//The special feature here is source rectangle, which basically specifies which part of the spritesheet to use foror the sprite.
			ftr.Draw(Game.SpriteBatch, gameTime);
			ftr2.Draw(Game.SpriteBatch, gameTime);

			Game.SpriteBatch.DrawString(Game1.GameFont, ftr.hitbox.Intersects(tt.hbRect).ToString() + ", (" + ftr.Position.X + ", " + ftr.Position.Y + ")", new Vector2(200, 10), Color.Black);

			Game.SpriteBatch.DrawString(Game1.GameFont, Drawer.m.ToString(), new Vector2(600, 10), Color.White);

			Drawer.Draw(Game.SpriteBatch, gameTime);
			damageOverlay.Draw(Game.SpriteBatch);
			Game.SpriteBatch.End();
		}
	}
}
