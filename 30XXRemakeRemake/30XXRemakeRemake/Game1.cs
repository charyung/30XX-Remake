using _30XXRemakeRemake.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

/*TODO:
 * - how do projectile rendering???
 *  - Consider looking into DrawableGameComponents
 *  - Apparently that's a bad idea though?? hmm
 * - Use currAnimation in Omastar
 * - Improve animations
 *  - Allow for pausing fighter at different times
 *  - Delayed animation
 *  - Moving hitbox
 * - Unify all the physics calculations
 * - Improve physics calculations
 * - Unify variable naming scheme
 * - Fix stage rendering (Fixed in Monogame 3.8.1, https://github.com/MonoGame/MonoGame/issues/7298)
 * - Move GameFont elsewhere
 * - Add stage properties (e.g. blast zones)
 * - Add AI
 * - Hold down to fall faster
 */


namespace _30XXRemakeRemake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        internal SpriteBatch SpriteBatch;

        internal static int SCREEN_WIDTH = 1050;
        internal static int SCREEN_HEIGHT = 750;

        internal readonly ScreenManager ScreenManager;

        /// <summary>
        /// Game.
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferWidth = SCREEN_WIDTH;  // set this value to the desired width of your window
            //graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;   // set this value to the desired height of your window
            //graphics.ApplyChanges();

            ScreenManager = new ScreenManager();
            Components.Add(ScreenManager);
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
            //graphics = new GraphicsDeviceManager(this);

            // TODO: When Monogame 3.8.1 releases, move back to constructor
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            ScreenManager.LoadScreen(new SplashScreen(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        internal static SpriteFont GameFont;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

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
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
