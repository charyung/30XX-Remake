using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*TODO:
 * - how do projectile rendering???
 *  - Consider looking into DrawableGameComponents
 *  - Apparently that's a bad idea though?? hmm
 * - Fix player at wrong height on the ground
 * - Use currAnimation in Omastar
 * - Update gitignore
 * * Improve animations
 *  - Allow for pausing fighter at different times
 *  - Delayed animation
 *  - Moving hitbox
 * . Attacks
 *  . Damage %
 * - Unify all the physics calculations
 * - Improve physics calculations
 * - Unify variable naming scheme
 * . Fix stage rendering (Fixed in Monogame 3.8.1)
 * - Move GameFont elsewhere
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

        internal static int SCREEN_WIDTH = 1050;
        internal static int SCREEN_HEIGHT = 750;

        /// <summary>
        /// Game.
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;   // set this value to the desired height of your window
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

        //private ProjectileAttack atk;
        Fighter ftr;
        private Fighter ftr2;
        Stage tt;
        DamageOverlay damageOverlay;
        //Animation a;
        Vector2 b = new Vector2(10, 100);
        internal static SpriteFont GameFont;
        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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

            GameFont = Content.Load<SpriteFont>("Fonts/Courier New");
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
            ftr.Update(gameTime);
            ftr2.Update(gameTime);
            //a.Animate(gameTime);
            b.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 5;

            Physics.Update(gameTime);

            base.Update(gameTime);
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

            //spriteBatch.Draw(a.SpriteTexture, b, a.SourceRect, Color.White);

            //The special feature here is source rectangle, which basically specifies which part of the spritesheet to use foror the sprite.
            ftr.Draw(spriteBatch, gameTime);
            ftr2.Draw(spriteBatch, gameTime);

            spriteBatch.DrawString(GameFont, ftr.hitbox.Intersects(tt.hbRect).ToString() + ", (" + ftr.Position.X + ", " + ftr.Position.Y + ")", new Vector2(200, 10), Color.Black);

            spriteBatch.DrawString(GameFont, Drawer.m.ToString(), new Vector2(600, 10), Color.White);

            Drawer.Draw(spriteBatch, gameTime);
            damageOverlay.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
