using System.Collections;
using System.Collections.Generic;
using Characters;
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

        [Header("Shooting Settings")]
        [SerializeField] private float reloadTime;
        [SerializeField] Transform firingTransform;
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] private Transform targetTransform;


        // Components
        private Animator _animator;


        // Private Variables
        private Vector3 _currentPos;
        private Vector3 _targetMovementPosition;
        private DroneMovementStates _droneMovementStates;
        private DroneFiringStates _droneFiringStates;
        private Transform playerTransform;


        private enum DroneMovementStates
        {
            None,
            Idle,
            Firing,
            Moving
        }


        private enum DroneFiringStates
        {
            None,
            Reloading,
            Ready,
            Firing
        }


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            _animator = GetComponentInChildren<Animator>();
            _targetMovementPosition = RandomPosition();

            _droneMovementStates = DroneMovementStates.Moving;
            _droneFiringStates = DroneFiringStates.Reloading;
        }


        private void Update()
        {
            MoveToPosition();

            DroneFiringHandler();
        }


        #region Movement

        private void MoveToPosition()
        {
            // If the drone more or less arrived at its destination
            if (Vector2.Distance(transform.position, _targetMovementPosition) < 0.2f)
            {
                // If drone is in moving state
                if (_droneMovementStates == DroneMovementStates.Moving)
                {
                    // Set drone to idle
                    _droneMovementStates = DroneMovementStates.Idle;

                    // Pick a new target position after a timer expires
                    StartCoroutine(PickNewPosition());
                }
                
                // Do nothing more (Still if drone is at its target position)
                return;
            }
            
            // Move to target position
            transform.position = Vector2.MoveTowards(
                transform.position, 
                _targetMovementPosition, 
                Time.deltaTime * droneSpeed
            );
        }


        private IEnumerator PickNewPosition()
        {
            float timeToWait = Random.Range(minTimeTillNextMovement, maxTimeTillNextMovement);
            
            yield return new WaitForSeconds(timeToWait);

            _targetMovementPosition = RandomPosition();

            _droneMovementStates = DroneMovementStates.Moving;
        }


        private Vector3 RandomPosition()
        {
            return droneAvailablePositions[Random.Range(0, droneAvailablePositions.Count)];
        }

        #endregion
        
        
        #region Firing

        private void DroneFiringHandler()
        {
            switch (_droneFiringStates)
            {
                case DroneFiringStates.Reloading:
                    StartCoroutine(Reload());
                    break;
                
                case DroneFiringStates.Ready:
                    FireWhenReady();
                    break;
            }
        }


        /// <summary>
        /// Wait for a timer till drone can shoot another bullet
        /// </summary>
        private IEnumerator Reload()
        {
            _droneFiringStates = DroneFiringStates.None;
            
            yield return new WaitForSeconds(reloadTime);

            _droneFiringStates = DroneFiringStates.Ready;
        }


        /// <summary>
        /// Fire bullet when drone movement next idles
        /// </summary>
        private void FireWhenReady()
        {
            if (_droneMovementStates != DroneMovementStates.Idle)
                return;

            if (playerTransform == null)
                playerTransform = FindObjectOfType<Player>().transform;
            
            GameObject bullet = Instantiate(projectilePrefab, firingTransform.position, Quaternion.identity);

            _droneFiringStates = DroneFiringStates.Reloading;
        }
        
        #endregion
    }
}
