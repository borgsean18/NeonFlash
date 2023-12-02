using System;
using UnityEngine;

namespace Enemies.Drone
{
    public class DroneBehaviour : MonoBehaviour
    {
        // Components
        private Animator animator;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            animator = GetComponentInChildren<Animator>();

            EnterScene();
        }


        private void EnterScene()
        {
            animator.SetTrigger("MoveToStart");
        }
    }
}
