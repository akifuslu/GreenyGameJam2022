using ResourceSystem;

namespace ChoiceSystem.ChoiceActions
{
    public class InstantSpendResource : ChoiceActionBase
    {
        public override ChoiceActionTypes ActionType => ChoiceActionTypes.InstantSpendResource;
        public override void DoAction(CardActionParameters actionParameters)
        {
            ResourceManager.Instance.DecreaseResource(actionParameters.Res,actionParameters.Value);
        }
    }
}