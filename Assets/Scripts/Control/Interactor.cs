using System.Collections;
using System.Collections.Generic;
using Grid;
using Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Control
{
    public class Interactor : MonoBehaviour
    {

        private PlayerAvatar _avatar;

        private void Awake()
        {
            _avatar = FindObjectOfType<PlayerAvatar>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                OnClick();
            }
        }

        private void OnClick()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if(hit.collider.GetComponent<Tile>() is Tile tile)
                {
                    _avatar.MoveTo(tile);
                }
            }
        }
    }
}
