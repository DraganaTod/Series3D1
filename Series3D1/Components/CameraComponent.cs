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
        public Vector3 Movement { get; set; } 
        public Matrix View { get; set; }
        public Matrix Proj { get; set; }
       public  Vector3 cameraPosition = new Vector3(-5, 0, 10);

        public CameraComponent( Matrix view, Matrix proj)
        {
            
            View = Matrix.CreateLookAt(new Vector3(-100, 0, 0), Vector3.Zero, Vector3.Up);
            Proj = Matrix.CreatePerspective(1.2f, 0.9f, 1.0f, 1000.0f);
        }

    }
}
