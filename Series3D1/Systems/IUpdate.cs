using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Systems
{
    public interface IUpdate : ISystem
    {
        void Update(GameTime gametime);
    }
}
