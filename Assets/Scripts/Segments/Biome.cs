using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Obstacles;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Segments
{
	public class Biome : MonoBehaviour
	{
		// Exposed Variables
		[Header("Settings")]
		[SerializeField] private int initSegmentSpawnCount;
		[SerializeField] private int deleteSegmentBuffer;
		
		[Header("Segments")]
		[SerializeField] private List<Segment> segmentTypes;

		// Obstacle Types
		[Header("Obstacle Settings")]
		[SerializeField] private List<Obstacle> obstacleTypes;

		// Enemy Types
		// TODO
		
		// Private Variables
		private bool _isBiomeAlive;
		
		private WorldManager _worldManager;
		
		private int _totalToSpawn;
		private int _spawnedSegmentsCount;
		private List<Segment> _activeSegments;
		private Coroutine _deleteBiomeCoRoutine;
		
		// Properties 
		public List<Segment> ActiveSegments => _activeSegments;


		public void Init(WorldManager _worldManager, int _segmentsToSpawn)
		{
			_isBiomeAlive = true;
			
			this._worldManager = _worldManager;
			this._totalToSpawn = _segmentsToSpawn;

			_spawnedSegmentsCount = 0;
			_activeSegments = new List<Segment>();

			PopulateBiome(initSegmentSpawnCount);
		}


		/// <summary>
		/// Add segments to this biome and Initialize them.
		/// </summary>
		/// <param name="_count">Number of segments to add to this biome. Default is 1
		/// but can be overriden to add more segments</param>
		private void PopulateBiome(int _count = 1)
		{
			// Biome is still active so spawn more segments
			for (int i = 0; i < _count; i++)
			{
				// Create Segment
				Segment newSegment = Instantiate(
					segmentTypes[Random.Range(0, segmentTypes.Count)],
					new Vector3(0, 0),
					quaternion.identity,
					transform);

				// Initialize the segment
				newSegment.Init(this);

				// Add the segment to list of active segments
				_activeSegments.Add(newSegment);
				
				// Increment count of segments spawned so far in this biome
				_spawnedSegmentsCount++;
			}
			
			// If this biome reached its given limit, start next biome
			if (_spawnedSegmentsCount >= _totalToSpawn)
			{
				// last created segment in this biome
				Segment lastSegment = _activeSegments[^1];

				// get XPos of last segment
				float newBiomeX = lastSegment.transform.position.x;
				
				// Add half of the width of the last segment, to that XPos
				newBiomeX += lastSegment.SegmentWidth() / 2;
				
				// Create the new biome at that X Coordinate
				Vector3 newBiomePos = new Vector3(newBiomeX, 0);
				_worldManager.SetUpNewBiome(newBiomePos);

				// Set this biome as dead to stop spawning segments
				_isBiomeAlive = false;
			}
		}


		public void DeleteSegments(Segment _segment)
		{
			// If last segment, destroy the biome
			if (_segment == _activeSegments[^1])
				Destroy(gameObject);
			
			// Destroy the segment
			_activeSegments.Remove(_segment);
			Destroy(_segment.gameObject);

			// If biome is still alive, make a new segment
			if (_isBiomeAlive)
				PopulateBiome();
		}
	}
}
