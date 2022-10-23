using System.Collections.Generic;
using UnityEngine;
using ResourceSystem;
using ChoiceSystem;
using UniRx;
using TMPro;
using System.Linq;
using EncounterSystem;

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
        [SerializeField]
        private SpriteRenderer _icon;
        [SerializeField]
        private List<Sprite> _iconSet;

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
                _resAmountView.text = "stok: " + ev.ToString() + "<sprite=" + (int)Resource+ ">";

                var ratio = (float)ev / MaxResourceAmount;
                if (_iconSet.Count == 0)
                    return;

                if (ev > .66f)
                    _icon.sprite = _iconSet[2];
                else if (ev > .33f)
                    _icon.sprite = _iconSet[1];
                else
                    _icon.sprite = _iconSet[0];

            });

            _replenish.Subscribe(ev =>
            {
                _replenishView.text = "yenilenme +" + ev.ToString();
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

            ChoiceManager.Instance.OpenChoiceCanvas(_choices, "", () =>
            {
                var r = Random.value;
                if(r > .6f)
                {
                    OnEncounter();
                }
            });
        }

        private void OnEncounter()
        {
            EncounterManager.Instance.TriggerAvailRandomEncounter();
        }

        public void ReduceReplenish(int amount)
        {
            _replenish.Value -= amount;
            _replenish.Value = Mathf.Clamp(_replenish.Value, 0, MaxReplenish);
        }

        public void ReduceResource(int amount)
        {
            _resourceAmount.Value -= amount;
            RefreshResourceActions();
        }

        public override void OnDayEnd()
        {
            base.OnDayEnd();

            if(!_visited)
            {
                _replenish.Value += 1;
            }
            if (_replenish.Value > MaxReplenish) _replenish.Value = MaxReplenish;


            _resourceAmount.Value += _replenish.Value;

            _resourceAmount.Value = Mathf.Clamp(_resourceAmount.Value, 0, MaxResourceAmount);
            RefreshResourceActions();
            _visited = false;
        }

        private void RefreshResourceActions()
        {
            foreach (var choice in _choices)
            {
                var gr = choice.ChoiceActionDatas.Where(a => a.ActionType == ChoiceActionTypes.GatherResource).ToList();
                gr.ForEach(s => s.Value = Mathf.Min(s.BaseValue, _resourceAmount.Value));
            }
        }
    }
}
