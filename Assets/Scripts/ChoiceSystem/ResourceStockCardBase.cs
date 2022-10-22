using System.Text;
using ResourceSystem;
using UnityEngine;
using Utility;

namespace ChoiceSystem
{
    public class ResourceStockCardBase : ResourceCardBase
    {
        [SerializeField] private Color maxColor = Color.green;
        
        public int MaxAmount { get; private set; }

        private bool _isReachedMax;

        public void SetMaxAmount(int targetValue)
        {
            MaxAmount = targetValue;
            UpdateResourceCountText();
        }
        
        public override void IncreaseAmount(int amount)
        {
            base.IncreaseAmount(amount);
            if (TotalAmount>=MaxAmount)
            {
                _isReachedMax = true;
                UpdateResourceCountText();
            }
            else
                _isReachedMax = false;
        }

        public override void DecreaseAmount(int amount)
        {
            base.DecreaseAmount(amount);
            if (TotalAmount<MaxAmount && _isReachedMax)
            {
                _isReachedMax = false;
                UpdateResourceCountText();
            }
        }
        
        public override void UpdateResourceCountText()
        {
            var str = new StringBuilder();
            str.Append(TotalAmount).Append("/").Append(MaxAmount);
            if (_isReachedMax)
                str = ColorExtentions.ColorStringBuilder(str.ToString(),maxColor);
            
            ResourceCountTextField.text = str.ToString();
        }
    }
}