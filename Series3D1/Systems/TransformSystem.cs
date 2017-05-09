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
        float rot = 0.6f;
        public int Order()
        {
            throw new NotImplementedException();
        }

        void IUpdate.Update(GameTime gametime)
        {
            List<Entity> entities = SceneManager.Instance.GetActiveSceneEntities();
            foreach (Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<TransformComponent>())
            {
                TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);

                transComp.SRTMatrix = Matrix.CreateScale(transComp.Scaling) * Matrix.CreateFromQuaternion(transComp.QRotation) * Matrix.CreateTranslation(transComp.Position);
            }
            //foreach(Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<ModelComponent>())
            //{
            //    ModelComponent modelComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ent);
            //    TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);

            //    rot += 0.6f;
            //    //kroppen
            //    modelComp.Model.Bones[0].Transform = Matrix.CreateTranslation(modelComp.Model.Bones[0].Transform.Translation);
            //    //rotera stora propellern
            //    modelComp.Model.Bones[1].Transform = Matrix.CreateTranslation(modelComp.Model.Bones[1].Transform.Translation) * Matrix.CreateRotationY(rot);
            //    //lilla
            //    modelComp.Model.Bones[3].Transform = Matrix.CreateFromYawPitchRoll(5f, 0, 0) * modelComp.Model.Bones[3].Transform;   
            //}
        }
    }
}
