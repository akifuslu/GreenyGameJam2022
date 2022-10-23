using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UniRx;
using Utility;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

namespace Views
{
    public class TradeView : MonoBehaviour
    {

        [SerializeField]
        private Button _close;

        private CanvasGroup _cg;
        private List<TradeButton> _buttons;

        private void Awake()
        {
            _cg = GetComponent<CanvasGroup>();
            _buttons = GetComponentsInChildren<TradeButton>().ToList();
            _close.onClick.AddListener(() =>
            {
                Hide();
            });

            MessageBus.OnEvent<ShowTradeEvent>().Subscribe(ev =>
            {
                Show();
            });
        }

        private void Show()
        {
            _cg.blocksRaycasts = true;
            _cg.DOFade(1f, .5f);

            MessageBus.Publish(new RefreshTradeButtonEvent());
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].transform.DOShakeScale(.25f).SetDelay(i * 0.1f);
            }
        }

        private void Hide()
        {
            _cg.blocksRaycasts = false;
            _cg.DOFade(0f, .25f);
        }
    }


}
