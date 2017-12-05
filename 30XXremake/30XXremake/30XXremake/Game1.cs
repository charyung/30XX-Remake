using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _30XXremake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        animation whirlpool;
        fighter omastar;
        stage tt;
        omastar sldf;

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            whirlpool = new animation(Content.Load<Texture2D>("textures/whirlpool2"), 3, 10, 60, 10);
            whirlpool.Position = new Vector2(100, 100);

            omastar = new fighter(new Vector2(200, 10), "textures/omastar", 1, 31, 28);
            physics.addToCollisions(omastar, omastar.hitbox);

            tt = new stage(Content.Load<Texture2D>("textures/temporalTower"), new Rectangle(27, 132, 637, 144));
            physics.StageHitbox = tt.hbRect;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            whirlpool.animate(gameTime);

            omastar.movement(gameTime);
            omastar.hitbox.X = (int)omastar.Position.X;
            omastar.hitbox.Y = (int)omastar.Position.Y;
            omastar.update(gameTime);

            //physics.collisions();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            //spriteBatch.Draw(Content.Load<Texture2D>(omastar.getSprite()), new Vector2(omastar.getX(), omastar.getY()), Color.White);
            spriteBatch.Draw(whirlpool.SpriteTexture, whirlpool.Position, whirlpool.SourceRect, Color.White, 0f, whirlpool.Origin, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(Content.Load<Texture2D>(omastar.Sprite), omastar.Position, Color.White);
            spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/Courier New"), omastar.hitbox.Intersects(tt.hbRect).ToString(), new Vector2(200, 10), Color.Black);
            spriteBatch.Draw(tt.Img, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
