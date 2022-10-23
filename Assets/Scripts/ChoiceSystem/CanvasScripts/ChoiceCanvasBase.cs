using System;
using System.Collections.Generic;
using Audio;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChoiceSystem.CanvasScripts
{
    public class ChoiceCanvasBase : MonoBehaviour
    {
        [SerializeField] private ChoiceCardBase choiceCardPrefab;
        [SerializeField] private Transform choiceSpawnRoot;
        [SerializeField] private TextMeshProUGUI titleTextField;
        [SerializeField] private Transform root;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Button noChoiceButton;
        
        public ChoiceCardBase ChoiceCardPrefab => choiceCardPrefab;

        public Transform ChoiceSpawnRoot => choiceSpawnRoot;

        public TextMeshProUGUI TitleTextField => titleTextField;

        public Transform Root => root;

        public Button NoChoiceButton => noChoiceButton;

        private void Awake()
        {
            NoChoiceButton.onClick.AddListener(OnClosed);
            NoChoiceButton.onClick.AddListener(PlaySfx.PlayNegativeButtonClick);
        }

        public virtual void Build(List<ChoiceDataBase> choices, bool isSpecial)
        {
            backgroundImage.CrossFadeAlpha(0f,0f,false);
            backgroundImage.CrossFadeAlpha(1f, 1f, false);
            Root.localScale = Vector3.zero;
            Root.DOScale(Vector3.one, 0.2f).OnComplete(()=>ChoiceManager.Instance.SpawnChoices(choices,isSpecial));
            PlaySfx.PlayPopupShown();
        }
        
        public virtual void OnClosed()
        {
            gameObject.SetActive(false);  
        }

        public virtual void ShowResult(string result)
        {

        }
    }
}