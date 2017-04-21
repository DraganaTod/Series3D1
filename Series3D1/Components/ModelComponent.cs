using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Series3D1.Components
{
    class ModelComponent : IComponent
    {
        public Vector3 chopperPosition = new Vector3(8, 1, -3);
        public Quaternion chopperRotation = Quaternion.Identity;
        GraphicsDevice gd;
        public Model Model { get; set; }
        public BasicEffect Effect { get; set; }

        public ModelComponent(Model model, GraphicsDevice gd)
        {
            Model = model;
            Effect = new BasicEffect(gd);
            
        }
        
    }
}
