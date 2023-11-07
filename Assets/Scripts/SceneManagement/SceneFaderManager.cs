using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagement
{
    public class SceneFaderManager : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private GameObject faderObject;
        [SerializeField] private float fadeTime;
        
        
        // Private Variables
        private Image faderImage;
        private FaderState faderState;
        
        
        // Enum
        private enum FaderState
        {
            None,
            Fade
        }



        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            // Keep this object in all scenes
            DontDestroyOnLoad(gameObject);

            faderImage = faderObject.GetComponent<Image>();
        }


        private void Update()
        {
            switch (faderState)
            {
                case FaderState.Fade:
                    break;
                
                case FaderState.None:
                    break;
            }
        }


        public void TransitionToScene(string _sceneName)
        {
            //SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
        }


        private IEnumerator Fade()
        {
            faderState = FaderState.Fade;
            
            yield return new WaitForSeconds(fadeTime);
        }
    }
}
