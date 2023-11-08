using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneFaderManager : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private GameObject faderObject;
        [SerializeField] private float fadeTime;
        
        
        // Private Variables
        private Animator _animator;
        private bool init;



        private void Awake()
        {
            if (!init)
                Init();
        }


        private void Init()
        {
            // Keep this object in all scenes
            DontDestroyOnLoad(gameObject);

            _animator = GetComponent<Animator>();

            init = true;
        }


        public void TransitionToScene(string _sceneName)
        {
            StartCoroutine(FadeIn(_sceneName));
        }


        private IEnumerator FadeIn([CanBeNull] string _sceneName)
        {
            _animator.SetBool("FadeIn", true);

            if (_sceneName == "")
                yield return null;

            yield return new WaitForSeconds(fadeTime);
                ChangeScene(_sceneName);
        }


        public void CompleteSceneTransition()
        {
            StartCoroutine(FadeOut());
        }


        private IEnumerator FadeOut()
        {
            if (!init)
                Init();
            
            if (!_animator.GetBool("FadeIn"))
                yield return null;
            
            yield return new WaitForSeconds(1f);
            
            if(_animator.GetBool("FadeIn"))
            {
                _animator.SetBool("FadeOut", true);
                _animator.SetBool("FadeIn", false);
            }
        }


        private void ChangeScene(string _sceneName)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
