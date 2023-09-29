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
            Jump();

#if UNITY_EDITOR
            DebugChecks();
#endif
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
            _animator.SetBool("Jump", false);
        }

        private void JumpState()
        {
            _animator.SetBool("Jump", true);
        }

        #endregion
    }
}
