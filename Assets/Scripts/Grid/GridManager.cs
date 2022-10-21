using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            _tiles = GetComponentsInChildren<Tile>().ToList();

            if (_tiles.Count < _totalTileCount)
            {
                while(_tiles.Count < _totalTileCount)
                {
                    var nt = Instantiate(_tilePrefab, transform);
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
                    _tiles[k++].Init(lb, _tileRadius, _padding);
                    lb.x += 3 * _tileRadius;
                }
                lb.y += _tileRadius * Mathf.Sqrt(3) / 2;
            }
        }
    }
}
