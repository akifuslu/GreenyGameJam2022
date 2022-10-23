using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Utility;

namespace EndGame
{
    public class EndGamePanelBase : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Transform root;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private PlayableDirector playableDirector;

        private Tweener _counterTween;
        public void Open(int score =-1)
        {
            root.gameObject.SetActive(true);
            backgroundImage.gameObject.SetActive(true);
            playableDirector.Play();
            if (backgroundImage)
            {
                backgroundImage.CrossFadeAlpha(0f,0f,false);
                backgroundImage.CrossFadeAlpha(1f,0.5f,false);
            }

            root.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            
            scoreText.gameObject.SetActive(score>0);
            if (score>-1)
            {
                scoreText.text = "Skor: 0";
                _counterTween =TextHelper.PlayCounter(scoreText, score,startDelay:0.5f,prefix:"Score: ");
            }
        }

        public void Close()
        {
            root.gameObject.SetActive(false);
            backgroundImage.gameObject.SetActive(false);
            if (_counterTween != null)
            {
                _counterTween.Kill();
            }
        }
    }
}