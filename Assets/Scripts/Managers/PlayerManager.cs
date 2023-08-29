using System;
using Characters;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        // Exposed Variables
        [Header("General")]
        [SerializeField] private GameObject playerPrefab;
        

        // Private Variables
        private bool _init;
        private Player _player;


        private void Awake()
        {
            if (!_init)
                Init();
        }


        private void Init()
        {
            _init = true;
            
            SpawnPlayer();
        }


        private void SpawnPlayer()
        {
            _player = Instantiate(playerPrefab, new Vector3(0, 1, 0), quaternion.identity)
                .GetComponent<Player>();

            _player.Init();
        }
    }
}
