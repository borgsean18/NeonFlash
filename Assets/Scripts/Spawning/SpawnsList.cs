using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Spawning
{
    public class SpawnsList : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private List<GameObject> obstacles;
        
        
        // Private Variables
        private List<GameObject> filteredList;


        // Properties
        public List<GameObject> Obstacles => obstacles;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            filteredList = new List<GameObject>();
        }


        public GameObject GetRandomObstacle(List<string> _coolDownObjects)
        {
            if (_coolDownObjects.Count <= 0)
            {
                if (filteredList != null)
                    filteredList.Clear();

                return obstacles[Random.Range(0, obstacles.Count)];
            }

            filteredList = new List<GameObject>();

            // Filter out obstacles that have an active cooldown going
            foreach (GameObject obj in obstacles)
            {
                SpwanedObject spawnableObj = obj.GetComponent<SpwanedObject>();

                if (spawnableObj.SpawnableGUID == null || !_coolDownObjects.Contains(spawnableObj.SpawnableGUID))
                    filteredList.Add(obj);
            }

            return filteredList[Random.Range(0, filteredList.Count)];
        }
    }
}
