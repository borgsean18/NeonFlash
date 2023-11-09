using System;
using System.Collections;
using System.Collections.Generic;
using MainManagers;
using Movement;
using Obstacles;
using World;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World
{
	public class Biome : MonoBehaviour
	{
		// Exposed Variables
		[Header("Settings")]
		[SerializeField] private int initSegmentSpawnCount;
		
		[Header("Segments")]
		[SerializeField] private List<Segment> segmentTypes;

		[Header("Decoration Settings")]
		[SerializeField] private List<GameObject> biomeBackgroundPrefabs;

		// Enemy Types
		// TODO
		
		// Private Variables
		private bool _isBiomeAlive;
		private WorldManager _worldManager;
		
		private int _totalToSpawn;
		private int _spawnedSegmentsCount;
		private List<Segment> _activeSegments;
		private Coroutine _deleteBiomeCoRoutine;
		private float _lastSpawnedDecorationXPos;

		private List<BiomeBackground> _activeBgReferences;
		
		// Properties 
		public WorldManager WorldManager => _worldManager;
		public List<Segment> ActiveSegments => _activeSegments;


		public void Init(WorldManager _worldManager, int _segmentsToSpawn)
		{
			_isBiomeAlive = true;

			this._worldManager = _worldManager;
			this._totalToSpawn = _segmentsToSpawn;
			
			InitializeBiomeBackground();

			_spawnedSegmentsCount = 0;
			_activeSegments = new List<Segment>();
			SpawnSegments(initSegmentSpawnCount);
		}


		private void InitializeBiomeBackground()
		{
			if (biomeBackgroundPrefabs.Count == 0) return;
			
			_activeBgReferences = new List<BiomeBackground>();
			
			foreach (var t in biomeBackgroundPrefabs)
			{
				BiomeBackground biomeBackground = Instantiate(t, transform.position, quaternion.identity)
					.GetComponent<BiomeBackground>();
			
				biomeBackground.Init(this);
				
				_activeBgReferences.Add(biomeBackground);
			}
		}


		private void PopulateBiomeBackgrounds()
		{
			if (_activeBgReferences == null || _activeBgReferences.Count == 0) return; 
			
			foreach (var t in _activeBgReferences)
			{
				if (t != null)
					t.GetComponent<BiomeBackground>().SpawnBackgroundObjects();
			}
		}


		/// <summary>
		/// Add segments to this biome and Initialize them.
		/// </summary>
		/// <param name="_count">Number of segments to add to this biome. Default is 1
		/// but can be overriden to add more segments</param>
		private void SpawnSegments(int _count = 1)
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

			PopulateBiomeBackgrounds();
			
			// If this biome reached its given limit, start next biome
			if (_spawnedSegmentsCount >= _totalToSpawn)
				StartNextBiome();
		}


		private void StartNextBiome()
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


		public void UnLoadSegment(Segment _segment)
		{
			// If last segment, destroy the biome
			if (_segment == _activeSegments[^1])
				Destroy(gameObject);
			
			// Destroy the segment
			_activeSegments.Remove(_segment);

			// If biome is still alive, make a new segment
			if (_isBiomeAlive)
				SpawnSegments();
		}
	}
}
