using ResourceSystem;

namespace ChoiceSystem.ChoiceActions
{
    public class GiveAllResources : ChoiceActionBase
    {
        public override ChoiceActionTypes ActionType => ChoiceActionTypes.GiveAll;
        public override void DoAction(CardActionParameters actionParameters)
        {
            if (actionParameters.Value>0)
            {
                var electedResource = ResourceManager.Instance.GetResourceCard(actionParameters.Res);
                ResourceManager.Instance.DecreaseResource(electedResource.MyData.ResourceType, electedResource.TotalAmount);
                return;
            }
            foreach (var resourceCardBase in ResourceManager.Instance.SpawnedResourceCardDict)
                ResourceManager.Instance.DecreaseResource(resourceCardBase.Key, resourceCardBase.Value.TotalAmount);
            
        }
    }
}