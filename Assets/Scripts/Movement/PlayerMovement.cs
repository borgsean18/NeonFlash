using Characters;
using General;
using MainManagers;
using UnityEngine;
using System.Collections;

using Debugging;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour, IJump
    {
        // Exposed Vars
        [Header("Movement")]
        [SerializeField] private bool canMove;
        
        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField, Min(1)] private int maxJumps;
        [SerializeField] private LayerMask jumpAbleLayers;
        
        
        // Private Vars
        private Player player;
        private TouchDetector touchDetector;
        private GameManagerScript gameManagerScript;
        private int _availableJumps;
        
        
        // Components
        private Rigidbody2D _rb;
        private Animator _animator;


        public void Init(Player _player)
        {
            canMove = false;

            player = _player;
            gameManagerScript = player.GameManagerScript;
            
            _rb = GetComponent<Rigidbody2D>();
            _animator = _player.Animator;
            
            gameManagerScript.StartGame.AddListener(Run);
            gameManagerScript.LoseGame.AddListener(StopMovement);

#if UNITY_ANDROID || UNITY_IOS
            touchDetector = FindObjectOfType<TouchDetector>();
            touchDetector.AddPlayTouchBehaviour(MobileDetectJump);
#endif
        }


        private void FixedUpdate()
        {

#if UNITY_EDITOR

            DebugChecks();

#endif


#if UNITY_WEBGL

            WebDetectJump();

#endif

        }


        private void Run()
        {
            canMove = true;
            
            SprintState();
        }


        public void StopMovement()
        {
            canMove = false;

            DownedState();
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
            if (!canMove)
                return false;
            
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                -Vector2.up, 
                0.8f, 
                jumpAbleLayers);

            if (hit)
            {
                SprintState();

                if (_availableJumps < maxJumps)
                    _availableJumps = maxJumps;
            }
            else
                JumpState();

            return hit;
        }
        
        
        /// <summary>
        /// This method is being called in the Update method of TouchDetector.cs
        /// </summary>
        private void MobileDetectJump()
        {
            for (int i = 0; i < Input.touchCount; ++i)
            { 
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    // Position of the touch
                    Vector2 pos = Input.GetTouch(0).position;

                    // If player touched the lower half of the screen
                    if (pos.y >= Screen.height / 2 && pos.x <= Screen.width / 2)
                    {
                        DebugText.Singleton.DisplayMessage("jump");
                        Jump();
                    }
                    else
                    {
                        string debugString = "P:" + pos.y + "\n\rSH:" + Screen.height / 2;
                        DebugText.Singleton.DisplayMessage(debugString);
                    }
                }
            }     
        }


        private void WebDetectJump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }


        public void Jump()
        {
            if (_availableJumps <= 1) return;

            _availableJumps--;

            _rb.velocity = Vector3.zero;

            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        #endregion

        
        #region Movement Animation States

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

        private void DownedState()
        {
            _animator.SetBool("Downed", true);
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", false);
            _animator.SetBool("Jump", false);
        }

        #endregion
    }
}
