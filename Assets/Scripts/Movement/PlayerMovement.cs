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
        [SerializeField] private bool canMove;
        [SerializeField] private AnimationCurve speedMultiplier;
        [SerializeField] private float minutesTillMaxSpeed;
        
        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] private LayerMask jumpAbleLayers;
        
        // Private Vars
        private float _currentSpeed;
        private float _timePassed;
        
        // Components
        private Rigidbody2D _rb;
        private Animator _animator;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            canMove = true;
            
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            CalculateCurrentSpeed();

            Sprint();
            Jump();

#if UNITY_EDITOR
            DebugChecks();
#endif
        }


        private void CalculateCurrentSpeed()
        {
            if (_currentSpeed >= maxSpeed) return;
            
            _timePassed += Time.deltaTime;

            _currentSpeed = maxSpeed * speedMultiplier.Evaluate(_timePassed / (minutesTillMaxSpeed * 60));
        }


        public void Sprint()
        {
            if (!canMove) return;
            
            // Calculate the movement distance based on speed and time
            float movementDistance = _currentSpeed * Time.deltaTime;

            // Get the current position of the GameObject
            Vector3 currentPosition = transform.position;

            // Calculate the new position
            Vector3 newPosition = currentPosition + new Vector3(movementDistance, 0, 0);

            // Move the GameObject to the new position
            transform.position = newPosition;
        }


        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                -Vector2.up, 
                0.8f, 
                jumpAbleLayers);

            if (hit)
                SprintState();
            else
                JumpState();

            return hit;
        }
        
        
        public void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }


        private void DebugChecks()
        {
            if (!IsGrounded())
            {
                Debug.DrawLine(transform.position, transform.position + (Vector3.down * 0.8f), Color.red);
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + (Vector3.down * 0.8f), Color.green);
            }
        }


        #region Animation States

        private void SprintState()
        {
            _animator.SetBool("Jump", false);
        }

        private void JumpState()
        {
            _animator.SetBool("Jump", true);
        }

        #endregion
    }
}
