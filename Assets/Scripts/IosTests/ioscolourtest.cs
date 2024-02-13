using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace IosTests
{
    public class ioscolourtest : MonoBehaviour
    {
        public float colorChangeSpeed = 1.0f; // Adjust the speed of color change
        private float hue = 0.0f;
        private Image image;


        private void Awake()
        {
            image = GetComponent<Image>();
        }


        private void Update()
        {
            // Update the hue value over time
            hue += colorChangeSpeed * Time.deltaTime;
            
            // Ensure hue stays within the valid range (0 to 1)
            hue = Mathf.Repeat(hue, 1.0f);

            // Convert the hue value to a color
            Color newColor = Color.HSVToRGB(hue, 1.0f, 1.0f);

            // Apply the new color to the image
            image.color = newColor;
        }
    }
}