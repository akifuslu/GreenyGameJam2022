using System.Collections.Generic;
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

            List<ResourceCardBase> _tList = new List<ResourceCardBase>();

            foreach (var resourceCardBase in ResourceManager.Instance.SpawnedResourceCardDict)
            {
                if (resourceCardBase.Value)
                    _tList.Add(resourceCardBase.Value);
            }
            
            foreach (var resourceCardBase in _tList)
                ResourceManager.Instance.DecreaseResource(resourceCardBase.MyData.ResourceType, resourceCardBase.TotalAmount);
            
        }
    }
}