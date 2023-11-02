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

            worldManager.LoseGame.AddListener(Lose);
        }


        private void Lose()
        {
            playerMovement.CanMove = false;
        }
    }
}
