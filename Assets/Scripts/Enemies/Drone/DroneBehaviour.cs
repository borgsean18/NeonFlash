using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Enemies.Drone
{
    public class DroneBehaviour : MonoBehaviour
    {
        // Exposed Variables
        [Header("Settings")]
        [SerializeField] float minTimeTillNextMovement;
        [SerializeField] float maxTimeTillNextMovement;
        

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
