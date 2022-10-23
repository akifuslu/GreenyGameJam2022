using System;
using Audio;
using ResourceSystem;
using TMPro;
using UniRx;
using UnityEngine;
using Utility;

namespace EndGame
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] private EndGamePanelBase winPanel;
        [SerializeField] private EndGamePanelBase losePanel;
       

        private void Awake()
        {
            MessageBus.OnEvent<EndGameEvent>().Take(1).Subscribe(ev =>
            {
                CheckEndGame();
            });
            
            winPanel.Close();
            losePanel.Close();
        }


        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.V))
            {
                Win();
            }
            
            if (Input.GetKeyDown(KeyCode.B))
            {
                Lose();
            }
#endif
        }

        public void CheckEndGame()
        {
            if (TryWin())
            {
                Win();
            }
            else
            {
                Lose();
            }
        }

        private bool TryWin()
        {
            var stockDict = ResourceManager.Instance.SpawnedStockCardDict;
            foreach (var stock in stockDict)
            {
                if (stock.Value.TotalAmount<stock.Value.MyData.StockGoalValue)
                    return false;
            }

            return true;
        }

        private bool _isFinished;
        public void Win()
        {
            if (_isFinished) return;
            _isFinished = true;
            
            PlaySfx.PlayWin();
            winPanel.Open(ConvertStockToScore());
        }

        public void Lose()
        {
            if (_isFinished) return;
            _isFinished = true;
            PlaySfx.PlayLose();
            losePanel.Open();
        }


        private int ConvertStockToScore()
        {
            var score = 0;
            foreach (var stockCardBase in ResourceManager.Instance.SpawnedStockCardDict)
            {
                score += stockCardBase.Value.TotalAmount * stockCardBase.Value.MyData.CollectValue;
            }

            return score;
        }

    }
    
    public class EndGameEvent : GameEvent {}
}