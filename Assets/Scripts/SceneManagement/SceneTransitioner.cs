namespace SceneManagement
{
    public class SceneTransitioner : SceneScript
    {
        public string nextSceneName;

        public void PlayGame()
        {
            // Fade to scene through 
            sceneFaderManager.TransitionToScene(nextSceneName);
        }
    }
}