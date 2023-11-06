using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class MainMenuManager : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("EndlessRun", LoadSceneMode.Single);
        }
    }
}
