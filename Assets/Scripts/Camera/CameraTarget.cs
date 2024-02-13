using UnityEngine;

namespace Camera
{
    public class CameraTarget : MonoBehaviour
    {
        private CustomCamera customCamera;


        private void Awake()
        {
            customCamera = FindObjectOfType<CustomCamera>();

            customCamera. SetCameraTarget(transform);
        }
    }
}