using System;
using System.Collections.Generic;
using System.Linq;
using ChoiceSystem.CanvasScripts;
using DG.Tweening;
using TMPro;
using UniRx;
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

            if (Input.GetMouseButtonDown(0))
                MessageBus.Publish(new OnMouseButtonDownEvent());
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
            _activeChoiceCanvas.NoChoiceButton.gameObject.SetActive(false);
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
            _activeChoiceCanvas.NoChoiceButton.gameObject.SetActive(false);
            _activeChoiceCanvas.Build(choices,true);
        }

        public void CloseChoiceCanvas()
        {
            if (_activeChoiceCanvas)
            {
                _activeChoiceCanvas.OnClosed();
            }
        }

        public void SpawnChoices(List<ChoiceDataBase> possibleChoiceList,bool isSpecial,int spawnCount = 3)
        {
            DisposeSpawnedChoiceCards();
            if (isSpecial)
            {
                MessageBus.OnEvent<SpecialChoiceDescriptionFinishedEvent>().Take(1).Subscribe(ev =>
                {
                    PrepareSpawn(possibleChoiceList, true);
                });
            }
            else
            {
                PrepareSpawn(possibleChoiceList, false);
            }
        }

        private void PrepareSpawn(List<ChoiceDataBase> possibleChoiceList, bool isSpecial)
        {
            for (int i = 0; i < possibleChoiceList.Count; i++)
            {
                SpawnChoice(possibleChoiceList[i], isSpecial, i);
            }

            if (_activeChoiceCanvas)
            {
                var hasNoOption = _spawnedChoiceCardList.All(x => !x.CardButton.interactable);
                if (hasNoOption)
                {
                    _activeChoiceCanvas.NoChoiceButton.gameObject.SetActive(true);
                    _activeChoiceCanvas.NoChoiceButton.transform.localScale = Vector3.zero;
                    _activeChoiceCanvas.NoChoiceButton.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack)
                        .SetDelay(0.5f);
                }
            }
        }

        private ChoiceCardBase SpawnChoice(ChoiceDataBase choiceData,bool isSpecial, int index)
        {
            var prefab = isSpecial ? specialChoiceChoiceCanvas.ChoiceCardPrefab : choiceChoiceCanvas.ChoiceCardPrefab;
            var spawnRoot = isSpecial ? specialChoiceChoiceCanvas.ChoiceSpawnRoot : choiceChoiceCanvas.ChoiceSpawnRoot;
            
            var cloneCard = Instantiate(prefab,spawnRoot);
            cloneCard.Build(choiceData, isSpecial,index);
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
    
    public class OnMouseButtonDownEvent : GameEvent { }
}