using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

namespace ChoiceSystem
{
    public class ChoiceCardBase : MonoBehaviour
    {
        [SerializeField] private Button cardButton;
        [SerializeField] private Image choiceImage;
        [SerializeField] private TextMeshProUGUI nameTextField;
        [SerializeField] private TextMeshProUGUI descriptionTextField;
        
        public ChoiceDataBase MyData { get; private set; }
        public int SpawnIndex { get; private set; }
        public void Build(ChoiceDataBase choiceData, bool isSpecial,int spawnIndex)
        {
            MyData = choiceData;
            SpawnIndex = spawnIndex;
            cardButton.onClick.AddListener(TriggerChoice);
            choiceImage.sprite = choiceData.ChoiceSprite;
            nameTextField.text = choiceData.ChoiceName;
            
            var str = new StringBuilder();
            if (isSpecial)
                str.Append(spawnIndex +1).Append(". ");
            
            str.Append(choiceData.GetDescription());
            descriptionTextField.text = str.ToString();
            
            PlayFx(isSpecial, spawnIndex);
        }

        private void PlayFx(bool isSpecial, int spawnIndex)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f * (spawnIndex + 1));
            if (isSpecial)
                TextHelper.PlayingText(descriptionTextField, 20f, 0.2f * (spawnIndex + 1));
        }

        public void TriggerChoice()
        {
            MyData.TriggerChoiceAction();
            ChoiceManager.Instance.OnChoiceSelected();
        }
        
    }
}