namespace ChoiceSystem
{
    public class CardActionParameters
    {
        public readonly float Value;
      
        public CardActionParameters(float value)
        {
            Value = value;
        }
    }
    public abstract class ChoiceActionBase
    {
        protected ChoiceActionBase(){}
        public abstract ChoiceActionTypes ActionType { get;}
        public abstract void DoAction(CardActionParameters actionParameters);
    }
}