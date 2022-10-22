using TMPro;
using UnityEngine;

namespace ChoiceSystem.CanvasScripts
{
    public class ChoiceCanvasBase : MonoBehaviour
    {
        [SerializeField] private ChoiceCardBase choiceCardPrefab;
        [SerializeField] private Transform choiceSpawnRoot;
        [SerializeField] private TextMeshProUGUI titleTextField;

        public ChoiceCardBase ChoiceCardPrefab => choiceCardPrefab;

        public Transform ChoiceSpawnRoot => choiceSpawnRoot;

        public TextMeshProUGUI TitleTextField => titleTextField;

        public virtual void Build()
        {
            
        }
    }
}