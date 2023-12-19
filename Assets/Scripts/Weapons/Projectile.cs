using System;
using System.Collections;
using UnityEngine;


namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        // Private Variables
        private Transform _target;
        private Vector3 _targetPos;
        private GameObject _firer;
        private float _speed;

        // Properties
        public Transform Target => _target;
        public GameObject Firer => _firer;
        public float Speed => _speed;


        public void Init(Transform target, GameObject firer)
        {
            _target = target;
            _targetPos = _target.position;
            _firer = firer;
        }
    }
}
