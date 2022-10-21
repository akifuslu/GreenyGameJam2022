using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class Tile : MonoBehaviour
    {

        private float _radius;
        private int _index;
        private GridManager _grid;

        public void Init(Vector2 pos, float radius, float padding, int index, GridManager grid)
        {
            transform.position = pos;
            transform.localScale = Vector3.one * radius * 2 - Vector3.one * padding; 
            _radius = radius;
            _index = index;
            _grid = grid;
        }


        public void Highlight()
        {
            _grid.HighlightNei(_index);
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }

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
