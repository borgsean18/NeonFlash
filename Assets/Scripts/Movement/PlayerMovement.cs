using System;
using UnityEngine;

namespace Movement
{
    public class PlayerMovement : Movement, ISprint
    {
        // Exposed Vars
        [Header("Settings")]
        [SerializeField] private AnimationCurve speedMultiplier;
        [SerializeField] private float minutesTillMaxSpeed;
        
        // Private Vars
        // private bool _init;
        private float _currentSpeed;
        private float _timePassed;


        // private void Awake()
        // {
        //     if (!_init)
        //         Init();
        // }
        //
        //
        // private void Init()
        // {
        //     _currentSpeed = startingSpeed;
        // }


        private void Update()
        {
            CalculateCurrentSpeed();

            Sprint();
        }


        private void CalculateCurrentSpeed()
        {
            if (_currentSpeed >= maxSpeed) return;
            
            _timePassed += Time.deltaTime;

            _currentSpeed = maxSpeed * speedMultiplier.Evaluate(_timePassed / (minutesTillMaxSpeed * 60));
        }


        public void Sprint()
        {
            // Calculate the movement distance based on speed and time
            float movementDistance = _currentSpeed * Time.deltaTime;

            // Get the current position of the GameObject
            Vector3 currentPosition = transform.position;

            // Calculate the new position
            Vector3 newPosition = currentPosition + new Vector3(movementDistance, 0, 0);

            // Move the GameObject to the new position
            transform.position = newPosition;
        }
        
        
        public void Jump()
        {
            throw new System.NotImplementedException();
        }
    }
}
