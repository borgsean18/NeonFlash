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
        private GameManagerScript gameManager;
        private float score;


        void Awake()
        {
            Init();
        }


        private void Init()
        {
            gameManager = GetComponent<GameManagerScript>();
            
            gameManager.LoseGame.AddListener(Lose);
        }


        void Update()
        {
            if (gameManager.GameState == GameStates.Play)
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
