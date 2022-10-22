using System.Collections;
using System.Collections.Generic;
using Flow;
using TMPro;
using UnityEngine;
using UniRx;

namespace Views
{
    public class DayCycleView : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI _dayText;

        public void Bind(DayCycle dayCycle)
        {
            dayCycle.CurrentDay.Subscribe(ev =>
            {
                _dayText.text = "DAY " + ev;
            });
        }
    }
}
