using Characters;
using General;
using MainManagers;
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
        
        
        // Private Vars
        private Player player;
        private TouchDetector touchDetector;
        private GameManagerScript gameManagerScript;
        
        
        // Components
        private Rigidbody2D _rb;
        private Animator _animator;


        public void Init(Player _player)
        {
            canMove = false;

            player = _player;
            gameManagerScript = player.GameManagerScript;
            
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            
            gameManagerScript.StartGame.AddListener(Run);
            gameManagerScript.LoseGame.AddListener(Fall);

            touchDetector = FindObjectOfType<TouchDetector>();
            touchDetector.AddPlayTouchBehaviour(Jump);
        }


        private void Update()
        {

#if UNITY_EDITOR
            DebugChecks();
#endif
            
        }


        private void Run()
        {
            canMove = true;
            
            SprintState();
        }


        private void Fall()
        {
            if (gameManagerScript.ImmortalDebugRun)
                return;
            
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

            if (hit && canMove)
                SprintState();
            else
                JumpState();

            return hit;
        }
        
        
        public void Jump()
        {
            if (!IsGrounded()) return;
#if UNITY_WEBGL
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
#endif
            
#if UNITY_ANDROID
            
            // Do nothing if attack cool down is active, or if theres no touches
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        // Position of the touch
                        Vector2 pos = Input.GetTouch(i).position;

                        // If player touched the lower half of the screen
                        if (pos.y >= Screen.height / 2)
                        {
                            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        }
                    }
                }
            }
            
#endif
            
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
