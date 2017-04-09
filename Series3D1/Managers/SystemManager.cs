using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Series3D1.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Managers
{
    class SystemManager
    {
        Dictionary<string, Dictionary<Type, ISystem>> IDrawDict = new Dictionary<string, Dictionary<Type, ISystem>>();
        Dictionary<string, Dictionary<Type, ISystem>> IUpdateDict = new Dictionary<string, Dictionary<Type, ISystem>>();

        public string ActiveCategory { get; set; }

        private static SystemManager instance;
        private SystemManager() { }

        public static SystemManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SystemManager();
                return instance;
            }
        }

        public void RegisterSystem(string category, ISystem system)
        {
            if (system is Systems.IDrawable)
            {
                if (!IDrawDict.ContainsKey(category))
                {
                    IDrawDict.Add(category, new Dictionary<Type, ISystem>());
                }
                IDrawDict[category].Add(system.GetType(), system);
            }

            if (system is IUpdate)
            {
                if (!IUpdateDict.ContainsKey(category))
                {
                    IUpdateDict.Add(category, new Dictionary<Type, ISystem>());
                }
                IUpdateDict[category].Add(system.GetType(), system);
            }
        }

        public void RunRenderSystems(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IDrawDict.ContainsKey(ActiveCategory))
            {
                foreach (Systems.IDrawable rensys in IDrawDict[ActiveCategory].Values)
                {
                    rensys.Draw(spriteBatch, gameTime);
                }
            }
        }

        public void RunUpdateSystems(GameTime gameTime)
        {
            if (IUpdateDict.ContainsKey(ActiveCategory))
            {
                foreach (IUpdate updateSys in IUpdateDict[ActiveCategory].Values)
                {
                    updateSys.Update(gameTime);
                }
            }
        }
    }
}
