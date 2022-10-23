using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceSystem;
using UnityEngine.UI;
using TMPro;
using Utility;
using UniRx;

namespace Views
{
    public class TradeButton : MonoBehaviour
    {

        public GameResourceTypes Res;
        public int Cost;
        private TextMeshProUGUI _costText;

        private void Start()
        {
            _costText = GetComponentInChildren<TextMeshProUGUI>();
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Cost <= ResourceManager.Instance.GetResourceValue(GameResourceTypes.Water))
                {
                    ResourceManager.Instance.DecreaseResource(GameResourceTypes.Water, Cost);
                    ResourceManager.Instance.IncreaseResource(Res, 1);

                    MessageBus.Publish(new RefreshTradeButtonEvent());
                }
            });

            MessageBus.OnEvent<RefreshTradeButtonEvent>().Subscribe(ev =>
            {
                RefreshText();
            });
        }

        private void RefreshText()
        {
            if (Cost <= ResourceManager.Instance.GetResourceValue(GameResourceTypes.Water))
            {
                _costText.color = Color.green;
            }
            else
            {
                _costText.color = Color.red;
            }
        }
    }

    public class RefreshTradeButtonEvent : GameEvent { }
}
