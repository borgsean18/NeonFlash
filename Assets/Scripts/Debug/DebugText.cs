using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Debugging
{
    public class DebugText : MonoBehaviour
    {
        private TextMeshProUGUI debugText;


        // Singleton
        public static DebugText Singleton;


        private void Awake()
        {
            Singleton = this;
            
            debugText = GetComponent<TextMeshProUGUI>();
        }


        public void DisplayMessage(string _message)
        {
            debugText.text = _message;
        }
    }
}