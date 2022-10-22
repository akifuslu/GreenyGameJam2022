using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceSystem
{
    public class ResourceCardBase : MonoBehaviour
    {
        [SerializeField] private Transform root;
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
            root.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutBack);
            root.transform.localScale = Vector3.zero;
            root.DOScale(Vector3.one, 0.2f).SetEase(Ease.Linear);
            UpdateResourceCountText();
        }

        public void UpdateResourceCountText()
        {
            resourceCountTextField.text = TotalAmount.ToString();
        }

    }
}