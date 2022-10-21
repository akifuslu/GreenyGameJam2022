using UnityEngine;

namespace ChoiceSystem.ChoiceActions
{
    public class ExampleChoiceAction : ChoiceActionBase
    {
        public override ChoiceActionTypes ActionType => ChoiceActionTypes.Example;
        public override void DoAction(CardActionParameters actionParameters)
        {
            Debug.Log("Example Choice Action" + actionParameters.Value);
        }
    }
}