using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Grid
{
    public class EndTile : Tile
    {
        public override void OnEnter()
        {
            base.OnEnter();
            MessageBus.Publish(new EndTileReachedEvent());
        }
    }

    public class EndTileReachedEvent : GameEvent { }
}
