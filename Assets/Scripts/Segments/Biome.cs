using System.Collections.Generic;
using Managers;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Segments
{
    public class Biome : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private List<GameObject> biomeSegments;
        
        // Private Variables
        private WorldManager _worldManagerRef;
        private List<Segment> _activeBiomeSegments;
        

        public void Init(WorldManager _worldManager)
        {
            _worldManagerRef = _worldManager;
        }

        public void SpawnSegments(int _segmentsCount)
        {
            // Starting at origin
            float segmentXPos = 0;
            
            for (int i = 0; i < _segmentsCount; i++)
            {
                GameObject newSegmentObject = Instantiate(
                    biomeSegments[Random.Range(0, biomeSegments.Count)],
                    new Vector3(segmentXPos,0),
                    quaternion.identity);

                Segment newSegment = newSegmentObject.GetComponent<Segment>();
                
                newSegment.Init(this);

                segmentXPos += newSegment.SegmentWidth();
            }
        }
    }
}
