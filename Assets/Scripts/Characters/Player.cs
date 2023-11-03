using Camera;
using MainManagers;
using Movement;
using UnityEngine;

namespace Characters
{
    public class Player : MonoBehaviour
    {
        // Private Variables
        private GameManagerScript gameManager;
        private WorldManager worldManager;
        private PlayerManager playerManager;
        private PlayerMovement playerMovement;


        public void Init(PlayerManager _playerManager)
        {
            playerManager = _playerManager;
            playerMovement = GetComponent<PlayerMovement>();
            
            playerManager.PlayerCamera.SetCameraTarget(transform);

            gameManager = playerManager.WorldManager;
            worldManager = playerManager.WorldManager;

            gameManager.StartGame.AddListener(StartGame);
            gameManager.LoseGame.AddListener(Lose);
        }


        private void StartGame()
        {
            playerMovement.Run();
        }


        private void Lose()
        {
            if (gameManager.ImmortalDebugRun)
                return;
            
            playerMovement.Lose();
        }
    }
}
