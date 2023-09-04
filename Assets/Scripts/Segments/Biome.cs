using System;
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
		private WorldManager _worldManager;
		
		private float _spawnXPos;
		private int _segmentsToSpawn;
		private int _spawnedSegmentsCount;
		private List<Segment> _activeSegments;
		


		public void Init(WorldManager _worldManager, int _segmentsToSpawn)
		{
			this._worldManager = _worldManager;
			this._segmentsToSpawn = _segmentsToSpawn;

			_spawnXPos = 0;
			_spawnedSegmentsCount = 0;
			_activeSegments = new List<Segment>();

			PopulateBiome(initSegmentSpawnCount);
		}


		private void PopulateBiome(int _segmentCountToSpawn = 1)
		{
			for (int i = 0; i < _segmentCountToSpawn; i++)
			{
				Segment newSegment = Instantiate(
					segmentTypes[Random.Range(0, segmentTypes.Count)],
					new Vector3(_spawnXPos, 0),
					quaternion.identity,
					transform);

				newSegment.Init(this);

				_spawnXPos += newSegment.SegmentWidth();
				
				_activeSegments.Add(newSegment);
				_spawnedSegmentsCount++;
			}
		}


		public void DeleteSegments(Segment _segment)
		{
			int segmentIndex = _activeSegments.IndexOf(_segment);

			int indexOfSegmentToDestroy = segmentIndex - deleteSegmentBuffer;

			if (indexOfSegmentToDestroy < 0)
				return;

			Segment segmentToDestroy = _activeSegments[indexOfSegmentToDestroy];
			
			Destroy(segmentToDestroy.gameObject);

			PopulateBiome();
		}
	}
}
