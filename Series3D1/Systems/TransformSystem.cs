using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Series3D1.Managers;
using Series3D1.Components;
using Series3D1.Entities;

namespace Series3D1.Systems
{
    class TransformSystem : IUpdate
    {
        public int Order()
        {
            throw new NotImplementedException();
        }

        void IUpdate.Update(GameTime gametime)
        {
            List<Entity> entities = SceneManager.Instance.GetActiveSceneEntities();
            foreach (Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<CameraComponent>())
            {
                CameraComponent camComp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ent);
                TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);

                camComp.View = camComp.View * Matrix.CreateRotationX(transComp.Rotation.X) * Matrix.CreateRotationY(transComp.Rotation.Y) * Matrix.CreateTranslation(transComp.Position);
            }
            foreach(Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<ModelComponent>())
            {
                ModelComponent modelComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ent);
                TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);

                //kroppen
                modelComp.Model.Bones[0].Transform = Matrix.CreateTranslation(modelComp.Model.Bones[0].Transform.Translation);
                //rotera stora propellern
                modelComp.Model.Bones[1].Transform = Matrix.CreateTranslation(modelComp.Model.Bones[1].Transform.Translation) * Matrix.CreateRotationY(transComp.Rotation.Y);
                //lilla
                modelComp.Model.Bones[3].Transform = Matrix.CreateFromYawPitchRoll(transComp.Rotation.Y, 0, 0) * modelComp.Model.Bones[3].Transform;
            }
            //foreach(Entity ent in entities)
            //{
            //    TransformComponent tc = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);
            //    ModelComponent mc = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ent);
            //    CameraComponent cc = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ent);
            //    ComponentManager.Instance.GetAllEntitiesWithCertainComp<Camera>
            //    if (tc == null || mc == null)
            //        continue;



            //}
            //rotera stora propellern

        }
    }
}
