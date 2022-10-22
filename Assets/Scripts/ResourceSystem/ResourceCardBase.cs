using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceSystem
{
    public class ResourceCardBase : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private Transform shakeRoot;
        [SerializeField] private TextMeshProUGUI resourceNameTextField;
        [SerializeField] private TextMeshProUGUI resourceCountTextField;
        [SerializeField] private Image resourceImage;
        [SerializeField] private Image backgroundImage;
        
        
        public int TotalAmount { get; private set; }
        public ResourceData MyData { get; private set; }

        public TextMeshProUGUI ResourceCountTextField => resourceCountTextField;

        private Tween _shakeTween;
        public virtual void IncreaseAmount(int amount)
        {
            if (TotalAmount>0)
                Shake();
            TotalAmount += amount;
            UpdateResourceCountText();
        }

        private void Shake()
        {
            if (_shakeTween != null)
                _shakeTween.Kill();
            
            _shakeTween = shakeRoot.DOPunchScale(Vector3.one *0.2f,0.5f,2,0.1f).OnComplete(() =>
            {
                _shakeTween = null;
                shakeRoot.localScale = Vector3.one;
            }).OnKill(() =>
                {
                    _shakeTween = null;
                    shakeRoot.localScale = Vector3.one;
                });
        }

        public virtual void DecreaseAmount(int amount)
        {
            if (TotalAmount>0)
                Shake();
            TotalAmount -= amount;
            if (TotalAmount<0)
                TotalAmount = 0;

           
            
            UpdateResourceCountText();
        }
        
        public virtual void Build(ResourceData targetData)
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

        public virtual void UpdateResourceCountText()
        {
            ResourceCountTextField.text = TotalAmount.ToString();
        }

    }
}