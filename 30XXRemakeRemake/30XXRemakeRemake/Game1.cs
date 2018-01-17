﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*TODO:
 * - how do projectile rendering???
 *  - Consider looking into DrawableGameComponents
 *  - Apparently that's a bad idea though?? hmm
 */


namespace _30XXRemakeRemake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Game.
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1050;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 750;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        Animation whirlpool;
        //Texture2D whirlpool;
        Fighter omastar;
        Stage tt;
        Animation a;
        Vector2 b = new Vector2(10, 100);
        Texture2D c;
        bool d = false;
        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            whirlpool = new Animation(Content.Load<Texture2D>("Textures/whirlpool"), new Rectangle(0, 0, 10, 120), 3, "V", true, 10);

            a = new Animation(Content.Load<Texture2D>("Textures/omastar2"), new Rectangle(0, 0, 51, 44), 2, "H", true);
            c = Content.Load<Texture2D>("Textures/auraSphere");

            //tt = new Stage(Content.Load<Texture2D>("textures/temporalTower"), new Rectangle(27, 132, 637, 144));
            tt = new Stage(Content.Load<Texture2D>("Textures/temporalTower"), new Rectangle(38, 198, 947, 255));
            Physics.StageHitbox = tt.hbRect;

            omastar = new Omastar(new Vector2(200, 10), Content);
            Physics.AddToCollisions(omastar, omastar.hitbox);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //omastar.Movement(gameTime);
            omastar.Update(gameTime);
            //whirlpool.Animate(gameTime);
            a.Animate(gameTime);
            b.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 5;
            //omastar.walking.Animate(gameTime);

            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                d = true;
            }
            else
            {
                d = false;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            spriteBatch.Draw(tt.Img, new Vector2(0, 0), new Rectangle(0, 0, 700, 500), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Content.Load<Texture2D>(whirlpool.SpriteTexture), new Vector2(10, 10), whirlpool.SourceRect, Color.White);

            spriteBatch.Draw(a.SpriteTexture, b, a.SourceRect, Color.White);
            //spriteBatch.Draw(Content.Load<Texture2D>("Textures/whirlpool"), new Rectangle(10, 10, 120, 10), Color.White);
            //The special feature here is source rectangle, which basically specifies which part of the spritesheet to use foror the sprite.
            //spriteBatch.Draw(omastar.walking.SpriteTexture, omastar.Position, new Rectangle(0, 0, omastar.hitbox.Width, omastar.hitbox.Height), Color.White, 0f, new Vector2(0, 0), 1, omastar.Facing, 0f);
            spriteBatch.Draw(omastar.Sprite, omastar.Position, omastar.walking.SourceRect, Color.White, 0f, new Vector2(0, 0), 1, omastar.Facing, 0f);

            spriteBatch.DrawString(Content.Load<SpriteFont>("Fonts/Courier New"), omastar.hitbox.Intersects(tt.hbRect).ToString() + ", (" + omastar.Position.X + ", " + omastar.Position.Y + ")", new Vector2(200, 10), Color.Black);

            if (d)
            {
                spriteBatch.Draw(c, new Rectangle(10, 50, 30, 30), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
