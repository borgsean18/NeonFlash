using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Drone
{
    public class DroneBehaviour : MonoBehaviour
    {
        // Exposed Variables
        [Header("Movement Settings")]
        [SerializeField] private float droneSpeed;
        [SerializeField] private float minTimeTillNextMovement;
        [SerializeField] private float maxTimeTillNextMovement;
        [SerializeField] private List<Vector3> droneAvailablePositions;


        // Components
        private Animator _animator;


        // Private Variables
        private Vector3 _currentPos;
        private Vector3 _targetPos;
        private bool _travel;
        private DroneStates _droneStates;


        private enum DroneStates
        {
            None,
            Idle,
            Firing,
            Moving
        }


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            _animator = GetComponentInChildren<Animator>();
            _targetPos = RandomPosition();

            _droneStates = DroneStates.Moving;
        }


        private void Update()
        {
            MoveToPosition();
        }

        
        private void MoveToPosition()
        {
            // If the drone more or less arrived at its destination
            if (Vector2.Distance(transform.position, _targetPos) < 0.2f)
            {
                // If drone is in moving state
                if (_droneStates == DroneStates.Moving)
                {
                    // Set drone to idle
                    _droneStates = DroneStates.Idle;

                    // Pick a new target position after a timer expires
                    StartCoroutine(PickNewPosition());
                }
                
                // Do nothing more (Still if drone is at its target position)
                return;
            }
            
            // Move to target position
            transform.position = Vector2.MoveTowards(
                transform.position, 
                _targetPos, 
                Time.deltaTime * droneSpeed
                );
        }


        private IEnumerator PickNewPosition()
        {
            float timeToWait = Random.Range(minTimeTillNextMovement, maxTimeTillNextMovement);
            
            yield return new WaitForSeconds(timeToWait);

            _targetPos = RandomPosition();

            _droneStates = DroneStates.Moving;
        }


        private Vector3 RandomPosition()
        {
            return droneAvailablePositions[Random.Range(0, droneAvailablePositions.Count)];
        }
    }
}
