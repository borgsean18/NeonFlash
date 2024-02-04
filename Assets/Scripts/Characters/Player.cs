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


        // Components
        private Animator _animator;
        
        
        // Properties
        public GameManagerScript GameManagerScript => gameManager;
        public Animator Animator => _animator;


        public void Init(PlayerManager _playerManager)
        {
            playerManager = _playerManager;
            
            playerManager.PlayerCamera.SetCameraTarget(transform);

            gameManager = playerManager.WorldManager;

            _animator = GetComponent<Animator>();
            
            // Must be init after gameManager is set
            playerMovement = GetComponent<PlayerMovement>();
            playerMovement.Init(this);

            gameManager.StartGame.AddListener(StartRun);
            gameManager.LoseGame.AddListener(EndRun);
        }


        private void StartRun()
        {
            
        }


        public void EndRun()
        {
            playerMovement.StopMovement();
        }
    }
}
