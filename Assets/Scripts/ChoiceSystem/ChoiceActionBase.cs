using Grid;
using ResourceSystem;

namespace ChoiceSystem
{
    public class CardActionParameters
    {
        public readonly int Value;
        public readonly GameResourceTypes Res;
        public readonly ResourceTile Tile;
      
        public CardActionParameters(int value, GameResourceTypes res, ResourceTile tile)
        {
            Value = value;
            Res = res;
            Tile = tile;
        }
    }
    public abstract class ChoiceActionBase
    {
        protected ChoiceActionBase(){}
        public abstract ChoiceActionTypes ActionType { get;}
        public abstract void DoAction(CardActionParameters actionParameters);
    }
}