using System.Collections.Generic;
using ChoiceSystem;
using UnityEngine;

namespace EncounterSystem
{
    [CreateAssetMenu(fileName = "Encounter Data", menuName = "Data/Encounter", order = 0)]
    public class EncounterData : ScriptableObject
    {
        [SerializeField] private string encounterTitle;
        [SerializeField] private Sprite encounterSprite;
        [SerializeField][TextArea] private string encounterDescription;
        [SerializeField] private List<SpecialChoiceData> encounterChoiceDataList;


        public string EncounterTitle => encounterTitle;
        
        public string EncounterDescription => encounterDescription;

        public List<SpecialChoiceData> EncounterChoiceDataList => encounterChoiceDataList;

        public Sprite EncounterSprite => encounterSprite;
    }
}