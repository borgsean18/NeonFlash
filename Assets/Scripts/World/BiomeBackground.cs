using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace World
{
    public class BiomeBackground : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private GameObject bgObjectPrefab;
        [SerializeField] private Vector2 spawnPos;
        
        // Private Variables
        private Biome attachedBiome;
        private List<Decoration> activeBackgroundObjects;
        
        private float currentSpawnXPos;
        private float biomeEndXPos;


        public void Init(Biome _biome)
        {
            attachedBiome = _biome;

            activeBackgroundObjects = new List<Decoration>();
            
            currentSpawnXPos = transform.position.x;
        }


        public void SpawnBackgroundObjects()
        {
            // Remove deleted references from the list
            activeBackgroundObjects = activeBackgroundObjects.Where(_x => _x != null).ToList();

            // X Pos of last segment in the biome
            biomeEndXPos = attachedBiome.ActiveSegments[^1].XPos;

            // Create a Vector 3 for the spawn point of the next BG Image
            Vector3 spawnPoint = spawnPos;

            // If there are bg images attached to this biome, spawn the next bg image at the end of the last existing one
            if (activeBackgroundObjects.Count > 0)
                spawnPoint.x = activeBackgroundObjects[^1].transform.position.x +
                               activeBackgroundObjects[^1].DecorationWidth();
            else
                spawnPoint.x = transform.position.x; // Spawn the building at the beginning of the biome
            
            // While the last spawned building has not caught up to the position of the last spawned segment,
            // spawn a building
            while (spawnPoint.x < biomeEndXPos)
            {
                Decoration newDecoration = Instantiate(bgObjectPrefab, spawnPoint, quaternion.identity, transform)
                    .GetComponent<Decoration>();

                activeBackgroundObjects.Add(newDecoration);
                
                spawnPoint.x += newDecoration.DecorationWidth();
            }
        }
    }
}
