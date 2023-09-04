using UnityEngine;

namespace Managers
{
    public class WorldManager : MonoBehaviour
    {
        // Exposed Vars
        [Header("Biome Settings")]
        [SerializeField, Min(10)] private int minSegmentsPerBiome;
        [SerializeField, Min(15)] private int maxSegmentsPerBiome;
        
        // Private Vars

        
        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            
        }
    }
}
