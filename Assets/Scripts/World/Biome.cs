using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
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
		
		[Header("Decorations")]
		[SerializeField] private List<BiomeDecorationEntry> backgroundLayers;

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
		private float _lastSpawnedDecorationXPos;
		private float _localXPos;
		
		// Properties 
		public List<Segment> ActiveSegments => _activeSegments;


		public void Init(WorldManager _worldManager, int _segmentsToSpawn)
		{
			_isBiomeAlive = true;
			
			this._worldManager = _worldManager;
			this._totalToSpawn = _segmentsToSpawn;

			_spawnedSegmentsCount = 0;
			_activeSegments = new List<Segment>();

			SpawnSegments(initSegmentSpawnCount);
		}


		private void Update()
		{
			MoveBiome();
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

				SpawnDecoration(newSegment);

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


		private void SpawnDecoration(Segment _segment)
		{
			// Do nothing if this biome has no decorations
			if (backgroundLayers.Count == 0) return;

			if (_lastSpawnedDecorationXPos == 0)
				_lastSpawnedDecorationXPos = _segment.transform.localPosition.x;

			// Dont spawn another decoration if it overlaps an existing one
			if (_segment.transform.localPosition.x < _lastSpawnedDecorationXPos) return;

			int lastDecorationListPos = 0;
			
			foreach (var entry in backgroundLayers)
			{
				// Create Decoration
				Decoration decoration = Instantiate(
					entry.decorationPrefab,
					new Vector2(_segment.transform.position.x, entry.decorationTransform.position.y),
					quaternion.identity,
					entry.decorationTransform);
				
				// Tell Decoration to pick a sprite, but dont look like the previously spawned decoration
				lastDecorationListPos = decoration.SetSprite(lastDecorationListPos);

				// Add the sprite width to the variable storing the X Pos the last decoration was spawned at
				_lastSpawnedDecorationXPos += decoration.DecorationWidth();

				// Link the decoration to the segment
				_segment.LinkObject(decoration.gameObject);
			}
		}


		public void DeleteSegments(Segment _segment)
		{
			// If last segment, destroy the biome
			if (_segment == _activeSegments[^1])
				Destroy(gameObject);
			
			// Destroy the segment
			_activeSegments.Remove(_segment);
			_segment.DeleteSegment();

			// If biome is still alive, make a new segment
			if (_isBiomeAlive)
				SpawnSegments();
		}


		private void MoveBiome()
		{
			// Calculate the movement distance based on speed and time
			float movementDistance = _worldManager.CalculateCurrentSpeed() * Time.deltaTime;

			// Get the current position of the GameObject
			Vector3 currentPosition = transform.position;

			// Calculate the new position
			Vector3 newPosition = currentPosition - new Vector3(movementDistance, 0, 0);

			// Move the GameObject to the new position
			transform.position = newPosition;

		}
	}
}
