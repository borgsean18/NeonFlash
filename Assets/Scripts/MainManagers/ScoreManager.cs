using System;
using TMPro;
using UnityEngine;

namespace MainManagers
{
    public class ScoreManager : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private TextMeshProUGUI scoreText;
        
        // Private Variables
        private WorldManager worldManager;
        private float score;


        void Awake()
        {
            Init();
        }


        private void Init()
        {
            worldManager = GetComponent<WorldManager>();
            
            worldManager.LoseGame.AddListener(Lose);
        }


        void Update()
        {
            if (worldManager.GameState == GameStates.Play)
            {
                score += Time.deltaTime;

                scoreText.text = (int)score + "";
            }
        }


        private void Lose()
        {
            
        }
    }
}
