using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
  public  class CameraComponent : IComponent
    {
        public Matrix View { get; set; }
        public Matrix Proj { get; set; }
        public Vector3 CameraOffSet { get; set; }
        public float distance = 20;
        public Vector3 CameraPosition; 

        public CameraComponent( Matrix view, Matrix proj, Vector3 offset)
        {
            View = Matrix.CreateLookAt(new Vector3(-100, 0, 0), Vector3.Zero, Vector3.Up);
            Proj = Matrix.CreatePerspective(1.2f, 0.9f, 1.0f, 1000.0f);
            CameraOffSet = offset;
            CameraPosition = distance * new Vector3((float)Math.Sin(0), 0, (float)Math.Cos(0));
        }

    }
}
