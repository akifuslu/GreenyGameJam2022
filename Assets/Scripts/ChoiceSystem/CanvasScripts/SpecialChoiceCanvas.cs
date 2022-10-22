using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility;

namespace ChoiceSystem.CanvasScripts
{
    public class SpecialChoiceCanvas : ChoiceCanvasBase
    {
        [SerializeField] private TextMeshProUGUI mainEventTextField;

        private Tween _textDisplayTween;
        public void SetMainEventText(string mainEventText)
        {
            mainEventTextField.text = mainEventText;
            _textDisplayTween =TextHelper.PlayingText(mainEventTextField);
        }

        public override void OnClosed()
        {
            base.OnClosed();
            if(_textDisplayTween != null) _textDisplayTween.Kill();
        }
    }
}