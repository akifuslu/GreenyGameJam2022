using Mono.Cecil;
using UnityEngine;

namespace ResourceSystem
{
    [CreateAssetMenu(fileName = "Resource Data", menuName = "Data/Resource", order = 0)]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] private GameResourceTypes resourceType;
        [SerializeField] private string resourceName;
        [SerializeField] private Sprite resourceSprite;
        [SerializeField] private Color backgroundColor;
        [SerializeField] private bool excludeStock;
        [SerializeField] private int stockGoalValue;
        
        public string ResourceName => resourceName;

        public Sprite ResourceSprite => resourceSprite;

        public GameResourceTypes ResourceType => resourceType;

        public Color BackgroundColor => backgroundColor;

        public bool ExcludeStock => excludeStock;

        public int StockGoalValue => stockGoalValue;
    }
}