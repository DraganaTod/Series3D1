using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
    class TagComponent : IComponent
    {
        public string ID { get; set; }

        public TagComponent(string id)
        {
            ID = id;
        }
    }
}
