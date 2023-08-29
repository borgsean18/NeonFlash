using System;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Movement
{
    public class PlayerMovement : Movement, ISprint
    {
        // Exposed Vars
        [Header("Sprinting")]
        [SerializeField] private AnimationCurve speedMultiplier;
        [SerializeField] private float minutesTillMaxSpeed;
        
        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] private LayerMask jumpAbleLayers;
        
        // Private Vars
        private float _currentSpeed;
        private float _timePassed;
        private bool _canJump;
        
        // Components
        private Rigidbody2D _rb;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            _rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            CalculateCurrentSpeed();

            Sprint();
            Jump();
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
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                -Vector2.up, 
                0.6f, 
                jumpAbleLayers);


            // Player is not on a jump able surface
            if (!hit)
            {
                if (_canJump)
                    _canJump = false;
                
                return;
            }
            
            if (!_canJump)
                _canJump = true;

            if (Input.GetKeyDown(KeyCode.Space) && _canJump)
            {
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
