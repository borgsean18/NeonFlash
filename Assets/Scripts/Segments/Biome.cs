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
		
		private float _spawnXPos;
		private int _segmentsToSpawn;
		private int _spawnedSegmentsCount;
		private List<Segment> _activeSegments;
		private Coroutine _deleteBiomeCoRoutine;


		public void Init(WorldManager _worldManager, int _segmentsToSpawn)
		{
			_isBiomeAlive = true;
			
			this._worldManager = _worldManager;
			this._segmentsToSpawn = _segmentsToSpawn;

			_spawnXPos = transform.position.x;
			_spawnedSegmentsCount = 0;
			_activeSegments = new List<Segment>();

			PopulateBiome(initSegmentSpawnCount);
		}


		private void PopulateBiome(int _segmentCountToSpawn = 1)
		{
			// Biome is still active so spawn more segments
			for (int i = 0; i < _segmentCountToSpawn; i++)
			{
				Segment newSegment = Instantiate(
					segmentTypes[Random.Range(0, segmentTypes.Count)],
					new Vector3(_spawnXPos, 0),
					quaternion.identity,
					transform);

				if (_spawnedSegmentsCount + 1 == _segmentsToSpawn)
					newSegment.Init(this, true);
				else
					newSegment.Init(this);

				_spawnXPos += newSegment.SegmentWidth();
				
				_activeSegments.Add(newSegment);
				_spawnedSegmentsCount++;
			}
			
			// If this biome reached its given limit, start next biome
			if (_spawnedSegmentsCount >= _segmentsToSpawn)
			{
				Vector3 newBiomePos = new Vector3(_spawnXPos, 0);
				
				_worldManager.SetUpNewBiome(newBiomePos);

				_isBiomeAlive = false;
			}
		}


		public void DeleteSegments(Segment _segment)
		{
			if (_segment.IsBiomeTrigger)
				Destroy(gameObject);
			
			Destroy(_segment.gameObject);

			if (_isBiomeAlive)
				PopulateBiome();
		}
	}
}
