using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Grid
{
    public class TradeTile : Tile
    {
        public override void OnEnter()
        {
            base.OnEnter();

            MessageBus.Publish(new ShowTradeEvent());
        }
    }

    public class ShowTradeEvent : GameEvent
    {

    }
}
