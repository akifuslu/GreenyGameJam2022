using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceSystem
{
    public class ResourceCardBase : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resourceNameTextField;
        [SerializeField] private TextMeshProUGUI resourceCountTextField;
        [SerializeField] private Image resourceImage;
        [SerializeField] private Image backgroundImage;
        
        
        public int TotalAmount { get; private set; }
        public ResourceData MyData { get; private set; }

        public void IncreaseAmount(int amount)
        {
            TotalAmount += amount;
            UpdateResourceCountText();
        }

        public void DecreaseAmount(int amount)
        {
            TotalAmount -= amount;
            if (TotalAmount<0)
            {
                TotalAmount = 0;
            }
            UpdateResourceCountText();
        }
        
        public void Build(ResourceData targetData)
        {
            MyData = targetData;
            resourceNameTextField.text = targetData.ResourceName;
            resourceImage.sprite = targetData.ResourceSprite;
            backgroundImage.color = targetData.BackgroundColor;
            UpdateResourceCountText();
        }

        public void UpdateResourceCountText()
        {
            resourceCountTextField.text = TotalAmount.ToString();
        }

    }
}