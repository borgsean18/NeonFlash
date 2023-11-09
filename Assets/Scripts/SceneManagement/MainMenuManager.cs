namespace SceneManagement
{
    public class MainMenuManager : SceneScript
    {
        public void PlayGame()
        {
            // Fade to scene through 
            sceneFaderManager.TransitionToScene("EndlessRun");
        }
    }
}
