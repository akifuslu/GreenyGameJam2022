using System;
using System.Collections.Generic;
using ChoiceSystem;
using Grid;
using UniRx;
using UnityEngine;
using Utility;

namespace ResourceSystem
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance;
        [SerializeField] private List<ResourceData> allResourceDataList;
        [SerializeField] private ResourceCardBase resourceCardPrefab;
        [SerializeField] private ResourceStockCardBase stockCardPrefab;
        [SerializeField] private Transform resourceCardSpawnRoot;
        [SerializeField] private Transform stockCardSpawnRoot;
        public Dictionary<GameResourceTypes, ResourceData> ResourceDict { get; private set; } =
            new Dictionary<GameResourceTypes, ResourceData>();
        
        public Dictionary<GameResourceTypes, ResourceCardBase> SpawnedResourceCardDict { get; private set; } =
            new Dictionary<GameResourceTypes, ResourceCardBase>();
        
        public Dictionary<GameResourceTypes, ResourceStockCardBase> SpawnedStockCardDict { get; private set; } =
            new Dictionary<GameResourceTypes, ResourceStockCardBase>();

        #region Setup
        private void Awake()
        {
            Instance = this;
            Init();
            MessageBus.OnEvent<EndTileReachedEvent>().Subscribe(ev =>
            {
                StockAllActiveResources();
            });
        }
        private void Init()
        {
            var resourceTypes =Enum.GetNames(typeof(GameResourceTypes));
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                var targetType = (GameResourceTypes)i;
                var resource = allResourceDataList.Find(x => x.ResourceType == targetType);
                ResourceDict.Add(targetType,resource);
                if (!resource.ExcludeStock)
                {
                    IncreaseStock(resource,0);
                    var stock =GetStockCard(resource.ResourceType);
                    stock.SetMaxAmount(resource.StockGoalValue);
                    DecreaseStock(resource,0);
                }
            }
            
        }
        #endregion

        #region Process
        
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
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                StockAllActiveResources();
            }
#endif
        }
        
         
        #endregion

        #region Resource
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
        #endregion

        #region Stock
        private void IncreaseStock(ResourceData targetData, int amount)
        {
            IncreaseStock(targetData.ResourceType,amount);
        }
        private void DecreaseStock(ResourceData targetData, int amount)
        {
            DecreaseStock(targetData.ResourceType,amount);
        }
        public void IncreaseStock(GameResourceTypes targetType, int amount)
        {
            var resourceData = GetResourceData(targetType);
            ResourceStockCardBase card = null;
            if (SpawnedStockCardDict.ContainsKey(targetType))
            {
                card = GetStockCard(targetType);
            }
            else
            {
                card = Instantiate(stockCardPrefab,stockCardSpawnRoot);
                card.Build(resourceData);
                SpawnedStockCardDict.Add(targetType,card);
            }
            
            if (!card) return;
           
            card.IncreaseAmount(amount);
        }
        public void DecreaseStock(GameResourceTypes targetType, int amount)
        {
            ResourceStockCardBase card = null;
            if (SpawnedStockCardDict.ContainsKey(targetType))
            {
                card = GetStockCard(targetType);
            }
            if (!card) return;
           
            card.DecreaseAmount(amount);
        }
        public ResourceStockCardBase GetStockCard(GameResourceTypes targetType)
        {
            if (!SpawnedStockCardDict.ContainsKey(targetType))
            {
                return null;
            }
            return SpawnedStockCardDict[targetType];
        }
        public int GetStockValue(GameResourceTypes targetType)
        {
            var resource = GetStockCard(targetType);
            if (!resource)
                return 0;
            return resource.TotalAmount;
        }
        #endregion

        #region Common

        public void StockAllActiveResources()
        {
            foreach (var resourceData in allResourceDataList)
            {
                if (resourceData.ExcludeStock) continue ;
                StockResource(resourceData.ResourceType);
            }
        }
        private void StockResource(GameResourceTypes targetType)
        {
            var targetResource = GetResourceCard(targetType);
            if (!targetResource) return;
            
            IncreaseStock(targetType,targetResource.TotalAmount);
            DecreaseResource(targetType,targetResource.TotalAmount);
           
        }
        public ResourceData GetResourceData(GameResourceTypes targetType)
        {
            return ResourceDict[targetType];
        }
        #endregion
       
        
    }
}
