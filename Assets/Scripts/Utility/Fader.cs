using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utility
{
    public class Fader : MonoBehaviour
    {
        public static Fader Instance { get; set; }
        [SerializeField] private Image fadeImage;
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                fadeImage.gameObject.SetActive(true);
                _fadeTween = fadeImage.DOFade(0f, 0.2f).OnComplete(()=>_fadeTween = null);
                DontDestroyOnLoad(gameObject);
            }
        }


        private Tween _fadeTween;
        public void FadeScene(int targetSceneIndex)
        {
            if (_fadeTween != null)
                return;

            _fadeTween = fadeImage.DOFade(1f, 0.5f).OnComplete(() =>
            {
                SceneManager.LoadScene(targetSceneIndex);
                
                _fadeTween = fadeImage.DOFade(0f, 0.5f).OnComplete(()=>_fadeTween = null);
            });;
        }
    }
}
