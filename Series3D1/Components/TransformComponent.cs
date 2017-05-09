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
        public Vector3 Movement { get; set; }
        public Vector3 Scaling { get; set; }
        public Matrix SRTMatrix { get; set; }
        public Matrix CalcMatrix { get; set; }
        public Quaternion QRot { get; set; }
        public Quaternion QRotation = Quaternion.Identity;

        public TransformComponent(Vector3 pos, Quaternion rot, Vector3 scal)
        {
            QRotation = rot;
            Position = pos;
            Scaling = scal;
        }
    }
}
