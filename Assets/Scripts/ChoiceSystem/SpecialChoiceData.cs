using System;
using UnityEngine;
using Utility;

namespace ChoiceSystem
{
    [CreateAssetMenu(fileName = "Special Choice Data",menuName = "Data/Special Choice")]
    public class SpecialChoiceData : ChoiceDataBase
    {
        [Header("Special")]
        [SerializeField][TextArea] private string specialDescription;
        [SerializeField][TextArea] private string resultDescription;

        public override string GetPositiveDescription(bool colorize = false)
        {
            return colorize ? ColorExtentions.ColorString(specialDescription,Color.cyan):specialDescription;
        }

        public string GetResultDescription()
        {
            string desc = resultDescription + Environment.NewLine;
            foreach (var action in ChoiceActionDatas)
            {
                desc += action.ToString() + Environment.NewLine;
            }
            return desc;
        }
    }
}