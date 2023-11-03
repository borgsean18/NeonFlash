using MainManagers;
using Movement;
using UnityEngine;

namespace Characters
{
    public class Player : MonoBehaviour
    {
        // Private Variables
        private GameManagerScript gameManager;
        private PlayerManager playerManager;
        private PlayerMovement playerMovement;
        
        
        // Properties
        public GameManagerScript GameManagerScript => gameManager;


        public void Init(PlayerManager _playerManager)
        {
            playerManager = _playerManager;
            
            playerManager.PlayerCamera.SetCameraTarget(transform);

            gameManager = playerManager.WorldManager;
            
            // Must be init after gameManager is set
            playerMovement = GetComponent<PlayerMovement>();
            playerMovement.Init(this);

            gameManager.StartGame.AddListener(StartRun);
            gameManager.LoseGame.AddListener(EndRun);
        }


        private void StartRun()
        {
            
        }


        private void EndRun()
        {
            
        }
    }
}
