using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace ChoiceSystem.CanvasScripts
{
    public class SpecialChoiceCanvas : ChoiceCanvasBase
    {
        [SerializeField] private TextMeshProUGUI mainEventTextField;
        [SerializeField] private Image specialImage;
        [SerializeField] private TextMeshProUGUI resultText;

        private Tween _textDisplayTween;
        public void SetMainEventText(string mainEventText)
        {
            mainEventTextField.text = mainEventText;
            _textDisplayTween =TextHelper.PlayingText(mainEventTextField,onCompletedAction: PublishDescription, onKillAction: PublishDescription);
        }

        public void SetImage(Sprite targetSprite)
        {
            specialImage.sprite = targetSprite;
        }

        private void PublishDescription()
        {
            MessageBus.Publish(new SpecialChoiceDescriptionFinishedEvent());
        }

        public override void OnClosed()
        {
            if(_textDisplayTween != null) _textDisplayTween.Kill();
            resultText.enabled = false;
            Root.gameObject.SetActive(true);
            base.OnClosed();
        }

        public override void ShowResult(string result)
        {
            base.ShowResult(result);
            Root.gameObject.SetActive(false);
            resultText.enabled = true;
            resultText.text = result;
            TextHelper.PlayingText(resultText, 30);
        }
    }
    
    public class SpecialChoiceDescriptionFinishedEvent : GameEvent{}
}