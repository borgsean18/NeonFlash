using UnityEngine;

namespace Movement
{
    public class HorizMovement : MonoBehaviour
    {
        // Exposed Variables
        [Header("Settings")]
        [SerializeField] protected float movementSpeed;
        [SerializeField] private bool moveLeft;
        
        
        // Update is called once per frame
        void Update()
        {
            SideScroll();
        }

        
        /// <summary>
        /// Virtual method used to move objects sideways only in the world,
        /// depending on if moveLeft is true or false, objects will move in
        /// that direction.
        /// </summary>
        protected virtual void SideScroll()
        {
            // Calculate the movement distance based on speed and time
            float movementDistance = movementSpeed * Time.deltaTime;

            // Get the current position of the GameObject
            Vector3 currentPosition = transform.position;

            // Calculate the new position
            Vector3 newPosition = currentPosition + new Vector3(movementDistance, 0, 0);

            if (moveLeft)
                newPosition.x *= -1;

            // Move the GameObject to the new position
            transform.position = newPosition;
        }
    }
}
