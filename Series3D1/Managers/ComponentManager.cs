using Series3D1.Components;
using Series3D1.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Managers
{
    class ComponentManager
    {
        Dictionary<Type, Dictionary<Entity, IComponent>> components = new Dictionary<Type, Dictionary<Entity, IComponent>>();

        private static ComponentManager instance;

        public static ComponentManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ComponentManager();
                return instance;
            }
        }
        public void AddComponentToEntity(Entity entity, IComponent component)
        {
            Type type = component.GetType();
            if (!components.ContainsKey(type))
            {
                components.Add(type, new Dictionary<Entity, IComponent>());
            }
            components[type][entity] = component;
        }

        public T GetEntityComponent<T>(Entity entity) where T : class, IComponent
        {
            Type type = typeof(T);
            if (!components.ContainsKey(type))
                return null;
            if (components[type].ContainsKey(entity))
                return (T)components[type][entity];
            return null;
        }
        public IEnumerable<T> GetAllSpecComponents<T>() where T : class, IComponent
        {
            List<T> temp = new List<T>();
            Type type = typeof(T);
            if (!components.ContainsKey(type))
                return null;
            foreach (KeyValuePair<Entity, IComponent> pair in components[type])
            {
                temp.Add((T)pair.Value);
            }
            return temp;
        }

        public Entity GetEntityWithTag(String tagName, List<Entity> entities)
        {
            foreach (Entity e in entities)
            {
                TagComponent t = GetEntityComponent<TagComponent>(e);
                if (t != null && t.ID.Equals(tagName))
                {
                    return e;
                }
            }
            return null;
        }
    }
}
