using SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace MainManagers
{
    public class UIManager : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private GameObject startGameButton;
        [SerializeField] private Image gameLoaderBackGround;
        [SerializeField] private GameObject endRunPanel;


        // Private Variables
        private GameManagerScript gameManagerScript;


        public void Init(GameManagerScript _gameManagerScript)
        {
            gameManagerScript = _gameManagerScript;

            gameManagerScript.StartGame.AddListener(StartGame);

            gameManagerScript.LoseGame.AddListener(EndRunUI);
        }


        private void StartGame()
        {
            startGameButton.SetActive(false);
        }


        private void EndRunUI()
        {
            endRunPanel.SetActive(true);
        }
    }
}