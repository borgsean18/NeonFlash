using UnityEngine;

namespace SceneManagement
{
    public class SceneScript : MonoBehaviour
    {
        // Private Variables
        protected SceneFaderManager sceneFaderManager;


        private void Awake()
        {
            Init();
        }


        protected virtual void Init()
        {
            sceneFaderManager = FindObjectOfType<SceneFaderManager>();
            
            sceneFaderManager.CompleteSceneTransition();
        }
    }
}
