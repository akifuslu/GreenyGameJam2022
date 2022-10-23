using System;
using System.Collections.Generic;
using Grid;
using ResourceSystem;
using UnityEngine;
using System.Linq;
using Utility;

namespace ChoiceSystem
{
    [CreateAssetMenu(fileName = "Choice Data",menuName = "Data/Choice")]
    public class ChoiceDataBase : ScriptableObject
    {
        [SerializeField] private Sprite choiceSprite;
        [SerializeField] private string choiceName;
        [SerializeField] private List<ChoiceActionData> choiceActionDataList;
        public Sprite ChoiceSprite => choiceSprite;

        public string ChoiceName => choiceName;
        
        public List<ChoiceActionData> ChoiceActionDatas => choiceActionDataList;

        public virtual string GetPositiveDescription(bool colorize = false)
        {
            //return Description;
            string desc = "";
            foreach (var action in choiceActionDataList)
            {
                if (action.ActionType is ChoiceActionTypes.GatherResource)
                    desc += action.ToString() + Environment.NewLine;
               
            }

            return colorize ?ColorExtentions.ColorString(desc,new Color(0.32f, 1f, 0.48f)) : desc;
        }
        
        public virtual string GetNegativeDescription(bool colorize = false)
        {
            //return Description;
            string desc = "";
            foreach (var action in choiceActionDataList)
            {
                if (action.ActionType == ChoiceActionTypes.ReduceReplenish || action.ActionType == ChoiceActionTypes.SpendResource )
                    desc += action.ToString() + Environment.NewLine;
            }

            return colorize ?ColorExtentions.ColorString(desc,new Color(1f, 0.17f, 0.18f)) : desc;
        }

        public bool GetAvailability()
        {
            var spends = choiceActionDataList.Where(c => c.ActionType == ChoiceActionTypes.SpendResource);
            foreach (var spend in spends)
            {
                if (ResourceManager.Instance.GetResourceValue(spend.Res) < spend.BaseValue)
                {
                    return false;
                }
            }

            return true;
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
        [SerializeField] private int baseValue;
        private int value;
        private ResourceTile tile;

        public ChoiceActionTypes ActionType => actionType;

        public int BaseValue => baseValue;

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
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
            value = other.baseValue;
            baseValue = other.baseValue;
        }

        public override string ToString()
        {
            switch (actionType)
            {
                case ChoiceActionTypes.Example:
                    return "Example desc";
                case ChoiceActionTypes.GatherResource:
                    return "+" + Value + " <sprite=" + (int)res + ">";
                case ChoiceActionTypes.ReduceReplenish:
                    return "-" + Value + " Replenish";
                case ChoiceActionTypes.SpendResource:
                    return "-" + Value + " <sprite=" + (int)res + ">";
                default:
                    break;
            }
            return base.ToString();
        }
    }
    
}