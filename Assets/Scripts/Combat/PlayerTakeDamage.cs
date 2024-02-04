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

        // Private Variables
        private Player player;


        protected override void Awake()
        {
            player = GetComponent<Player>();
        }


        protected override void Die()
        {
            player.GameManagerScript.LoseGame.Invoke();
        }
    }
}

