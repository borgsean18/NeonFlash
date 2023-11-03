using Camera;
using MainManagers;
using Movement;
using UnityEngine;

namespace Characters
{
    public class Player : MonoBehaviour
    {
        // Exposed Variables
        [Header("Settings")]
        [SerializeField] private CustomCamera playerCamera;
        
        // Private Variables
        private WorldManager worldManager;
        private PlayerManager playerManager;
        private PlayerMovement playerMovement;


        public void Init(PlayerManager _playerManager)
        {
            if (playerCamera == null)
                playerCamera = FindObjectOfType<CustomCamera>();
            
            playerCamera.SetCameraTarget(transform);

            playerManager = _playerManager;
            worldManager = playerManager.WorldManager;
            playerMovement = GetComponent<PlayerMovement>();

            worldManager.StartGame.AddListener(StartGame);
            worldManager.LoseGame.AddListener(Lose);
        }


        private void StartGame()
        {
            playerMovement.Run();
        }


        private void Lose()
        {
            if (worldManager.ImmortalDebugRun)
                return;
            
            playerMovement.Lose();
        }
    }
}
