using UnityEngine;

namespace ChoiceSystem
{
    [CreateAssetMenu(fileName = "Special Choice Data",menuName = "Data/Special Choice")]
    public class SpecialChoiceData : ChoiceDataBase
    {
        [Header("Special")]
        [SerializeField][TextArea] private string specialDescription;

        public override string GetDescription()
        {
            return specialDescription;
        }
    }
}