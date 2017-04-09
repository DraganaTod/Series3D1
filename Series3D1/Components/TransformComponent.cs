using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
    class TransformComponent : IComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scaling { get; set; }

        public TransformComponent(Vector3 pos, Vector3 rot, Vector3 scal)
        {
            Position = pos;
            Rotation = rot;
            Scaling = scal;
        }
    }
}
