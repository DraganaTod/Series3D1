using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Series3D1.Components
{
    class ModelComponent : IComponent
    {
        public Model Model { get; set; }

        public ModelComponent(Model model)
        {
            Model = model;
        }
    }
}
