using TMPro;
using UnityEngine;

namespace ChoiceSystem.CanvasScripts
{
    public class SpecialChoiceCanvas : ChoiceCanvasBase
    {
        [SerializeField] private TextMeshProUGUI mainEventTextField;

        public void SetMainEventText(string mainEventText)
        {
            mainEventTextField.text = mainEventText;
        }
        
    }
}