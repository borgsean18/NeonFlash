using System;
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
        [SerializeField] private float minTimeTillNextMovement;
        [SerializeField] private float maxTimeTillNextMovement;
        [SerializeField] private List<Vector3> droneAvailablePositions;
        

        // Components
        private Animator _animator;
        private Vector3 _targetPos;
        
        
        // Private Variables
        private bool _travel;
        

        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            _animator = GetComponentInChildren<Animator>();
        }


        private void Update()
        {
            MoveToPosition();
        }


        private void MoveToPosition()
        {
            _targetPos = RandomPosition();
        }


        private Vector3 RandomPosition()
        {
            return droneAvailablePositions[Random.Range(0, droneAvailablePositions.Count)];
        }
    }
}
