using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series3D1.Components;
using Series3D1.Managers;
using Series3D1.Entities;

namespace Series3D1.Systems
{
    public struct MyOwnVertexFormat
    {
        public Vector3 position;
        public Vector2 texCoord;
        public Vector3 normal;
        public MyOwnVertexFormat(Vector3 position, Vector2 texCoord, Vector3 normal)
        {
            this.position = position;
            this.texCoord = texCoord;
            this.normal = normal;

        }
        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
             (
                 new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                 new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
                 new VertexElement(sizeof(float) * (3 + 2), VertexElementFormat.Vector3, VertexElementUsage.Normal, 0));

    }
    class HeightmapSystem : ILoadContent, IDraw
    {
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
       
        //heithMap
        //array to read heightMap data
        
        //CameraComponent camcomp = new CameraComponent(new Vector3(2, 2, 2),
        //        Matrix.CreateLookAt(new Vector3(-100, 0, 0), Vector3.Zero, Vector3.Up), Matrix.CreatePerspective(1.2f, 0.9f, 1.0f, 1000.0f));
        //method for getting out textures
        private void SetEffects(HeightmapComponent hmComp)
        {
            CameraComponent camcomp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities()));


            hmComp.Effect.Texture = hmComp.HeightMapTexture;
            hmComp.Effect.FogEnabled = false;

