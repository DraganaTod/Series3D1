using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
    class CameraComponent : IComponent
    {
        public Vector3 Movement { get; set; } 
        public Matrix View { get; set; }
        public Matrix Proj { get; set; }

        public CameraComponent(Vector3 mov, Matrix view, Matrix proj)
        {
            Movement = mov;
            View = view;
            Proj = proj;
        }
    }
}
