using System.Collections.Generic;
using UnityEngine;

namespace Spawning
{
    public class SpawnsList : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private List<GameObject> obstacles;
        
        
        // Properties
        public List<GameObject> Obstacles => obstacles;


        public GameObject GetRandomObstacle()
        {
            return obstacles[Random.Range(0, obstacles.Count)];
        }
    }
}
