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
    class CameraSystem : IDraw, IUpdate
    {
        
        public CameraSystem()
        {

        }

        // position the camera behind the chopper
        public void Update(GameTime gametime)
        {
                       
            //Vector3 tempMovement = Vector3.Zero;
            //Vector3 tempRotation = Vector3.Zero;

            Entity camEntity = ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities());
            CameraComponent camComp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camEntity);
            TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(camEntity);
            ModelComponent mc = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities()));

            Vector3 cameraPosition = new Vector3(0, 0.1f, 0.6f);
            Vector3 camerUp = new Vector3(0, 1, 0);
            cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateFromQuaternion(mc.chopperRotation));
            cameraPosition += mc.chopperPosition;
            camerUp = Vector3.Transform(camerUp, Matrix.CreateFromQuaternion(mc.chopperRotation));
            camComp.View = Matrix.CreateLookAt(cameraPosition, mc.chopperPosition, camerUp);
            //camComp.Proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 4.0f / 3.0f, 0.2f, 500.0f);
          
            //KeyboardState key = Keyboard.GetState();
            //foreach(Keys k in key.GetPressedKeys())
            //switch (k)
            //{
            //    case Keys.A:
            //        tempMovement.X = +camComp.Movement.X;
            //        break;
            //    case Keys.D:
            //        tempMovement.X = -camComp.Movement.X;
            //        break;
            //    case Keys.W:
            //        tempMovement.Y = -camComp.Movement.Y;
            //        break;
            //    case Keys.S:
            //        tempMovement.Y = +camComp.Movement.Y;
            //        break;
            //    case Keys.F:
            //        tempMovement.Z = -camComp.Movement.Z;
            //        break;
            //    case Keys.R:
            //        tempMovement.Z = +camComp.Movement.Z;
            //        break;
            //    case Keys.Q:
            //        tempRotation.Y = -camComp.Movement.Y * 0.02f;
            //        break;
            //    case Keys.E:
            //        tempRotation.Y = +camComp.Movement.Y * 0.02f;
            //        break;
            //    case Keys.G:
            //        tempRotation.X = -camComp.Movement.X * 0.02f;
            //        break;
            //    case Keys.T:
            //        tempRotation.X = +camComp.Movement.X * 0.02f;
            //        break;
            //    default:
            //        break;
            //}
            //move camera to new position
            //transComp.Rotation =  tempRotation;
            //transComp.Position =  tempMovement;
            //camComp.View = camComp.View * Matrix.CreateRotationX(tempRotation.X) * Matrix.CreateRotationY(tempRotation.Y)  * Matrix.CreateTranslation(tempMovement);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            ModelComponent mc = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities()));
            Entity ent = ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities());
            CameraComponent camcomp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ent);
            HeightmapComponent hcomp = ComponentManager.Instance.GetEntityComponent<HeightmapComponent>(ComponentManager.Instance.GetEntityWithTag("heightmap", SceneManager.Instance.GetActiveSceneEntities()));
            
             SetEffects(camcomp, hcomp);


        }
        public void SetEffects(CameraComponent camComp, HeightmapComponent hc)
        {
            hc.Effect.View = camComp.View;
            hc.Effect.Projection = camComp.Proj;
            hc.Effect.World = hc.World;
        }
        public void SetEffects(CameraComponent camComp, ModelComponent mc)
        {
            mc.Effect.View = camComp.View;
            mc.Effect.Projection = camComp.Proj;
            //mc.Effect.World = mc.World;

        }

        public int Order()
        {
            return 0;
        }
    }
}
