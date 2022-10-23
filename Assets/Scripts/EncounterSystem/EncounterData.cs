using System.Collections.Generic;
using ChoiceSystem;
using UnityEngine;
using Utility;
using ResourceSystem;

namespace EncounterSystem
{
    [CreateAssetMenu(fileName = "Encounter Data", menuName = "Data/Encounter", order = 0)]
    public class EncounterData : ScriptableObject
    {
        [SerializeField] private string encounterTitle;
        [SerializeField] private Sprite encounterSprite;
        [SerializeField][TextArea] private string encounterDescription;
        [SerializeField] private List<SpecialChoiceData> encounterChoiceDataList;
        [SerializeField] private SerializedDictionary<GameResourceTypes, int> Reqs;


        public string EncounterTitle => encounterTitle;
        
        public string EncounterDescription => encounterDescription;

        public List<SpecialChoiceData> EncounterChoiceDataList => encounterChoiceDataList;

        public Sprite EncounterSprite => encounterSprite;

        public bool ReqsSatisfied()
        {
            foreach (var item in Reqs.Keys)
            {
                if (ResourceManager.Instance.GetResourceValue(item) < Reqs[item])
                    return false;
            }

            return true;
        }
    }
}