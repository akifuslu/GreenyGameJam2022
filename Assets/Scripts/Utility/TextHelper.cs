using ChoiceSystem;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace Utility
{
    public static class TextHelper
    {
        public static Tweener PlayingText(TextMeshProUGUI textField,float durationRate = 10f, float startDelay =0f)
        {
            var charCount = textField.text.Length;
            var showDuration = Mathf.RoundToInt(charCount / durationRate);
            textField.maxVisibleCharacters = 0;
          
            var t =DOTween.To(value =>
                {
                    textField.maxVisibleCharacters = Mathf.RoundToInt(value);
                }, 0, charCount, showDuration).SetDelay(startDelay)
                .OnKill((() =>
                {
                    textField.maxVisibleCharacters = charCount;
                }));
            
            MessageBus.OnEvent<OnMouseButtonDownEvent>().Take(1).Subscribe(ev =>
            {
                if (t != null)
                    t.Kill();
            });
            
            return t;
        }

        public static Tweener PlayCounter(TextMeshProUGUI textField,int targetValue,int startValue =0,string prefix = "",float duration = 1f, float startDelay =0f)
        {
            var t = DOTween
                .To(value => { textField.text = prefix+Mathf.RoundToInt(value).ToString(); }, startValue, targetValue,
                    duration).SetDelay(startDelay).SetEase(Ease.Linear)
                .OnComplete(() => { textField.text =  prefix+targetValue.ToString(); })
                .OnKill(() => { textField.text =  prefix+targetValue.ToString(); });
            MessageBus.OnEvent<OnMouseButtonDownEvent>().Take(1).Subscribe(ev =>
            {
                if (t != null)
                    t.Kill();
            });
            return t;
        }
    }
    
   
}