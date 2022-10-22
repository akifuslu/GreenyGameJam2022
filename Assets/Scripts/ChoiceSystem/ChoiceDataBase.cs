using System;
using System.Collections.Generic;
using Grid;
using ResourceSystem;
using UnityEngine;

namespace ChoiceSystem
{
    [CreateAssetMenu(fileName = "Choice Data",menuName = "Data/Choice")]
    public class ChoiceDataBase : ScriptableObject
    {
        [SerializeField] private Sprite choiceSprite;
        [SerializeField][TextArea] private string choiceName;
        [SerializeField][TextArea] private string description;
        [SerializeField] private List<ChoiceActionData> choiceActionDataList;
        public Sprite ChoiceSprite => choiceSprite;

        public string ChoiceName => choiceName;

        public string Description => description;

        public List<ChoiceActionData> ChoiceActionDatas => choiceActionDataList;

        public string GetDescription()
        {
            //return Description;
            string desc = "";
            foreach (var action in choiceActionDataList)
            {
                desc += action + Environment.NewLine;
            }

            return desc;
        }

        public void TriggerChoiceAction()
        {
            foreach (var choiceActionData in choiceActionDataList)
            {
                var act =ChoiceActionProcessor.GetAction(choiceActionData.ActionType);
                act.DoAction(new CardActionParameters(choiceActionData.Value, choiceActionData.Res, choiceActionData.Tile));
            }
        }


        public ChoiceDataBase GetCopy()
        {
            var copy = Instantiate(this);
            copy.choiceActionDataList = new List<ChoiceActionData>();
            foreach (var action in choiceActionDataList)
            {
                copy.choiceActionDataList.Add(new ChoiceActionData(action));
            }
            return copy;
        }
    }

    [Serializable]
    public class ChoiceActionData
    {
        [SerializeField] private ChoiceActionTypes actionType;
        [SerializeField] private GameResourceTypes res;
        [SerializeField] private int value;
        private ResourceTile tile;

        public ChoiceActionTypes ActionType => actionType;
        public int Value => value;
        public GameResourceTypes Res => res;
        public ResourceTile Tile
        {
            get
            {
                return tile;
            }
            set
            {
                tile = value;
            }
        }

        public ChoiceActionData(ChoiceActionData other)
        {
            actionType = other.actionType;
            res = other.res;
            value = other.value;
        }

        public override string ToString()
        {
            switch (actionType)
            {
                case ChoiceActionTypes.Example:
                    return "Example desc";
                case ChoiceActionTypes.GatherResource:
                    return "+" + value + " " + res.ToString();
                case ChoiceActionTypes.ReduceReplenish:
                    return "-" + value + " Replenish";
                default:
                    break;
            }
            return base.ToString();
        }
    }
    
}