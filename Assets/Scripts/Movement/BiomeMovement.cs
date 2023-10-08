using System;
using Managers;
using UnityEngine;
using World;

namespace Movement
{
    public class BiomeMovement : HorizMovement
    {
	    // Private Variables
	    private bool _init;
	    private WorldManager _worldManager;


	    public void Init(WorldManager _worldManager)
	    {
		    this._worldManager = _worldManager;

		    _init = true;
	    }
	    
	    
        protected override void SideScroll()
        {
	        if (!_init) return;
	        
            //Calculate the movement distance based on speed and time
	        float movementDistance = _worldManager.CalculateCurrentSpeed() * Time.deltaTime;
	        
	        // Get the current position of the GameObject
	        Vector3 currentPosition = transform.position;
            
	        // Calculate the new position
	        Vector3 newPosition = currentPosition - new Vector3(movementDistance, 0, 0);
            
	        // Move the GameObject to the new position
	        transform.position = newPosition;
        }
    }
}
