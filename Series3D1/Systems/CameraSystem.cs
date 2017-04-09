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
    class CameraSystem : IDraw, IUpdate
    {
        public CameraSystem()
        {

        }

        public void Update(GameTime gametime)
        {
            Vector3 tempMovement = Vector3.Zero;
            Vector3 tempRotation = Vector3.Zero;

            Entity camEntity = ComponentManager.Instance.GetEntityWithTag("Camera", SceneManager.Instance.GetActiveSceneEntities());
            CameraComponent camComp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camEntity);
            InputComponent inputComp = ComponentManager.Instance.GetEntityComponent<InputComponent>(camEntity);
            TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(camEntity);
            foreach (int number in inputComp.KeyPressed)
            {
                switch (number)
                {
                    case 1:
                        tempMovement.X = +camComp.Movement.X;
                        break;
                    case 2:
                        tempMovement.X = -camComp.Movement.X;
                        break;
                    case 3:
                        tempMovement.Y = -camComp.Movement.Y;
                        break;
                    case 4:
                        tempMovement.Y = +camComp.Movement.Y;
                        break;
                    case 5:
                        tempMovement.Z = -camComp.Movement.Z;
                        break;
                    case 6:
                        tempMovement.Z = +camComp.Movement.Z;
                        break;
                    case 7:
                        tempRotation.Y = -transComp.Rotation.Y;
                        break;
                    case 8:
                        tempRotation.Y = +transComp.Rotation.Y;
                        break;
                    case 9:
                        tempRotation.X = -transComp.Rotation.X;
                        break;
                    case 10:
                        tempRotation.X = +transComp.Rotation.X;
                        break;
                    case 11:
                        tempRotation.Z = -transComp.Rotation.Z;
                        break;
                    case 12:
                        tempRotation.Z = -transComp.Rotation.Z;
                        break;
                    default:
                        break;
                }
            }
            //move camera to new position

            camComp.View = camComp.View * Matrix.CreateRotationX(tempRotation.X) * Matrix.CreateRotationY(tempRotation.Y) * Matrix.CreateRotationZ(tempRotation.Z) * Matrix.CreateTranslation(tempMovement);
            //update position
            camComp.Movement += tempMovement;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
    }
}
