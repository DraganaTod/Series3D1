using Series3D1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Managers
{
    class SceneManager
    {
        public string ActiveScene { get; set; }
        private Dictionary<string, Scene> scenesDict = new Dictionary<string, Scene>(1);

        private static SceneManager instance;
        private SceneManager() { }

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                    instance.ActiveScene = null;
                }
                return instance;
            }
        }

        public void AddEntityToScene(string sceneName, Entity entity)
        {
            if (!scenesDict.ContainsKey(sceneName))
            {
                scenesDict.Add(sceneName, new Scene());
            }
            scenesDict[sceneName].AddEntityToScene(entity);
        }

        public List<Entity> GetActiveSceneEntities()
        {
            if (!string.IsNullOrEmpty(ActiveScene))
                return scenesDict[ActiveScene].GetAllEntities();
            return null;
        }
    }
}
