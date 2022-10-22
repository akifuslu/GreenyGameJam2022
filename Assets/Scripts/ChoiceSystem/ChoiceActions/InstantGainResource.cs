using ResourceSystem;

namespace ChoiceSystem.ChoiceActions
{
    public class InstantGainResource : ChoiceActionBase
    {
        public override ChoiceActionTypes ActionType => ChoiceActionTypes.InstantGainResource;
        public override void DoAction(CardActionParameters actionParameters)
        {
            ResourceManager.Instance.IncreaseResource(actionParameters.Res,actionParameters.Value);
        }
    }
}