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
    class SkyBoxSystem : IDraw, ILoadContent
    {
        SkyboxComponent skyBox;
        CameraComponent cameraComponent;
        TransformComponent transComp;
        Matrix World = Matrix.Identity;

        //sky = Content.Load<Model>("cube");
        //    skyBoxTexture = Content.Load<TextureCube>(skyboxTexture);
        //    skyBoxEffect = Content.Load<Effect>("Skybox");

        public void LoadContent()
        {
            Entity Cament = ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities());
            Entity sky = ComponentManager.Instance.GetEntityWithTag("Sky", SceneManager.Instance.GetActiveSceneEntities());

            //transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(sky);
            skyBox = ComponentManager.Instance.GetEntityComponent<SkyboxComponent>(sky);
           cameraComponent = ComponentManager.Instance.GetEntityComponent<CameraComponent>(Cament);
           
        }
        /// <summary>
        /// Does the actual drawing of the skybox with our skybox effect.
        /// There is no world matrix, because we're assuming the skybox won't
        /// be moved around.  The size of the skybox can be changed with the size
        /// variable.
        /// </summary>
        /// <param name="view">The view matrix for the effect</param>
        /// <param name="projection">The projection matrix for the effect</param>
        /// <param name="cameraPosition">The position of the camera</param>
        public void DrawSky(SkyboxComponent sky,SpriteBatch sb,GameTime gm)
        {
            // Go through each pass in the effect, but we know there is only one...
            foreach (EffectPass pass in sky.SkyboxEffect.CurrentTechnique.Passes)
            {
                // Draw all of the components of the mesh, but we know the cube really
                // only has one mesh
                foreach (ModelMesh mesh in sky.SkyboxModel.Meshes)
                {
                    // Assign the appropriate values to each of the parameters
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = sky.SkyboxEffect;
                        sky.SkyboxEffect.Parameters["World"].SetValue(
                            Matrix.CreateScale(skyBox.Size) * Matrix.CreateTranslation(cameraComponent.CameraPosition));
                        sky.SkyboxEffect.Parameters["View"].SetValue(cameraComponent.View);
                        sky.SkyboxEffect.Parameters["Projection"].SetValue(cameraComponent.Proj);
                        sky.SkyboxEffect.Parameters["SkyBoxTexture"].SetValue(skyBox.SkyboxTextureCube);
                        sky.SkyboxEffect.Parameters["CameraPosition"].SetValue(cameraComponent.CameraPosition);
                    }

                    // Draw the mesh with the skybox effect
                    mesh.Draw();
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Entity modEntity = ComponentManager.Instance.GetEntityWithTag("Sky", SceneManager.Instance.GetActiveSceneEntities());
            SkyboxComponent modComp = ComponentManager.Instance.GetEntityComponent<SkyboxComponent>(modEntity);

            RasterizerState originalRasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.CullClockwiseFace;
            spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;

            DrawSky(skyBox,spriteBatch, gameTime);
        }

        public int Order()
        {
            return 1;
        }
    }
}
