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
        TransformComponent transComp;
        public void LoadContent()
        {
            Entity Cament = ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities());
            Entity Chopent = ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities());
            
            transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(Chopent);
            modComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(Chopent);
            cameraComponent = ComponentManager.Instance.GetEntityComponent<CameraComponent>(Cament);
        }
        public void Movement(GameTime gameTime)
        {
            float speed = gameTime.ElapsedGameTime.Milliseconds / 500.0f * 1.0f;
            Vector3 addVector = Vector3.Transform(new Vector3(0, 0, -1), transComp.QRotation);


            KeyboardState key = Keyboard.GetState();
            foreach (Keys k in key.GetPressedKeys())
            {
                switch (k)
                {
                    case Keys.A:
                        transComp.Position += new Vector3(-2f, 0, 0);
                        break;
                    case Keys.D:
                        transComp.Position += new Vector3(2f, 0, 0);
                        break;
                    case Keys.W:
                        transComp.Position += new Vector3(0, 2f, 0);
                        break;
                    case Keys.S:
                        transComp.Position += new Vector3(0, -2f, 0);
                        break;
                    case Keys.F:
                        transComp.Position += new Vector3(0, 0, 2f);
                        break;
                    case Keys.R:
                        transComp.Position += new Vector3(0, 0, -2f);
                        break;
                    case Keys.Q:
                        transComp.QRotation *= Quaternion.CreateFromYawPitchRoll(-0.02f, 0, 0);
                        break;
                    case Keys.E:
                        transComp.QRotation *= Quaternion.CreateFromYawPitchRoll(0.02f, 0, 0);
                        break;
                    case Keys.G:
                        transComp.QRotation *= Quaternion.CreateFromYawPitchRoll(0, -0.02f, 0);
                        break;
                    case Keys.T:
                        transComp.QRotation *= Quaternion.CreateFromYawPitchRoll(0, 0.02f, 0);
                        break;
                    case Keys.C:
                        transComp.QRotation *= Quaternion.CreateFromYawPitchRoll(0, 0, -0.02f);
                        break;
                    case Keys.V:
                        transComp.QRotation *= Quaternion.CreateFromYawPitchRoll(0, 0, 0.02f);
                        break;
                    default:
                        break;
                }
            }
        }

        private Matrix GetParentTransform(ModelComponent m, ModelBone mb)
        {
            return (mb == m.Model.Root) ? mb.Transform :
                mb.Transform * GetParentTransform(m, mb.Parent);
        }

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
            Matrix[] chopperTransforms = new Matrix[mc.Model.Bones.Count];
            mc.Model.CopyAbsoluteBoneTransformsTo(chopperTransforms);
            float radius = GetMaxMeshRadius(mc);
            foreach (ModelMesh mesh in mc.Model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    e.World = GetParentTransform(mc, mesh.ParentBone) * transComp.SRTMatrix;

                    e.View = cameraComponent.View;
                    e.Projection = cameraComponent.Proj;

                    e.EnableDefaultLighting();
                    e.PreferPerPixelLighting = true;
                    foreach (EffectPass pass in e.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        mesh.Draw();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Entity modEntity = ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities());
            ModelComponent modComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(modEntity);
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
    }
}
