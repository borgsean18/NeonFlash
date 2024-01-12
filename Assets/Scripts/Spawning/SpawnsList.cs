using System.Collections.Generic;
using System.Linq;
using Difficulty;
using Unity.Collections;
using UnityEngine;

namespace Spawning
{
    public class SpawnsList : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private List<GameObject> obstacles;


        // Properties
        public List<GameObject> Obstacles => obstacles;


        public GameObject GetRandomObstacle(int spawnAllowance, List<string> _coolDownObjects)
        {
            List<GameObject> affordableObstacles = new List<GameObject>();

            foreach(GameObject obst in obstacles)
            {
                DifficultyObject difficultyObject = obst.GetComponent<DifficultyObject>();

                if (difficultyObject == null || difficultyObject.DifficultyLevel > spawnAllowance)
                    continue;

                if (_coolDownObjects.Count() > 0)
                {
                    SpwanedObject spawnableObj = obst.GetComponent<SpwanedObject>();
                    if (_coolDownObjects.Contains(spawnableObj.SpawnableGUID))
                        continue;
                }

                affordableObstacles.Add(obst);
            }

            return affordableObstacles[Random.Range(0, affordableObstacles.Count)];
        }
    }
}
