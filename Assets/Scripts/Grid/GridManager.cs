using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        private Tile _tilePrefab;
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

        private List<Tile> _tiles;


        public void HighlightNei(int index)
        {
            _tiles.ForEach(t => t.Clear());
            var x = index % _countX;
            var y = index / _countX;
            var isEven = y % 2 == 0;
            var fwd = index + 2 * _countX;
            var left = index + _countX - 1 + y % 2;
            var right = index + _countX + y % 2;
            if (x == 0 && isEven) left = -1;
            if (x == _countX - 1 && !isEven) right = -1;

            if(y == _countY - 1)
            {
                left = -1;
                right = -1;
            }

            if (fwd < _tiles.Count) _tiles[fwd].CanMove();
            if (left != -1) _tiles[left].CanMove();
            if (right != -1) _tiles[right].CanMove();
        }

#if UNITY_EDITOR
        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            _tiles = GetComponentsInChildren<Tile>(true).ToList();

            if (_tiles.Count < _totalTileCount)
            {
                while(_tiles.Count < _totalTileCount)
                {
                    var nt = PrefabUtility.InstantiatePrefab(_tilePrefab, transform) as Tile;
                    _tiles.Add(nt);
                }
            }
            else if (_tiles.Count > _totalTileCount)
            {
                while (_tiles.Count > _totalTileCount)
                {
                    var l = _tiles.Last();
                    _tiles.Remove(l);
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
                    _tiles[k].Init(lb, _tileRadius, _padding, k, this);
                    k++;
                    lb.x += 3 * _tileRadius;
                }
                lb.y += _tileRadius * Mathf.Sqrt(3) / 2;
            }
        }
    }
#endif

}
