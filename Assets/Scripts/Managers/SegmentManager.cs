using System;
using System.Collections.Generic;
using Segments;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SegmentManager : MonoBehaviour
    {
        // Exposed Vars
        [Header("Base Settings")]
        [SerializeField, Min(0)] private int bufferOfDeletableUsedSegments;
        
        [Header("Segments")]
        [SerializeField] private List<Segment> activeSegments;
        
        [Header("Segment Biomes")]
        [SerializeField] private List<GameObject> citySegmentPrefabs;

        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            // Spawn a set number of segments
            SpawnSegments(20);
        }


        private void SpawnSegments(int _amountToSpawn)
        {
            for (int i = 0; i < _amountToSpawn; i++)
            {
                int randomIndex = Random.Range(0, citySegmentPrefabs.Count);
                
                float newSegmentXPos = DetermineSpawnPosition();

                GameObject newSegmentObject = Instantiate(citySegmentPrefabs[randomIndex],
                    new Vector3(newSegmentXPos, 0),
                    quaternion.identity);

                Segment newSegment = newSegmentObject.GetComponent<Segment>();
                
                newSegment.Init(this);
                
                activeSegments.Add(newSegment);
            }
        }


        private float DetermineSpawnPosition()
        {
            if (activeSegments.Count > 0)
            {
                // Get the last segment in the list
                Segment lastActiveSegment = activeSegments[^1];
                
                SpriteRenderer lastSegmentSprite = lastActiveSegment.GetComponent<SpriteRenderer>();

                float newSpawnPos = lastActiveSegment.transform.position.x;
                
                newSpawnPos += lastSegmentSprite.size.x;
                
                return newSpawnPos;
            }
            
            return 0;
        }
        
        
        /// <summary>
        /// Delete all the segments that have been exited by the player with an optional buffer
        /// of segments to delete from the last one the player would have just exited
        /// </summary>
        /// <param name="_segment">The segment that was last exited by the player</param>
        public void DeleteUsedSegments(Segment _segment)
        {
            // Get the index of the segment that was just exited
            int index = activeSegments.IndexOf(_segment);

            // Apply the buffer to the index of the segment just exited and see
            // if there is a segment at that index
            if (index - bufferOfDeletableUsedSegments >= 0)
            {
                Segment segmentToRemove = activeSegments[index - bufferOfDeletableUsedSegments];

                activeSegments.Remove(segmentToRemove);

                // Delete that segment
                Destroy(segmentToRemove.gameObject);

                // Spawn a new segment ahead of the player
                SpawnSegments(1);
            }
        }
    }
}
