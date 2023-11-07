using System;
using UnityEngine;

namespace SceneManagement
{
    public class MainMenuManager : SceneScript
    {
        // Private Variables
        private SceneFaderManager sceneFaderManager;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            sceneFaderManager = FindObjectOfType<SceneFaderManager>();
        }


        public void PlayGame()
        {
            // Fade to scene through 
            sceneFaderManager.TransitionToScene("EndlessRun");
        }
    }
}
