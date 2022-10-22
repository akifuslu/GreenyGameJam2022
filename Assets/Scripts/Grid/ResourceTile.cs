using System.Collections.Generic;
using UnityEngine;
using ResourceSystem;
using ChoiceSystem;
using UniRx;
using TMPro;

namespace Grid
{
    public class ResourceTile : Tile
    {
        public GameResourceTypes Resource;
        public int MaxResourceAmount;
        public int MaxReplenish;
        public List<ChoiceDataBase> Choices;

        [Header("VISUAL")]
        [SerializeField]
        private TextMeshPro _resAmountView;
        [SerializeField]
        private TextMeshPro _replenishView;

        private IntReactiveProperty _resourceAmount;
        private IntReactiveProperty _replenish;

        private bool _visited;

        private List<ChoiceDataBase> _choices;

        public override void Init(List<Tile> nei)
        {
            base.Init(nei);

            _resourceAmount = new IntReactiveProperty(MaxResourceAmount);
            _replenish = new IntReactiveProperty(MaxReplenish);

            _resourceAmount.Subscribe(ev =>
            {
                _resAmountView.text = ev.ToString();
            });

            _replenish.Subscribe(ev =>
            {
                _replenishView.text = (ev > 0 ? "+" : "") + ev.ToString();
            });

            _choices = new List<ChoiceDataBase>();

            foreach (var choice in Choices)
            {
                _choices.Add(choice.GetCopy());
            }

            foreach (var choice in _choices)
            {
                choice.ChoiceActionDatas.ForEach(ca => ca.Tile = this);
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _visited = true;

            ChoiceManager.Instance.OpenChoiceCanvas(_choices);
        }

        public void ReduceReplenish(int amount)
        {
            _replenish.Value -= amount;
        }

        public void ReduceResource(int amount)
        {
            _resourceAmount.Value -= amount;
        }

        public override void OnDayEnd()
        {
            if(!_visited)
            {
                _replenish.Value += 1;
            }
            if (_replenish.Value > MaxReplenish) _replenish.Value = MaxReplenish;


            _resourceAmount.Value += _replenish.Value;

            _resourceAmount.Value = Mathf.Clamp(_resourceAmount.Value, 0, MaxResourceAmount);

            _visited = false;
        }
    }
}
