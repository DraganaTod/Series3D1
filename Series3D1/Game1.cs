using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Series3D1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
         GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //-------------CAMERA------------------
        Camera camera;

        //-------------TERRAIN-----------------
        Terrain landscape;
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
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Test number uno :) ";
            // initialize camera start position
            camera = new Camera(new Vector3(-100, 0, 0), Vector3.Zero, new Vector3(2, 2, 2), new Vector3(0, -100, 256));

            // initialize terrain
            landscape = new Terrain(GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load heightMap and heightMapTexture to create landscape
            landscape.SetHeightMapData(Content.Load<Texture2D>("US_Canyon"), Content.Load<Texture2D>("adesert_mntn_d"));
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
            // move camera position with keyboard
            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.A))
            {
                camera.Update(1);
            }
            if (key.IsKeyDown(Keys.D))
            {
                camera.Update(2);
            }
            if (key.IsKeyDown(Keys.W))
            {
                camera.Update(3);
            }
            if (key.IsKeyDown(Keys.S))
            {
                camera.Update(4);
            }
            if (key.IsKeyDown(Keys.F))
            {
                camera.Update(5);
            }
            if (key.IsKeyDown(Keys.R))
            {
                camera.Update(6);
            }
            if (key.IsKeyDown(Keys.Q))
            {
                camera.Update(7);
            }
            if (key.IsKeyDown(Keys.E))
            {
                camera.Update(8);
            }
            if (key.IsKeyDown(Keys.G))
            {
                camera.Update(9);
            }
            if (key.IsKeyDown(Keys.T))
            {
                camera.Update(10);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // to get landscape viewable
            camera.Draw(landscape);

            base.Draw(gameTime);
        }
    }
}
