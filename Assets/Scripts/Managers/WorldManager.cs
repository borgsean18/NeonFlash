using System.Collections.Generic;
using Segments;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class WorldManager : MonoBehaviour
    {
        // Exposed Vars
        [Header("Biome Settings")]
        [SerializeField, Min(150)] private int minSegmentsPerBiome;
        [SerializeField, Min(250)] private int maxSegmentsPerBiome;
        [SerializeField] private List<GameObject> biomePrefabs;
        
        // Private Vars
        private GameObject _activeBiomeObject;
        private Biome _activeBiome;
        private GameObject _previousBiomeObject;

        private int _segmentsToSpawn;

        
        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            SetUpNewBiome();
        }


        /// <summary>
        /// Randomly Select a biome and Initialize it
        /// </summary>
        private void SetUpNewBiome()
        {
            // Keep reference to old biome if exists
            if (_activeBiomeObject != null)
                _previousBiomeObject = _activeBiomeObject;
            
            // Determine how long this biome will be
            _segmentsToSpawn = Random.Range(minSegmentsPerBiome, maxSegmentsPerBiome + 1);
            
            // Randomly pick a biome
            _activeBiomeObject = Instantiate(
                biomePrefabs[Random.Range(0, biomePrefabs.Count)],
                new Vector3(0, 0),
                quaternion.identity);

            // Reference Biome script
            _activeBiome = _activeBiomeObject.GetComponent<Biome>();

            // Initialize Biome
            _activeBiome.Init(this, _segmentsToSpawn);
        }
    }
}
