using System;
using Managers;
using UnityEngine;
using World;

namespace Movement
{
    public class BiomeMovement : HorizMovement
    {
	    // Exposed Variables
	    [SerializeField] private float _bufferSpeed;
	    
	    
	    // Private Variables
	    private bool _init;
	    private WorldManager _worldManager;


	    private void Awake()
	    {
		    Init();
	    }


	    private void Init()
	    {
		    _worldManager = FindObjectOfType<WorldManager>();

		    _init = true;
	    }


	    private float MovementSpeed()
	    {
		    movementSpeed = _worldManager.CurrentSpeed * Time.deltaTime;

		    if (_bufferSpeed > 1)
			    movementSpeed /= _bufferSpeed;
		    
		    return movementSpeed;
	    }
	    
	    
        protected override void SideScroll()
        {
	        if (!_init) return;
	        
            //Calculate the movement distance based on speed and time
            float movementDistance = MovementSpeed();
	        
	        // Get the current position of the GameObject
	        Vector3 currentPosition = transform.position;
            
	        // Calculate the new position
	        Vector3 newPosition = currentPosition - new Vector3(movementDistance, 0, 0);
            
	        // Move the GameObject to the new position
	        transform.position = newPosition;
        }
    }
}
