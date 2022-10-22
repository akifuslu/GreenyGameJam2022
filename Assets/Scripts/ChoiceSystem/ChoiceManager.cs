using System;
using System.Collections.Generic;
using System.Linq;
using ChoiceSystem.CanvasScripts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility;

namespace ChoiceSystem
{
    public class ChoiceManager : MonoBehaviour
    {
        public static ChoiceManager Instance { get; set; }

        [Header("References")] 
       
        [SerializeField] private List<ChoiceDataBase> allChoiceDataList;
        [SerializeField] private ChoiceCanvasBase choiceChoiceCanvas;
        [SerializeField] private SpecialChoiceCanvas specialChoiceChoiceCanvas;
        
        private readonly List<ChoiceCardBase> _spawnedChoiceCardList = new List<ChoiceCardBase>();

        public Action OnChoiceSelectedAction;

        private ChoiceCanvasBase _activeChoiceCanvas;

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
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OpenSpecialChoiceCanvas(allChoiceDataList,"TTTTTT");
            }

            if (Input.GetKeyDown(KeyCode.X))
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
                choiceChoiceCanvas.TitleTextField.gameObject.SetActive(true);
                choiceChoiceCanvas.TitleTextField.text = title;
            }
            else
                choiceChoiceCanvas.TitleTextField.gameObject.SetActive(false);
            
            choiceChoiceCanvas.gameObject.SetActive(true);
            specialChoiceChoiceCanvas.gameObject.SetActive(false);
            _activeChoiceCanvas = choiceChoiceCanvas;
            DisposeSpawnedChoiceCards();
            _activeChoiceCanvas.Build(choices,false);
        }
        
        public void OpenSpecialChoiceCanvas(List<ChoiceDataBase> choices,string mainEventText, string title = "")
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrWhiteSpace(title))
            {
                specialChoiceChoiceCanvas.TitleTextField.gameObject.SetActive(true);
                specialChoiceChoiceCanvas.TitleTextField.text = title;
            }
            else
                specialChoiceChoiceCanvas.TitleTextField.gameObject.SetActive(false);
            
            choiceChoiceCanvas.gameObject.SetActive(false);
            specialChoiceChoiceCanvas.gameObject.SetActive(true);
            specialChoiceChoiceCanvas.SetMainEventText(mainEventText);
            _activeChoiceCanvas = specialChoiceChoiceCanvas;
            DisposeSpawnedChoiceCards();
            _activeChoiceCanvas.Build(choices,true);
        }

        public void CloseChoiceCanvas()
        {
            if (_activeChoiceCanvas)
                _activeChoiceCanvas.gameObject.SetActive(false);  
        }

        public void SpawnChoices(List<ChoiceDataBase> possibleChoiceList,bool isSpecial,int spawnCount = 3)
        {
            DisposeSpawnedChoiceCards();
            for (int i = 0; i < possibleChoiceList.Count; i++)
            {
                var card =SpawnChoice(possibleChoiceList[i], isSpecial);
                card.transform.localScale = Vector3.zero;
                card.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f * (i+1));
            }
        }

        private ChoiceCardBase SpawnChoice(ChoiceDataBase choiceData,bool isSpecial)
        {
            var prefab = isSpecial ? specialChoiceChoiceCanvas.ChoiceCardPrefab : choiceChoiceCanvas.ChoiceCardPrefab;
            var spawnRoot = isSpecial ? specialChoiceChoiceCanvas.ChoiceSpawnRoot : choiceChoiceCanvas.ChoiceSpawnRoot;
            
            var cloneCard = Instantiate(prefab,spawnRoot);
            cloneCard.Build(choiceData, isSpecial);
            _spawnedChoiceCardList.Add(cloneCard);
            return cloneCard;
        }

        private void DisposeSpawnedChoiceCards()
        {
            foreach (var cardBase in _spawnedChoiceCardList)
                Destroy(cardBase.gameObject);
            _spawnedChoiceCardList?.Clear();
        }
    }
}