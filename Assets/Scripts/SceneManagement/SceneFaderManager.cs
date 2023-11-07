using System.Collections;
using JetBrains.Annotations;
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
        private Animator _animator;



        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            // Keep this object in all scenes
            DontDestroyOnLoad(gameObject);

            _animator = GetComponent<Animator>();
        }


        public void TransitionToScene(string _sceneName)
        {
            StartCoroutine(FadeIn(_sceneName));
        }


        private IEnumerator FadeIn([CanBeNull] string _sceneName)
        {
            _animator.SetTrigger("FadeIn");

            if (_sceneName == "")
                yield return null;

            yield return new WaitForSeconds(fadeTime);
                ChangeScene(_sceneName);
        }


        private void FadeOut()
        {
            
        }


        private void ChangeScene(string _sceneName)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
