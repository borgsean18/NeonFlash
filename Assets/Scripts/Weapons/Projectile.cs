using UnityEngine;


namespace Weapons
{
    [RequireComponent(typeof(DirectProjectile))]
    public class Projectile : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed = 5f;

        
        // Private Variables
        private Transform _target;
        private Vector3 _targetPos;
        private GameObject _owner;


        // Components
        private DirectProjectile _directProjectile;
        
        
        // Properties
        public GameObject Owner => _owner;
        public float Speed => _speed;
        public int Damage => _damage;


        public void Init(Transform target, GameObject owner)
        {
            _target = target;
            _targetPos = _target.position;
            _owner = owner;

            _directProjectile = GetComponent<DirectProjectile>();
            _directProjectile.SetUpMovement(_target, this);
        }


        public void ChangeOwner(GameObject _newOwner)
        {
            _owner = _newOwner;
        }
    }
}
