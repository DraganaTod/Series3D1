using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Entities
{
    class Scene
    {
        List<Entity> scene;

        public Scene()
        {
            scene = new List<Entity>();
        }

        public void AddEntityToScene(Entity entity)
        {
            scene.Add(entity);
        }

        public List<Entity> GetAllEntities()
        {
            return scene;       
        }
    }
}
