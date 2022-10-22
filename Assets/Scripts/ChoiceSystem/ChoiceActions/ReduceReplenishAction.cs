using UnityEngine;

namespace ChoiceSystem.ChoiceActions
{
    public class ReduceReplenishAction : ChoiceActionBase
    {
        public override ChoiceActionTypes ActionType => ChoiceActionTypes.ReduceReplenish;
        public override void DoAction(CardActionParameters actionParameters)
        {
            actionParameters.Tile.ReduceReplenish(actionParameters.Value);
        }
    }
}