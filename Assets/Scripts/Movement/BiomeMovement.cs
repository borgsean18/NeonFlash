using System;
using MainManagers;
using UnityEngine;
using World;

namespace Movement
{
    public class BiomeMovement : HorizMovement
    {
	    // Exposed Variables
	    [Header("Biome Movement Variables")]
	    [SerializeField] private float _bufferSpeed;


	    protected override void Awake()
	    {
		    base.Awake();
		    
		    Init();
	    }


	    private void Init()
	    {
		    moveWithWorld = true;
	    }


	    protected override float CalcMovementDistance()
	    {
		    movementSpeed = worldManager.CurrentSpeed * Time.deltaTime;

		    if (_bufferSpeed > 1)
			    movementSpeed /= _bufferSpeed;
		    
		    return movementSpeed *= -1;
	    }
    }
}
