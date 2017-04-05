using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Series3D1
{
   // bigprop = 0
   //body = 1
   //litleprop 2
    public class Game1 : Game
    {
         GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Model model;

        //-------------CAMERA------------------
        Camera camera;

        //-------------TERRAIN-----------------
        Terrain landscape;
        Vector3 rotation;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

  
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Test numero uno :) ";
            // initialize camera start position
            camera = new Camera(new Vector3(-100, 0, 0), Vector3.Zero, new Vector3(2, 2, 2), new Vector3(0, -100, 256));

            // initialize terrain
            landscape = new Terrain(GraphicsDevice);

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            rotation = Vector3.Zero;
            //load heightMap and heightMapTexture to create landscape
            landscape.SetHeightMapData(Content.Load<Texture2D>("US_Canyon"), Content.Load<Texture2D>("mntn_white_d"));
            model = Content.Load<Model>("Chopper");
            foreach(ModelMesh mm in model.Meshes)
            {
                foreach(Effect e in mm.Effects)
                {
                    IEffectLights ieLight = e as IEffectLights;
                    if(ieLight != null)
                    {
                        ieLight.EnableDefaultLighting();
                    }

                }
            }
        }

     
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            //rotera stora propellern
            rotation.Y += 0.6f;
            model.Bones[1].Transform = Matrix.CreateTranslation(model.Bones[1].Transform.Translation) * Matrix.CreateRotationY(rotation.Y);
            rotation.X += .1f;
            rotation.Z += .1f;
            model.Bones[0].Transform = Matrix.CreateTranslation(model.Bones[0].Transform.Translation);
            model.Bones[3].Transform = Matrix.CreateRotationX(rotation.X)* Matrix.CreateTranslation(model.Bones[3].Transform.Translation);


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

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           
            //by the book
            float radius = GetMaxMeshRadius(model);
            Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1.0f, 100.0f);
         
            Matrix view = Matrix.CreateLookAt(new Vector3(-1, 1, 5), Vector3.Zero, Vector3.Up);
            
            // to get landscape viewable
            camera.Draw(landscape);
            DrawModel(model, radius, proj, view);
            base.Draw(gameTime);
        }
        //by the book
        private void DrawModelViaMeshes(Model m, float radius, Matrix proj, Matrix view)
        {
            Matrix world = Matrix.CreateScale(1.0f / radius);
            foreach(ModelMesh mesh in model.Meshes)
            {
                foreach(Effect e in mesh.Effects)
                {
                    IEffectMatrices iEffectMatrices = e as IEffectMatrices;
                    if(iEffectMatrices != null)
                    {
                        iEffectMatrices.World = GetParentTransform(m, mesh.ParentBone) * world;
                        iEffectMatrices.Projection = proj;
                        iEffectMatrices.View = view;
                    }
                }
                mesh.Draw();

            }
        }

        //by the book
        //Combine bones transform with all of its parents. It stops when it gets to the root bone
        //because it has no parent and simply return the root bone's transform
        private Matrix GetParentTransform(Model m, ModelBone mb)
        {
            return (mb == m.Root) ? mb.Transform :
                mb.Transform * GetParentTransform(m, mb.Parent);
        }

        private void DrawModel(Model m, float radius, Matrix proj, Matrix view)
        {
            m.Draw(Matrix.CreateScale(1.0f / radius), view, proj);
        }
        //by the book
        private float GetMaxMeshRadius(Model m)
        {
            float radius = 0.0f;
            foreach(ModelMesh mesh in m.Meshes)
            {
                if(mesh.BoundingSphere.Radius > radius)
                {
                    radius = mesh.BoundingSphere.Radius;
                }
            }
            return radius;
        }
    }
}
