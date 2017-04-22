using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Series3D1.Managers;
using Series3D1.Components;
using Series3D1.Entities;
using Microsoft.Xna.Framework.Input;

namespace Series3D1.Systems
{
    class ModelSystem : IDraw, ILoadContent, IUpdate
    {
        ModelComponent modComp;
        CameraComponent cameraComponent;
        public void LoadContent()
        {
            Entity Cament = ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities());
            Entity Chopent = ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities());
           
            modComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(Chopent);
            cameraComponent = ComponentManager.Instance.GetEntityComponent<CameraComponent>(Cament);
            //foreach (ModelMesh mm in modComp.Model.Meshes)
            //{
            //    foreach (Effect e in mm.Effects)
            //    {
            //        IEffectLights ieLight = e as IEffectLights;
            //        if (ieLight != null)
            //        {
            //            ieLight.EnableDefaultLighting();
            //        }

            //    }
           // }
        }
        public void Movement(GameTime gameTime)
        {
            float speed = gameTime.ElapsedGameTime.Milliseconds / 500.0f * 1.0f;
            Vector3 addVector = Vector3.Transform(new Vector3(0, 0, -1), modComp.chopperRotation);


            KeyboardState key = Keyboard.GetState();
            foreach (Keys k in key.GetPressedKeys())
                switch (k)
                {
                    case Keys.A:
                        modComp.chopperPosition += addVector * speed;
                    
                        break;
                    case Keys.D:
                        modComp.tempMovement.X = -cameraComponent.Movement.X;
                        break;
                    case Keys.W:
                        modComp.tempMovement.Y = -cameraComponent.Movement.Y;
                        break;
                    case Keys.S:
                        modComp.tempMovement.Y = +cameraComponent.Movement.Y;
                        break;
                    case Keys.F:
                        modComp.tempMovement.Z = -cameraComponent.Movement.Z;
                        break;
                    case Keys.R:
                        modComp.tempMovement.Z = +cameraComponent.Movement.Z;
                        break;
                    case Keys.Q:
                    //    tempRotation.Y = -camcomp.Movement.Y * 0.02f;
                    //    break;
                    //case Keys.E:
                    //    tempRotation.Y = +camcomp.Movement.Y * 0.02f;
                    //    break;
                    //case Keys.G:
                    //    tempRotation.X = -camcomp.Movement.X * 0.02f;
                    //    break;
                    //case Keys.T:
                    //    tempRotation.X = +camcomp.Movement.X * 0.02f;
                    //    break;
                    default:
                        break;
                }
             
            cameraComponent.View = cameraComponent.View * Matrix.CreateRotationX(modComp.tempRotation.X) * Matrix.CreateRotationY(modComp.tempRotation.Y) * Matrix.CreateTranslation(modComp.tempMovement);
           // modComp. =  Matrix.CreateRotationX(modComp.tempRotation.X) * Matrix.CreateRotationY(modComp.tempRotation.Y) * Matrix.CreateTranslation(modComp.tempMovement);
    }

        //private void DrawModelViaMeshes(ModelComponent m, float radius, Matrix proj, Matrix view)
        //{
        //    Matrix world = Matrix.CreateScale(1.0f / radius);
        //    foreach (ModelMesh mesh in m.Model.Meshes)
        //    {
        //        foreach (Effect e in mesh.Effects)
        //        {
        //            IEffectMatrices iEffectMatrices = e as IEffectMatrices;
        //            if (iEffectMatrices != null)
        //            {
        //                iEffectMatrices.World = GetParentTransform(m, mesh.ParentBone) * world;
        //                iEffectMatrices.Projection = proj;
        //                iEffectMatrices.View = view;
        //            }
        //        }
        //        mesh.Draw();

        //    }
        //}

        private Matrix GetParentTransform(ModelComponent m, ModelBone mb)
        {
            return (mb == m.Model.Root) ? mb.Transform :
                mb.Transform * GetParentTransform(m, mb.Parent);
        }

        //private void DrawModel(Model m, float radius, Matrix proj, Matrix view)
        //{
        //    m.Draw(Matrix.CreateScale(1.0f / radius), view, proj);
        //}
        //by the book
        private float GetMaxMeshRadius(ModelComponent m)
        {
            float radius = 0.0f;
            foreach (ModelMesh mesh in m.Model.Meshes)
            {
                if (mesh.BoundingSphere.Radius > radius)
                {
                    radius = mesh.BoundingSphere.Radius;
                }
            }
            return radius;
        }
        public void DrawModel(ModelComponent mc, GameTime gameTime, SpriteBatch spriteBatch)
        {
           // Matrix world = Matrix.CreateScale(0.0005f, 0.0005f, 0.0005f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateFromQuaternion(mc.chopperRotation) * Matrix.CreateTranslation(mc.chopperPosition);

            Matrix[] chopperTransforms = new Matrix[mc.Model.Bones.Count];
            mc.Model.CopyAbsoluteBoneTransformsTo(chopperTransforms);
            float radius = GetMaxMeshRadius(mc);
            // Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, spriteBatch.GraphicsDevice.Viewport.AspectRatio, 1.0f, 100.0f);
            //Matrix view = Matrix.CreateLookAt(new Vector3(0, 1, 5), Vector3.Zero, Vector3.Up);
            // Matrix world = Matrix.CreateScale(1.0f / radius);
         //   Matrix world = new Matrix();
             Matrix world = Matrix.CreateScale(0.5f, 0.5f, 0.5f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateFromQuaternion(mc.chopperRotation) * Matrix.CreateTranslation(mc.chopperPosition);

            foreach (ModelMesh mesh in mc.Model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    e.World = GetParentTransform(mc, mesh.ParentBone) * world;

                    e.View = cameraComponent.View;
                    e.Projection = cameraComponent.Proj;

                    e.EnableDefaultLighting();
                    e.PreferPerPixelLighting = true;
                    foreach (EffectPass pass in e.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        mesh.Draw();
                    }
                    //IEffectMatrices iEffectMatrices = e as IEffectMatrices;
                    //if (iEffectMatrices != null)
                    //{
                    //    iEffectMatrices.World = GetParentTransform(mc, mesh.ParentBone) * world;
                    //   // iEffectMatrices.World = chopperTransforms[mesh.ParentBone.Index] * world;
                    //    iEffectMatrices.Projection = cameraComponent.Proj;
                    //    iEffectMatrices.View = cameraComponent.View;
                    //}
                }
                //mesh.Draw();

            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Entity modEntity = ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities());
            ModelComponent modComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(modEntity);

            spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            //by the book
            //float radius = GetMaxMeshRadius(modComp);
            //Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, spriteBatch.GraphicsDevice.Viewport.AspectRatio, 1.0f, 100.0f);

            //Matrix view = Matrix.CreateLookAt(new Vector3(0, 1, 5), Vector3.Zero, Vector3.Up);

            // to get landscape viewable
            //DrawModel(modComp, radius, proj, view);
            DrawModel(modComp, gameTime, spriteBatch);
        }

        public int Order()
        {
            return 2;
        }

        public void Update(GameTime gametime)
        {
            Movement(gametime);
        }

        //public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    DrawModel(modComp, gameTime, spriteBatch);
        //}
    }
}
