using UnityEngine;

namespace ChoiceSystem.ChoiceActions
{
    public class GatherResourceAction : ChoiceActionBase
    {
        public override ChoiceActionTypes ActionType => ChoiceActionTypes.GatherResource;
        public override void DoAction(CardActionParameters actionParameters)
        {
            ResourceSystem.ResourceManager.Instance.IncreaseResource(actionParameters.Res, actionParameters.Value);

            actionParameters.Tile.ReduceResource(actionParameters.Value);
        }
    }
}