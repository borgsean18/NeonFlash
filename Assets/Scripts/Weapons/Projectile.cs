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
    }
}
