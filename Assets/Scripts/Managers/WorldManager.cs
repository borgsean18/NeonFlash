using System;
using System.Collections.Generic;
using Segments;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class WorldManager : MonoBehaviour
    {
        // Exposed Vars
        [Header("Biomes")]
        [SerializeField] private List<Biome> biomes;
        
        [Header("Biome Settings")]
        [SerializeField, Min(10)] private int minSegmentsPerBiome;
        [SerializeField, Min(15)] private int maxSegmentsPerBiome;
        
        // Private Vars
        private Biome _activeBiome;
        private int _biomeSegmentsRequired;

        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            foreach (var biome in biomes)
            {
                biome.Init(this);
            }
            
            SelectBiome();
        }


        /// <summary>
        /// Randomly pick a biome and how many segments of that biome will be used
        /// </summary>
        private void SelectBiome()
        {
            _activeBiome = biomes[Random.Range(0, biomes.Count)];
            _biomeSegmentsRequired = Random.Range(minSegmentsPerBiome, maxSegmentsPerBiome + 1);

            StartBiomeProduction();
        }


        private void StartBiomeProduction()
        {
            _activeBiome.SpawnSegments(_biomeSegmentsRequired);
        }
    }
}
