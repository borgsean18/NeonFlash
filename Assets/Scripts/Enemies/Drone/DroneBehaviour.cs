using System.Collections;
using System.Collections.Generic;
using Characters;
using Movement;
using UnityEngine;
using Weapons;
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
        [SerializeField] private GameObject projectileOwner;
        [SerializeField] private float reloadTime;
        [SerializeField] private Transform firingTransform;
        [SerializeField] private GameObject projectilePrefab;


        // Components
        private Animator _animator;
        private HorizMovement _horizMovement;


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
            Moving,
            ChangePosition
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
            _horizMovement = GetComponent<HorizMovement>();

            _droneMovementStates = DroneMovementStates.ChangePosition;
            _droneFiringStates = DroneFiringStates.Reloading;
        }


        private void Update()
        {
            HandleMovement();

            DroneFiringHandler();
        }


        public void Die()
        {
            StopMovement();
            
            //_horizMovement.MoveWithWorld();
        }


        #region Movement

        private void HandleMovement()
        {
            switch (_droneMovementStates)
            {
                case DroneMovementStates.Moving:
                    MoveToPosition();
                    break;
                
                case DroneMovementStates.ChangePosition:
                    StartCoroutine(PickNewPosition());
                    _droneMovementStates = DroneMovementStates.Idle;
                    break;
                
                case DroneMovementStates.Idle:
                    break;
            }
        }

        private void MoveToPosition()
        {
            // If the drone more or less arrived at its destination
            if (Vector2.Distance(transform.position, _targetMovementPosition) < 0.2f)
            {
                _droneMovementStates = DroneMovementStates.ChangePosition;
                
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


        private void StopMovement()
        {
            _droneMovementStates = DroneMovementStates.Idle;

            droneSpeed = 0;
            
            _horizMovement.MoveWithWorld();
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

            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.Init(playerTransform, projectileOwner);

            _droneFiringStates = DroneFiringStates.Reloading;
        }
        
        #endregion
    }
}