            //ändra till false om man vill se trianglarna
            hmComp.Effect.TextureEnabled = true;
            hmComp.Effect.View = camcomp.View;
            hmComp.Effect.Projection = camcomp.Proj;   //Matrix.CreatePerspective(1.2f, 0.9f, 1.0f, 1000.0f);
            hmComp.Effect.World = hmComp.World;
            //// draw those triangles
            //RasterizerState state = new RasterizerState();
            //state.FillMode = FillMode.WireFrame;
            //graphicsDevice.RasterizerState = state;

        }

        private void SetIndices(HeightmapComponent hmComp)
        {
            // amount of triangles
            hmComp.Indices = new int[6 * (hmComp.Width - 1) * (hmComp.Height - 1)];
            int number = 0;
            // collect data for corners
            for (int y = 0; y < hmComp.Height - 1; y++)
                for (int x = 0; x < hmComp.Width - 1; x++)
                {
                    // create double triangles
                    hmComp.Indices[number] = x + (y + 1) * hmComp.Width;      // up left
                    hmComp.Indices[number + 1] = x + y * hmComp.Width + 1;        // down right
                    hmComp.Indices[number + 2] = x + y * hmComp.Width;            // down left
                    hmComp.Indices[number + 3] = x + (y + 1) * hmComp.Width;      // up left
                    hmComp.Indices[number + 4] = x + (y + 1) * hmComp.Width + 1;  // up right
                    hmComp.Indices[number + 5] = x + y * hmComp.Width + 1;        // down right
                    number += 6;
                }
        }
        public void setVer(HeightmapComponent hc, SpriteBatch sp)
        {
            MyOwnVertexFormat[] vertices = new MyOwnVertexFormat[18];

            vertices[0] = new MyOwnVertexFormat(new Vector3(-20, 0, 10), new Vector2(-0.25f, 25.0f), new Vector3(0, 1, 0));
            vertices[1] = new MyOwnVertexFormat(new Vector3(-20, 0, -100), new Vector2(-0.25f, 0.0f), new Vector3(0, 1, 0));
            vertices[2] = new MyOwnVertexFormat(new Vector3(2, 0, 10), new Vector2(0.25f, 25.0f), new Vector3(0, 1, 0));
            vertices[3] = new MyOwnVertexFormat(new Vector3(2, 0, -100), new Vector2(0.25f, 0.0f), new Vector3(0, 1, 0));
            vertices[4] = new MyOwnVertexFormat(new Vector3(2, 0, 10), new Vector2(0.25f, 25.0f), new Vector3(-1, 0, 0));
            vertices[5] = new MyOwnVertexFormat(new Vector3(2, 0, -100), new Vector2(0.25f, 0.0f), new Vector3(-1, 0, 0));
            vertices[6] = new MyOwnVertexFormat(new Vector3(2, 1, 10), new Vector2(0.375f, 25.0f), new Vector3(-1, 0, 0));
            vertices[7] = new MyOwnVertexFormat(new Vector3(2, 1, -100), new Vector2(0.375f, 0.0f), new Vector3(-1, 0, 0));
            vertices[8] = new MyOwnVertexFormat(new Vector3(2, 1, 10), new Vector2(0.375f, 25.0f), new Vector3(0, 1, 0));
            vertices[9] = new MyOwnVertexFormat(new Vector3(2, 1, -100), new Vector2(0.375f, 0.0f), new Vector3(0, 1, 0));
            vertices[10] = new MyOwnVertexFormat(new Vector3(3, 1, 10), new Vector2(0.5f, 25.0f), new Vector3(0, 1, 0));
            vertices[11] = new MyOwnVertexFormat(new Vector3(3, 1, -100), new Vector2(0.5f, 0.0f), new Vector3(0, 1, 0));
            vertices[12] = new MyOwnVertexFormat(new Vector3(13, 1, 10), new Vector2(0.75f, 25.0f), new Vector3(0, 1, 0));
            vertices[13] = new MyOwnVertexFormat(new Vector3(13, 1, -100), new Vector2(0.75f, 0.0f), new Vector3(0, 1, 0));
            vertices[14] = new MyOwnVertexFormat(new Vector3(13, 1, 10), new Vector2(0.75f, 25.0f), new Vector3(-1, 0, 0));
            vertices[15] = new MyOwnVertexFormat(new Vector3(13, 1, -100), new Vector2(0.75f, 0.0f), new Vector3(-1, 0, 0));
            vertices[16] = new MyOwnVertexFormat(new Vector3(13, 21, 10), new Vector2(1.25f, 25.0f), new Vector3(-1, 0, 0));
            vertices[17] = new MyOwnVertexFormat(new Vector3(13, 21, -100), new Vector2(1.25f, 0.0f), new Vector3(-1, 0, 0));

            vertexBuffer = new VertexBuffer(sp.GraphicsDevice, MyOwnVertexFormat.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);
        }

        private void SetVertices(HeightmapComponent hmComp)
        {
            hmComp.Vertices = new VertexPositionTexture[hmComp.Width * hmComp.Height];
            Vector2 texturePosition;
            for (int x = 0; x < hmComp.Width; x++)
            {
                for (int y = 0; y < hmComp.Height; y++)
                {
                    texturePosition = new Vector2((float)x / 25.5f, (float)y / 25.5f);
                    hmComp.Vertices[x + y * hmComp.Width] = new VertexPositionTexture(new Vector3(x, hmComp.heightMapData[x, y], -y), texturePosition);
                }

                // graphicsDevice.VertexDeclaration = new VertexDeclaration(graphicsDevice, VertexPositionTexture.VertexElements);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hmComp"></param>
        public void SetHeights(HeightmapComponent hmComp)
        {
            Color[] greyValues = new Color[hmComp.Width * hmComp.Height];
            hmComp.HeightMap.GetData(greyValues);
            hmComp.heightMapData = new float[hmComp.Width, hmComp.Height];

            for (int x = 0; x < hmComp.Width; x++)
            {
                for (int y = 0; y < hmComp.Height; y++)
                {
                    hmComp.heightMapData[x, y] = 0;
                    //hmComp.heightMapData[x, y] = greyValues[x + y * hmComp.Width].G / 3.1f;
                }
            }
        }
        /// <summary>
        /// draws the heightmap
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Entity hmEntity = ComponentManager.Instance.GetEntityWithTag("heightmap", SceneManager.Instance.GetActiveSceneEntities());
            HeightmapComponent hmComp = ComponentManager.Instance.GetEntityComponent<HeightmapComponent>(hmEntity);

            hmComp.Effect.CurrentTechnique.Passes[0].Apply();
            SetEffects(hmComp);
            foreach (EffectPass pass in hmComp.Effect.CurrentTechnique.Passes)
            {

                //pass.Begin();
                pass.Apply();
                spriteBatch.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, hmComp.Vertices, 0, hmComp.Vertices.Length, hmComp.Indices, 0, hmComp.Indices.Length / 3);
                //spriteBatch.GraphicsDevice.Indices = indexBuffer;
                spriteBatch.GraphicsDevice.SetVertexBuffer(vertexBuffer);
                //spriteBatch.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, hmComp.Vertices.Length, 0, hmComp.Indices.Length / 3);

                // pass.End();
            }
        }

        public void LoadContent()
        {
            Entity hmEntity = ComponentManager.Instance.GetEntityWithTag("heightmap", SceneManager.Instance.GetActiveSceneEntities());
            HeightmapComponent hmComp = ComponentManager.Instance.GetEntityComponent<HeightmapComponent>(hmEntity);
            TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(hmEntity);
            CameraComponent camComp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(hmEntity);

            hmComp.World = Matrix.CreateTranslation(transComp.Position);
            SetHeights(hmComp);
            SetVertices(hmComp);
            SetIndices(hmComp);
            //SetEffects(hmComp, camComp);
             CopyToBuffer(hmComp.device, hmComp);
            
        }
        /// <summary>
        /// Method for setting up vertex and index buffers
        /// </summary>
        /// <param name="device"></param>
        /// <param name="hmComp"></param>
        private void CopyToBuffer(GraphicsDevice device, HeightmapComponent hmComp)
        {
            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionTexture), hmComp.Vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionTexture>(hmComp.Vertices);

            indexBuffer = new IndexBuffer(device, typeof(int), hmComp.Indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(hmComp.Indices);
        }

        public int Order()
        {
            return 6;
        }
    }
}
