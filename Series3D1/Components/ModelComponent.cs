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
        public Vector3 chopperPosition = new Vector3(-5, 0, 10);
        public Vector3 tempMovement = Vector3.Zero;
        public Vector3 tempRotation = Vector3.Zero;
        public Vector3 movement = new Vector3(2, 2, 2);
       

        public Model Model { get; set; }
        public BasicEffect Effect { get; set; }
        

        public ModelComponent(Model model, Vector3 pos)
        {
            chopperPosition = pos;
            Model = model;
           // Effect = new BasicEffect(gd);
            
        }
        
    }
}
