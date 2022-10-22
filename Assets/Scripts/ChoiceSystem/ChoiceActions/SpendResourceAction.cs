using UnityEngine;

namespace ChoiceSystem.ChoiceActions
{
    public class SpendResourceAction : ChoiceActionBase
    {
        public override ChoiceActionTypes ActionType => ChoiceActionTypes.SpendResource;
        public override void DoAction(CardActionParameters actionParameters)
        {
            actionParameters.Tile.ReduceResource(actionParameters.Value);
            ResourceSystem.ResourceManager.Instance.DecreaseResource(actionParameters.Res, actionParameters.Value);
        }
    }
}