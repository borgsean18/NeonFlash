using System;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour, IJump
    {
        // Exposed Vars
        [Header("Movement")]
        [SerializeField] private bool canMove;
        
        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] private LayerMask jumpAbleLayers;
        
        // Components
        private Rigidbody2D _rb;
        private Animator _animator;
        static readonly int Idle = Animator.StringToHash("Idle");


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            canMove = false;
            
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (!canMove) return;
            
            Jump();

#if UNITY_EDITOR
            DebugChecks();
#endif
        }


        public void Run()
        {
            canMove = true;
            
            SprintState();
        }


        public void Lose()
        {
            canMove = false;
            
            // set to hurt animation
            IdleState();
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


        #region IJump Interface

        public bool IsGrounded()
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

        #endregion

        
        #region Animation States

        private void SprintState()
        {
            _animator.SetBool("Run", true);
            _animator.SetBool("Jump", false);
            _animator.SetBool("Idle", false);
        }

        private void JumpState()
        {
            _animator.SetBool("Jump", true);
            _animator.SetBool("Run", false);
            _animator.SetBool("Idle", false);
        }

        private void IdleState()
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Jump", false);
            _animator.SetBool("Run", false);
        }

        #endregion
    }
}
