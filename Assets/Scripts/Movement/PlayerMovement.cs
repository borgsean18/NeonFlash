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
        public float buttonTime = 0.3f;
        [SerializeField] private LayerMask jumpAbleLayers;
        
        // Private Vars
        private float _currentSpeed;
        private float _timePassed;

        private bool _grounded;
        
        private float _jumpTime;
        private bool _jumping;
        
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
        
        
        public void Jump()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                -Vector2.up, 
                0.8f, 
                jumpAbleLayers);

            if (!hit)
            {
                Debug.DrawLine(transform.position, transform.position + (Vector3.down * 0.8f), Color.red);
                
                if (_grounded)
                {
                    _animator.SetBool("Jump", true);
                    _grounded = false;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + (Vector3.down * 0.8f), Color.green);

                if (!_grounded)
                {
                    _animator.SetBool("Jump", false);
                    _grounded = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && hit && _grounded) 
            {
                _jumping = true;
                _jumpTime = 0;
            }
            
            if(_jumping)
            {
                _rb.AddForce(new Vector2(0, jumpForce));
                _jumpTime += Time.deltaTime;
            }
            
            if(Input.GetKeyUp(KeyCode.Space) || _jumpTime > buttonTime)
            {
                _jumping = false;
            }
        }
    }
}
