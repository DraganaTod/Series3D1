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

namespace Series3D1.Systems
{
    class ModelSystem : IDraw, ILoadContent
    {
        ModelComponent modComp;
        public void LoadContent()
        {
            Entity modEntity = ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities());
             modComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(modEntity);
            foreach (ModelMesh mm in modComp.Model.Meshes)
            {
                foreach (Effect e in mm.Effects)
                {
                    IEffectLights ieLight = e as IEffectLights;
                    if (ieLight != null)
                    {
                        ieLight.EnableDefaultLighting();
                    }

                }
            }
        }

        //private void DrawModelViaMeshes(Model m, float radius, Matrix proj, Matrix view)
        //{
        //    Matrix world = Matrix.CreateScale(1.0f / radius);
        //    foreach (ModelMesh mesh in m.Meshes)
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
        private float GetMaxMeshRadius(Model m)
        {
            float radius = 0.0f;
            foreach (ModelMesh mesh in m.Meshes)
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
            Matrix world = Matrix.CreateScale(0.0005f, 0.0005f, 0.0005f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateFromQuaternion(mc.chopperRotation) * Matrix.CreateTranslation(mc.chopperPosition);

            Matrix[] chopperTransforms = new Matrix[mc.Model.Bones.Count];
            mc.Model.CopyAbsoluteBoneTransformsTo(chopperTransforms);
            float radius = GetMaxMeshRadius(mc.Model);
             Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, spriteBatch.GraphicsDevice.Viewport.AspectRatio, 1.0f, 100.0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0, 1, 5), Vector3.Zero, Vector3.Up);
            foreach (ModelMesh mesh in mc.Model.Meshes)
            {
                foreach (Effect e in mesh.Effects)
                {
                    IEffectMatrices iEffectMatrices = e as IEffectMatrices;
                    if (iEffectMatrices != null)
                    {
                        iEffectMatrices.World = GetParentTransform(mc, mesh.ParentBone) * world;
                        iEffectMatrices.Projection = proj;
                        iEffectMatrices.View = view;
                    }
                }
                mesh.Draw();

            }
        }

        //public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
            //    Entity modEntity = ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities());
            //    ModelComponent modComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(modEntity);

            //    spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            //    //by the book
            //    float radius = GetMaxMeshRadius(modComp.Model);
            //    Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, spriteBatch.GraphicsDevice.Viewport.AspectRatio, 1.0f, 100.0f);

            //    Matrix view = Matrix.CreateLookAt(new Vector3(0, 1, 5), Vector3.Zero, Vector3.Up);

            //    // to get landscape viewable
            //    DrawModel(modComp.Model, radius, proj, view);
            
        //}

        public int Order()
        {
            return 2;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawModel(modComp, gameTime, spriteBatch);
        }
    }
}
