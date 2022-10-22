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
            _textDisplayTween =TextHelper.PlayingText(mainEventTextField,onCompletedAction: PublishDescription, onKillAction: PublishDescription);
        }

        private void PublishDescription()
        {
            MessageBus.Publish(new SpecialChoiceDescriptionFinishedEvent());
        }

        public override void OnClosed()
        {
            if(_textDisplayTween != null) _textDisplayTween.Kill();
            base.OnClosed();
        }
    }
    
    public class SpecialChoiceDescriptionFinishedEvent : GameEvent{}
}