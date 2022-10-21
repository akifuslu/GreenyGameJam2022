using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Grid
{

    public enum TileType
    {
        DEFAULT,
        EMPTY,
        START,
        END,
        FOREST,
        COALMINE,
        STONEMINE,
        WATER,
        FARM,
        DESERT
    }

    [ExecuteAlways]
    public class Tile : MonoBehaviour
    {

        public TileType Type;
        private float _radius;
        private GridManager _grid;
        private SpriteRenderer _outline;

        private List<Tile> _nei;


        private void Awake()
        {
            _outline = GetComponent<SpriteRenderer>();
            _grid = GetComponentInParent<GridManager>();
        }

        public bool CanMoveTo(Tile target)
        {
            return _nei.Contains(target);
        }

        public void OnEnter()
        {
            SetOutline(Color.yellow);
            _nei = _grid.GetNei(this);
            _nei.ForEach(n => n.SetOutline(Color.green));
        }

        public void OnLeave()
        {
            SetOutline(Color.red);
            _nei.ForEach(n => n.SetOutline(Color.white));
        }


        public void SetOutline(Color color)
        {
            _outline.color = color;
        }

#if UNITY_EDITOR
        public void Init(Vector2 pos, float radius, float padding, GridManager grid)
        {
            transform.position = pos;
            transform.localScale = Vector3.one * radius * 2 - Vector3.one * padding;
            _radius = radius;
            _grid = grid;
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            gameObject.SetActive(Type != TileType.EMPTY);
            GetComponentInChildren<TextMeshPro>().text = Type.ToString();
        }
#endif

        private void OnDrawGizmosSelected()
        {
            GizmoHexa(transform.position, _radius);
        }

        private void GizmoHexa(Vector2 center, float radius)
        {
            var rot = Quaternion.AngleAxis(60, Vector3.forward);
            var dir = Vector2.left * radius;
            var v1 = center + dir;
            dir = rot * dir;
            var v2 = center + dir;
            for (int i = 0; i < 6; i++)
            {
                Gizmos.DrawLine(v1, v2);
                v1 = v2;
                dir = rot * dir;
                v2 = center + dir;
            }
        }

        public void CanMove()
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }

        public void Clear()
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
