using System.Collections;
using Characters;
using Unity.Mathematics;
using UnityEngine;

namespace MainManagers
{
    public class PlayerManager : MonoBehaviour
    {
        // Exposed Variables
        [Header("General")]
        [SerializeField] private GameObject playerPrefab;
        

        // Private Variables
        private bool _init;
        private Player _player;
        private WorldManager _worldManager;
        
        // Components
        public WorldManager WorldManager => _worldManager;


        private void Awake()
        {
            if (!_init)
                Init();
        }


        private void Init()
        {
            _init = true;

            _worldManager = FindObjectOfType<WorldManager>();
            
            StartCoroutine(SpawnPlayer());
        }


        /// <summary>
        /// Delay spawning the player to give chance for ground beneath them to spawn first
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnPlayer()
        {
            yield return new WaitForSeconds(0.5f);
            
            _player = Instantiate(playerPrefab, 
                    transform.position, 
                    quaternion.identity)
                .GetComponent<Player>();

            _player.Init(this);
        }
    }
}
