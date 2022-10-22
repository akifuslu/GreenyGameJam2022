using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Utility;

namespace ChoiceSystem
{
    public class ChoiceManager : MonoBehaviour
    {
        public static ChoiceManager Instance { get; set; }

        [Header("References")] 
        [SerializeField] private ChoiceCardBase choiceCardPrefab;
        [SerializeField] private RectTransform choiceSpawnRoot;
        [SerializeField] private TextMeshProUGUI titleTextField;
        [SerializeField] private List<ChoiceDataBase> allChoiceDataList;
        [SerializeField] private Canvas choiceCanvas;
        
        private readonly List<ChoiceCardBase> _spawnedChoiceCardList = new List<ChoiceCardBase>();

        public Action OnChoiceSelectedAction;

        private void Awake()
        {
            Instance = this;
            ChoiceActionProcessor.Initialize();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OpenChoiceCanvas(allChoiceDataList);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                CloseChoiceCanvas();
            }
#endif
        }

        public void OnChoiceSelected()
        {
            DisposeSpawnedChoiceCards();
            CloseChoiceCanvas();
            OnChoiceSelectedAction?.Invoke();
        }
        public void OpenChoiceCanvas(List<ChoiceDataBase> choices, string title = "")
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrWhiteSpace(title))
            {
                titleTextField.gameObject.SetActive(true);
                titleTextField.text = title;
            }
            else
                titleTextField.gameObject.SetActive(false);
            
            choiceCanvas.gameObject.SetActive(true);
            SpawnChoices(choices);
        }

        public void CloseChoiceCanvas()
        {
            choiceCanvas.gameObject.SetActive(false);
        }

        public void SpawnChoices(List<ChoiceDataBase> possibleChoiceList,int spawnCount = 3)
        {
            DisposeSpawnedChoiceCards();
            //var tempList = possibleChoiceList.ToList();
            //for (int i = 0; i < spawnCount; i++)
            //{
            //    var randomChoice = tempList.RandomItem();
            //    if (!randomChoice) continue;

            //    SpawnChoice(randomChoice);
            //    tempList.Remove(randomChoice);
            //}
            for (int i = 0; i < possibleChoiceList.Count; i++)
            {
                SpawnChoice(possibleChoiceList[i]);
            }
        }

        private void SpawnChoice(ChoiceDataBase choiceData)
        {
            var cloneCard = Instantiate(choiceCardPrefab, choiceSpawnRoot);
            cloneCard.Build(choiceData);
            _spawnedChoiceCardList.Add(cloneCard);
        }

        private void DisposeSpawnedChoiceCards()
        {
            foreach (var cardBase in _spawnedChoiceCardList)
                Destroy(cardBase.gameObject);
            _spawnedChoiceCardList?.Clear();
        }
    }
}