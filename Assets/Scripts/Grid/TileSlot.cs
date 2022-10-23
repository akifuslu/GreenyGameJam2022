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
        DESERT,
        TRADE
    }

    [ExecuteInEditMode]
    [SelectionBase]
    public class TileSlot : MonoBehaviour
    {

        public TileType Type;
        [HideInInspector]
        public Tile Tile;

        private float _radius;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }

#if UNITY_EDITOR
        public void Init(Vector2 pos, float radius, float padding)
        {
            transform.position = pos;
            transform.localScale = Vector3.one * radius * 2 - Vector3.one * padding;
            _radius = radius;
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            GetComponent<SpriteRenderer>().enabled = Type != TileType.EMPTY;
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
    }
}
