using System;
using System.Collections.Generic;
using System.Linq;
using ChoiceSystem;
using UnityEngine;
using Utility;

namespace EncounterSystem
{
    public class EncounterManager : MonoBehaviour
    {
        public static EncounterManager Instance;
        [SerializeField] private List<EncounterData> allEncounterDatalist;
        private void Awake()
        {
            Instance = this;
        }


        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.K))
            {
                TriggerRandomEncounter();
            }
#endif
        }

        public void TriggerEncounter(EncounterData targetData)
        {
            var choices = targetData.EncounterChoiceDataList.ToList();
            var tList = new List<ChoiceDataBase>();
            foreach (var choiceData in choices)
                tList.Add(choiceData);
            
            ChoiceManager.Instance.OpenSpecialChoiceCanvas(tList,targetData.EncounterDescription,targetData.EncounterSprite,targetData.EncounterTitle);
        }

        public void TriggerRandomEncounter()
        {
            var rand = allEncounterDatalist.RandomItem();
            if (rand)
            {
                TriggerEncounter(rand);
            }
        }
    }
}