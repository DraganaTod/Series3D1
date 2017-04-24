using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Series3D1.Managers;
using Series3D1.Entities;
using Series3D1.Components;
using Series3D1.Systems;
using System.Collections.Generic;

namespace Series3D1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
         GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Test number uno :) ";
            // initialize camera start position

            SystemManager.Instance.RegisterSystem("game", new TransformSystem());
            SystemManager.Instance.RegisterSystem("game", new CameraSystem());
            SystemManager.Instance.RegisterSystem("game", new ModelSystem());
            SystemManager.Instance.RegisterSystem("game", new HeightmapSystem());

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
            AddEntitiesAndComponents();
            SystemManager.Instance.RunLoadContentSystems();
            //load heightMap and heightMapTexture to create landscape
            base.LoadContent();
        }
        /// <summary>
        /// Adds all the entities and components to the engine.
        /// Also sets the name of the scene and sets it active
        /// </summary>
        public void AddEntitiesAndComponents()
        {
            Entity chopper = new Entity();
            SceneManager.Instance.AddEntityToScene("game", chopper);
            ComponentManager.Instance.AddComponentToEntity(chopper, new TagComponent("chopper"));
            ComponentManager.Instance.AddComponentToEntity(chopper, new ModelComponent(Content.Load<Model>("Chopper")));
            ComponentManager.Instance.AddComponentToEntity(chopper, new TransformComponent(new Vector3(-5, 0, 10), Quaternion.Identity, new Vector3(0.5f, 0.5f, 0.5f)));

            Entity camera = new Entity();
            Vector3 pos = new Vector3(-100, 0, 0);
            SceneManager.Instance.AddEntityToScene("game", camera);
            ComponentManager.Instance.AddComponentToEntity(camera, new TagComponent("camera"));
            ComponentManager.Instance.AddComponentToEntity(camera, new CameraComponent(
                Matrix.CreateLookAt(new Vector3(-100, 0, 0), Vector3.Zero, Vector3.Up), Matrix.CreatePerspective(1.2f, 0.9f, 1.0f, 1000.0f), new Vector3(0, 1, 10)));
            ComponentManager.Instance.AddComponentToEntity(camera, new TransformComponent(pos, Quaternion.CreateFromYawPitchRoll(0.02f, 0.02f, 0.02f), new Vector3()));

            Entity heightmap = new Entity();
            SceneManager.Instance.AddEntityToScene("game", heightmap);
            ComponentManager.Instance.AddComponentToEntity(heightmap, new TagComponent("heightmap"));
            ComponentManager.Instance.AddComponentToEntity(heightmap, new HeightmapComponent(Content.Load<Texture2D>("US_Canyon"), Content.Load<Texture2D>("mntn_canyon_d"), graphics.GraphicsDevice));
            ComponentManager.Instance.AddComponentToEntity(heightmap, new TransformComponent(new Vector3(0, -100, 256), Quaternion.Identity, new Vector3()));
            SceneManager.Instance.ActiveScene = "game";
            SystemManager.Instance.ActiveCategory = "game";

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
            SystemManager.Instance.RunUpdateSystems(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SystemManager.Instance.RunDrawSystems(spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
