using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Control
{
    public class Interactor : MonoBehaviour
    {

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
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
                    tile.Highlight();
                }
            }
        }
    }
}
