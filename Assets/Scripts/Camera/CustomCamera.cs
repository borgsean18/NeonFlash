using System;
using UnityEngine;

namespace Camera
{
    public class CustomCamera : MonoBehaviour
    {
        // Exposed Variables
        [Header("Target")]
        [SerializeField] private Transform targetTransform;
        [SerializeField] private float maxDistToTarget;
        [SerializeField] private float xOffsetToPlayer;

        [Header("Clamps")]
        [SerializeField] private bool freezeYPos;
        [SerializeField] private float minYValue;


        private void Update()
        {
            if (targetTransform == null) return;

            FollowTarget();
        }


        public void SetCameraTarget(Transform _target)
        {
            targetTransform = _target;
        }


        private void FollowTarget()
        {
            Vector3 currentPos = transform.position;
            Vector3 targetPos = targetTransform.position;
            
            targetPos.x += xOffsetToPlayer; // Keep camera slightly ahead of player
            
            float distToTarget = Vector2.Distance(currentPos, targetPos);

            // Only proceed through here if the player is far enough from the camera
            if (distToTarget < maxDistToTarget) 
                return;

            float newX = targetPos.x;
            float newY;
            
            if (freezeYPos)
                newY = minYValue;
            else
                newY = Mathf.Clamp(targetPos.y, minYValue, targetPos.y);

            Vector3 newPos = new Vector3(newX, newY, currentPos.z);

            transform.position = Vector3.Lerp(currentPos, newPos, Time.deltaTime * distToTarget);
        }
    }
}
