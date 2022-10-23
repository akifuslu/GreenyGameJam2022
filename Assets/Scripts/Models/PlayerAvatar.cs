using System.Collections;
using System.Collections.Generic;
using Audio;
using Grid;
using UnityEngine;
using Utility;
using UniRx;
using Views;
using DG.Tweening;

namespace Models
{
    public class PlayerAvatar : MonoBehaviour
    {

        private GridManager _grid;
        private Tile _current;
        private Tile _start;

        private Vector3 _sca;

        private void Awake()
        {
            _sca = transform.localScale;
            _grid = FindObjectOfType<GridManager>();
        }

        private void Start()
        {
            _start = _current = _grid.GetStartingTile();
            OnNewTile();

            MessageBus.OnEvent<DayEndedEvent>().Subscribe(ev =>
            {
                transform.localScale = Vector3.zero;
            });

            MessageBus.OnEvent<DayStartedEvent>().Subscribe(ev =>
            {
                transform.DOScale(_sca, .25f).SetEase(Ease.OutBack);
                _current = _start;
                OnNewTile();
            });
        }

        private void OnNewTile()
        {
            transform.position = _current.transform.position;
            _current.OnEnter();
        }

        public void MoveTo(Tile target)
        {
            if (!_current.CanMoveTo(target))
                return;

            _current.OnLeave();
            _current = target;

            OnNewTile();
            PlaySfx.PlayMove();
        }

    }
}
