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
            
            MessageBus.OnEvent<OnMouseButtonDownEvent>().Subscribe(ev =>
            {
                if (t != null)
                    t.Kill();
            });
            
            return t;
        }
    }
}