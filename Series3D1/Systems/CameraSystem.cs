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
    class CameraSystem : IUpdate
    {
        public CameraSystem()
        {

        }

        // position the camera behind the chopper
        public void Update(GameTime gametime)
        {
            ModelComponent mc = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities()));
            CameraComponent camcomp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities()));
            TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities()));
            TransformComponent cTransComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities()));
            // Vector3 cameraPosition = mc.chopperPosition; // new Vector3(-2f, 0.1f, -0.1f);

            //transComp.Position = Vector3.Transform(transComp.Position, Matrix.CreateFromQuaternion(transComp.QRotation));
            transComp.Position = cTransComp.Position + camcomp.CameraOffSet;
            //camerUp = Vector3.Transform(camerUp, Matrix.CreateFromQuaternion(cTransComp.QRotation));

            
            camcomp.View = Matrix.CreateLookAt(transComp.Position, cTransComp.Position, Vector3.Up);
            camcomp.Proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 4.0f / 3.0f, 0.2f, 500.0f);
        }

        public int Order()
        {
            return 0;
        }
    }
}
