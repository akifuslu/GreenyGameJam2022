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
        [SerializeField] private TextMeshProUGUI positiveDescriptionText;
        [SerializeField] private TextMeshProUGUI negativeDescriptionText;
        
        public ChoiceDataBase MyData { get; private set; }

        public Button CardButton => cardButton;

        private bool special;

        public void Build(ChoiceDataBase choiceData, bool isSpecial, int spawnIndex)
        {
            MyData = choiceData;
            special = isSpecial;
            CardButton.interactable = MyData.GetAvailability();
            CardButton.onClick.AddListener(TriggerChoice);
            choiceImage.sprite = MyData.ChoiceSprite;
            nameTextField.text = MyData.ChoiceName;

            var str = new StringBuilder();
            if (isSpecial)
                str.Append(spawnIndex + 1).Append(". ");
            
            str.Append(MyData.GetPositiveDescription());
            positiveDescriptionText.text = str.ToString();
            
            if (negativeDescriptionText)
            {
                str.Clear();
                str.Append(MyData.GetNegativeDescription());
                negativeDescriptionText.text = str.ToString();
            }

            PlayFx(isSpecial, spawnIndex);
        }

        private void PlayFx(bool isSpecial, int spawnIndex)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f * (spawnIndex + 1));
            if (isSpecial)
                TextHelper.PlayingText(positiveDescriptionText, 20f, 0.2f * (spawnIndex + 1));
        }

        public void TriggerChoice()
        {
            if (special)
            {
                var res = (MyData as SpecialChoiceData).GetResultDescription();
                ChoiceManager.Instance.OnChoiceSelectedSpecial(res, () =>
                {
                    MyData.TriggerChoiceAction();
                });
            }
            else
            {
                MyData.TriggerChoiceAction();
                ChoiceManager.Instance.OnChoiceSelected();
            }
        }
        
    }
}