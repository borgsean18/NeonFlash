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
        
        
        // public GameObject GetRandomObstacle(List<string> _cooldownObjects)
        // {
        //     List<GameObject> newObstaclesList = obstacles;
        //     
        //     
        //     
        //     return newObstaclesList[Random.Range(0, newObstaclesList.Count)];
        // }
    }
}
