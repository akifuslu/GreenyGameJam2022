using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChoiceSystem
{
    public class ChoiceCardBase : MonoBehaviour
    {
        [SerializeField] private Button cardButton;
        [SerializeField] private Image choiceImage;
        [SerializeField] private TextMeshProUGUI nameTextField;
        [SerializeField] private TextMeshProUGUI descriptionTextField;
        
        public ChoiceDataBase MyData { get; private set; }
        public void Build(ChoiceDataBase choiceData)
        {
            MyData = choiceData;
            cardButton.onClick.AddListener(TriggerChoice);
            choiceImage.sprite = choiceData.ChoiceSprite;
            nameTextField.text = choiceData.ChoiceName;
            descriptionTextField.text = choiceData.GetDescription();
        }

        public void TriggerChoice()
        {
            MyData.TriggerChoiceAction();
        }
        
    }
}