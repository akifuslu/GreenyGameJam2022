using System.Collections;
using System.Collections.Generic;
using Flow;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;
using Utility;
using EndGame;

namespace Views
{
    public class DayCycleView : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI _dayText;
        [SerializeField]
        private Image _overlay;


        public void Bind(DayCycle dayCycle)
        {
            _overlay.color = Color.black;
            dayCycle.CurrentDay.Subscribe(ev =>
            {
                if (ev == 0)
                    return;

                if(ev > dayCycle.TotalDays)
                {
                    MessageBus.Publish(new EndGameEvent());
                    return;
                }

                _overlay.raycastTarget = true;
                _overlay.DOFade(1, .25f).OnComplete(() =>
                {
                    MessageBus.Publish(new DayEndedEvent());
                    _overlay.DOFade(0, .25f).From(1).SetDelay(1.5f)
                        .OnComplete(() =>
                        {
                            _overlay.raycastTarget = false;
                            MessageBus.Publish(new DayStartedEvent());
                        });

                    _dayText.text = "GÜN " + ev + "/" + dayCycle.TotalDays;
                    //_dayText.rectTransform.anchoredPosition = Vector2.zero;
                    _dayText.rectTransform.DOAnchorPosY(0f, .5f).SetDelay(1).SetEase(Ease.InBack);
                });
            });
        }
    }

    public class DayStartedEvent : GameEvent { }

    public class DayEndedEvent : GameEvent { }
}
