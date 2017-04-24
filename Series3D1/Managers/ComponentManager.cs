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

        //singelton
        public static ComponentManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ComponentManager();
                return instance;
            }
        }
        /// <summary>
        /// adds component to the given entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="component"></param>
        public void AddComponentToEntity(Entity entity, IComponent component)
        {
            Type type = component.GetType();
            if (!components.ContainsKey(type))
            {
                components.Add(type, new Dictionary<Entity, IComponent>());
            }
            components[type][entity] = component;
        }
        /// <summary>
        /// gets all the entities with given component type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<Entity> GetAllEntitiesWithCertainComp<T>() where T : class, IComponent
        {
            List<Entity> temp = new List<Entity>();
            Type type = typeof(T);
            if (!components.ContainsKey(type))
                return null;
            foreach(KeyValuePair<Entity, IComponent> pair in components[type])
            {
                temp.Add(pair.Key);
            }
            return temp;
        }
        /// <summary>
        /// get the component with given component type from the given entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T GetEntityComponent<T>(Entity entity) where T : class, IComponent
        {
            Type type = typeof(T);
            if (!components.ContainsKey(type))
                return null;
            if (components[type].ContainsKey(entity))
                return (T)components[type][entity];
            return null;
        }
        /// <summary>
        /// gets all the components with given component type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllSpecComponents<T>() where T : class, IComponent
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
        /// <summary>
        /// gets an entity with a given tag from the given entities
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
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
