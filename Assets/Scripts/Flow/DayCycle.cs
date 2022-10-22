using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Utility;
using Models;
using Grid;

namespace Flow
{
    public class DayCycle : MonoBehaviour
    {

        public IntReactiveProperty CurrentDay = new IntReactiveProperty();


        private void Awake()
        {
            MessageBus.OnEvent<EndTileReachedEvent>().Subscribe(ev =>
            {
                NextDay();
            });
        }

        public void NextDay()
        {
            CurrentDay.Value += 1;
        }
    }
}
