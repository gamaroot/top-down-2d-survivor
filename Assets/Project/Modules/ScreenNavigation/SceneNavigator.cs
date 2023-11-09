using System;
using UnityEngine.SceneManagement;

namespace ScreenNavigation
{
    public class SceneNavigator : SceneLifecycleOperator
    {
        public static SceneNavigator Instance { private set; get; }

        private SceneNavigator()
        {
            SceneManager.sceneLoaded += base.OnSceneLoaded;
            SceneManager.sceneUnloaded += base.OnSceneUnloaded;
        }

        public static void Initialize()
        {
            Instance = new SceneNavigator();
        }

        public void LoadAdditiveSceneAsync(SceneID sceneID, bool clearOldSceneParams = false)
        {
            base.StartShowScreenProcess(new SceneLoadData
            {
                ID = sceneID,
                Mode = LoadSceneMode.Additive
            }, clearOldSceneParams);
        }

        public void LoadAdditiveSceneAsync(SceneID sceneID, Action onComplete, bool clearOldSceneParams = false)
        {
            base.ExecuteOnShowAnimationComplete(sceneID, onComplete);

            base.StartShowScreenProcess(new SceneLoadData
            {
                ID = sceneID,
                Mode = LoadSceneMode.Additive
            }, clearOldSceneParams);
        }

        public void UnloadSceneAsync(SceneID sceneID)
        {
            base.StartHideScreenProcess(sceneID);
        }

        public void UnloadSceneAsync(SceneID sceneID, Action onComplete)
        {
            base.ExecuteOnSceneUnload(sceneID, onComplete);
            base.StartHideScreenProcess(sceneID);
        }
    }
}
