using Camera;
using UnityEngine;

namespace Characters
{
    public class Player : MonoBehaviour
    {
        // Exposed Variables
        [Header("Settings")]
        [SerializeField] private CustomCamera playerCamera;


        public void Init()
        {
            if (playerCamera == null)
                playerCamera = FindObjectOfType<CustomCamera>();
            
            playerCamera.SetCameraTarget(transform);
        }
    }
}
