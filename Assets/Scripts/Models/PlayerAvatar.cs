using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using Utility;
using UniRx;

namespace Models
{
    public class PlayerAvatar : MonoBehaviour
    {

        private GridManager _grid;
        private Tile _current;
        private Tile _start;

        private void Awake()
        {
            _grid = FindObjectOfType<GridManager>();
        }

        private void Start()
        {
            _start = _current = _grid.GetStartingTile();
            OnNewTile();

            MessageBus.OnEvent<EndTileReachedEvent>().Subscribe(ev =>
            {
                OnEndTile();
            });
        }

        private void OnNewTile()
        {
            transform.position = _current.transform.position;
            _current.OnEnter();

            //if(_current.Type == TileType.END)
            //{
            //    OnEndTile();
            //}
        }

        private void OnEndTile()
        {
            _grid.OnDayEnd();
            _current = _start;
            OnNewTile();
        }

        public void MoveTo(Tile target)
        {
            if (!_current.CanMoveTo(target))
                return;

            _current.OnLeave();
            _current = target;

            OnNewTile();
        }

    }
}
