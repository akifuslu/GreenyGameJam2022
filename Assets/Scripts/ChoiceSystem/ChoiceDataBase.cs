using System;
using System.Collections.Generic;
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

        public string GetDescription()
        {
            return Description;
        }

        public void TriggerChoiceAction()
        {
            foreach (var choiceActionData in choiceActionDataList)
            {
                var act =ChoiceActionProcessor.GetAction(choiceActionData.ActionType);
                act.DoAction(new CardActionParameters(choiceActionData.Value));
            }
        }
    }

    [Serializable]
    public class ChoiceActionData
    {
        [SerializeField] private ChoiceActionTypes actionType;
        [SerializeField] private int value;


        public ChoiceActionTypes ActionType => actionType;

        public int Value => value;
    }
    
}