using System;
using MainManagers;
using UnityEngine;
using UnityEngine.Events;

namespace General
{
    public class TouchDetector : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private GameManagerScript gameManagerScript;
        
        // Private Variables
        UnityEvent pauseTouch;
        UnityEvent playTouch;
        
        
        void Awake()
        {
            Init();
        }

        
        void Init()
        {
            pauseTouch = new UnityEvent();
            playTouch = new UnityEvent();
        }
        

        void Update()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        switch (gameManagerScript.GameState)
                        {
                            case GameStates.Pause:
                                pauseTouch.Invoke();
                                break;
                            
                            case GameStates.Play:
                                playTouch.Invoke();
                                break;
                        }
                    }
                }
            }
        }


        public void AddPlayTouchBehaviour(UnityAction _method)
        {
            playTouch.AddListener(_method);
        }
    }
}
