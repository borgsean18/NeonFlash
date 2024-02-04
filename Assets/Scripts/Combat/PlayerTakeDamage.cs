using Characters;
using Combat;
using Movement;
using UnityEngine;
using UnityEngine.Scripting;

namespace Combat
{
    public class PlayerTakeDamage : TakeDamage
    {   
        // Exposed Variables
        [Header("Debug Settings")]
        [SerializeField] private bool _isImmortal;

        // Private Variables
        private Player player;


        protected override void Awake()
        {
            player = GetComponent<Player>();
        }


        protected override void Die()
        {
            if (!_isImmortal)
                player.GameManagerScript.LoseGame.Invoke();
        }
    }
}

