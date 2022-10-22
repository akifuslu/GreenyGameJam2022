using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Grid
{
    [ExecuteInEditMode]
    public class GridManager : MonoBehaviour
    {
        private int _totalTileCount => _countX * _countY;

        [SerializeField]
        private SerializedDictionary<TileType, Tile> _tileMapping;

        [SerializeField]
        private TileSlot _tileSlotPrefab;
        [SerializeField]
        private Vector2 _center;
        [SerializeField]
        private int _countX;
        [SerializeField]
        private int _countY;
        [SerializeField]
        private float _tileRadius;
        [SerializeField]
        private float _padding;

        private List<TileSlot> _tileSlots;
        private List<Tile> _tiles;


        public Tile GetStartingTile()
        {
            return _tileSlots.FirstOrDefault(t => t.Type == TileType.START).Tile;
        }

        public List<Tile> GetNei(int index)
        {
            var nei = new List<Tile>();
            var x = index % _countX;
            var y = index / _countX;
            var isEven = y % 2 == 0;
            var fwd = index + 2 * _countX;
            var left = index + _countX - 1 + y % 2;
            var right = index + _countX + y % 2;
            if (x == 0 && isEven) left = -1;
            if (x == _countX - 1 && !isEven) right = -1;

            if (y == _countY - 1)
            {
                left = -1;
                right = -1;
            }

            if (fwd < _tileSlots.Count) nei.Add(_tileSlots[fwd].Tile);
            if (left != -1) nei.Add(_tileSlots[left].Tile);
            if (right != -1) nei.Add(_tileSlots[right].Tile);

            nei.RemoveAll(t => t == null);
            return nei;
        }

        public void OnDayEnd()
        {
            _tiles.ForEach(t => t.OnDayEnd());
        }

        private void Awake()
        {
            if (Application.isPlaying)
            {
                BuildTiles();
            }
        }

        private void BuildTiles()
        {
            _tileSlots = GetComponentsInChildren<TileSlot>(true).ToList();
            _tiles = new List<Tile>();

            for (int i = 0; i < _tileSlots.Count; i++)
            {
                if(_tileSlots[i].Type == TileType.EMPTY)
                {
                    continue;
                }

                var t = Instantiate(_tileMapping[_tileSlots[i].Type], transform);
                t.transform.position = _tileSlots[i].transform.position;
                t.transform.localScale = Vector3.one * _tileRadius * 2 - Vector3.one * _padding;
                _tiles.Add(t);
                _tileSlots[i].Tile = t;
            }

            for (int i = 0; i < _tileSlots.Count; i++)
            {
                if (_tileSlots[i].Type == TileType.EMPTY)
                {
                    continue;
                }
                _tileSlots[i].Tile.Init(GetNei(i));
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            _tileSlots = GetComponentsInChildren<TileSlot>(true).ToList();

            if (_tileSlots.Count < _totalTileCount)
            {
                while(_tileSlots.Count < _totalTileCount)
                {
                    var nt = PrefabUtility.InstantiatePrefab(_tileSlotPrefab, transform) as TileSlot;
                    _tileSlots.Add(nt);
                }
            }
            else if (_tileSlots.Count > _totalTileCount)
            {
                while (_tileSlots.Count > _totalTileCount)
                {
                    var l = _tileSlots.Last();
                    _tileSlots.Remove(l);
                    DestroyImmediate(l.gameObject);
                }
            }


            Vector2 lb = _center + Vector2.left * _countX + Vector2.down * _countY / 2;

            var sx = lb.x;
            int k = 0;
            for(int i = 0; i < _countY; i++)
            {
                lb.x = sx + (i % 2) * _tileRadius * 1.5f;
                for (int j = 0; j < _countX; j++)
                {
                    _tileSlots[k].Init(lb, _tileRadius, _padding);
                    k++;
                    lb.x += 3 * _tileRadius;
                }
                lb.y += _tileRadius * Mathf.Sqrt(3) / 2;
            }
        }
    }
#endif

}
