using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
    class InputComponent : IComponent
    {
        public List<int> KeyPressed { get; set; }

        public InputComponent(List<int> keyPressed)
        {
            KeyPressed = keyPressed;
        }
    }
}
