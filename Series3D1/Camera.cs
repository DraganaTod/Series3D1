using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Series3D1
{
    class Camera
    {
        //matrix for camera view and projection
        Matrix viewMatrix;
        Matrix projectionMatrix;

        // world matrix for our landskape
        public Matrix terrainMatrix;

        //actual camera position, direction, movement and rotation
        Vector3 position;
        Vector3 direction;
        Vector3 movement;
        Vector3 rotation;

        public Camera(Vector3 position, Vector3 direction, Vector3 movement, Vector3 landskapePosition)
        {
            this.position = position;
            this.direction = direction;
            this.movement = movement;
            rotation = movement * 0.02f;

            //camera position, view of camera, see what is over camera
            viewMatrix = Matrix.CreateLookAt(position, direction, Vector3.Up);
            //width and height of camera near plane, range of camera far plane (1-1000)
            projectionMatrix = Matrix.CreatePerspective(1.2f, 0.9f, 1.0f, 1000.0f);
            //positioning our landskape in camera start position
            terrainMatrix = Matrix.CreateTranslation(landskapePosition);

        }

        public void Update(int number)
        {
            Vector3 tempMovement = Vector3.Zero;
            Vector3 tempRotation = Vector3.Zero;

            //left
            if(number == 1)
            {
                tempMovement.X = +movement.X;
            }
            //rigth
            if (number == 2)
            {
                tempMovement.X = -movement.X;
            }
            //up
            if (number == 3)
            {
                tempMovement.Y = -movement.Y;
            }
            //down
            if (number == 4)
            {
                tempMovement.Y = +movement.Y;
            }
            //backward/zoom put
            if(number == 5)
            {
                tempMovement.Z = -movement.Z;
            }
            //forward (zoomIn)
            if (number == 6)
            {
                tempMovement.Z = +movement.Z;
            }
            //left rotation
            if (number == 7)
            {
                tempRotation.Y = -rotation.Y;
            }
            //right rotation
            if (number == 8)
            {
                tempRotation.Y = +rotation.Y;
            }
            //forward rotation
            if (number == 9)
            {
                tempRotation.X = -rotation.X;
            }
            //backward rotation
            if (number == 10)
            {
                tempRotation.X = +rotation.X;
            }

            //move camera to new position
            viewMatrix = viewMatrix * Matrix.CreateRotationX(tempRotation.X) * Matrix.CreateRotationY(tempRotation.Y) * Matrix.CreateTranslation(tempMovement);
            //update position
            position += tempMovement;
            direction += tempRotation;
        }
        public void SetEffects(BasicEffect basicEffect)
        {
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;
            basicEffect.World = terrainMatrix;
        }
        public void Draw(Terrain terrain)
        {
            // terrain.basicEffect.Begin();
            terrain.basicEffect.CurrentTechnique.Passes[0].Apply();
            SetEffects(terrain.basicEffect);
            terrain.Draw();
            foreach (EffectPass pass in terrain.basicEffect.CurrentTechnique.Passes)
            {

                //pass.Begin();
                pass.Apply();
                terrain.Draw();


                // pass.End();
            }
            ////terrain.basicEffect.End();
        }
    }
}
