using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace ResourceSystem
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance;
        [SerializeField] private List<ResourceData> allResourceDataList;
        [SerializeField] private ResourceCardBase resourceCardPrefab;
        [SerializeField] private Transform resourceCardSpawnRoot;
        public Dictionary<GameResourceTypes, ResourceData> ResourceDict { get; private set; } =
            new Dictionary<GameResourceTypes, ResourceData>();
        
        public Dictionary<GameResourceTypes, ResourceCardBase> SpawnedResourceCardDict { get; private set; } =
            new Dictionary<GameResourceTypes, ResourceCardBase>();

        private void Awake()
        {
            Instance = this;
            Init();
        }

        private void Init()
        {
            var resourceTypes =Enum.GetNames(typeof(GameResourceTypes));
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                var targetType = (GameResourceTypes)i;
                var resource = allResourceDataList.Find(x => x.ResourceType == targetType);
                ResourceDict.Add(targetType,resource);
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.A))
            {
                IncreaseResource(allResourceDataList.RandomItem(),11);
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                DecreaseResource(allResourceDataList.RandomItem(),10);
            }
#endif
        }

        private void IncreaseResource(ResourceData targetData, int amount)
        {
            IncreaseResource(targetData.ResourceType,amount);
        }
        
        private void DecreaseResource(ResourceData targetData, int amount)
        {
            DecreaseResource(targetData.ResourceType,amount);
        }

        public void IncreaseResource(GameResourceTypes targetType, int amount)
        {
            var resourceData = GetResourceData(targetType);
            ResourceCardBase card = null;
            if (SpawnedResourceCardDict.ContainsKey(targetType))
            {
                card = GetResourceCard(targetType);
            }
            else
            {
                card = Instantiate(resourceCardPrefab,resourceCardSpawnRoot);
                card.Build(resourceData);
                SpawnedResourceCardDict.Add(targetType,card);
            }

            if (!card) return;
           
            card.IncreaseAmount(amount);
        }

        public void DecreaseResource(GameResourceTypes targetType, int amount)
        {
            var resourceData = GetResourceData(targetType);
            ResourceCardBase card = null;
            if (SpawnedResourceCardDict.ContainsKey(targetType))
            {
                card = GetResourceCard(targetType);
            }
            if (!card) return;
           
            card.DecreaseAmount(amount);
            if (card.TotalAmount<=0)
            {
                SpawnedResourceCardDict.Remove(targetType);
                Destroy(card.gameObject);
            }
        }

        public ResourceData GetResourceData(GameResourceTypes targetType)
        {
            return ResourceDict[targetType];
        }
        
        public ResourceCardBase GetResourceCard(GameResourceTypes targetType)
        {
            if (!SpawnedResourceCardDict.ContainsKey(targetType))
            {
                return null;
            }
            return SpawnedResourceCardDict[targetType];
        }

        public int GetResourceValue(GameResourceTypes targetType)
        {
            var resource = GetResourceCard(targetType);
            if (!resource)
                return 0;
            return resource.TotalAmount;
        }

    }
}
