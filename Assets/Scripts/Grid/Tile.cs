using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _outline;

        private List<Tile> _nei;


        public virtual void Init(List<Tile> nei)
        {
            _nei = nei;
        }

        public bool CanMoveTo(Tile target)
        {
            return _nei.Contains(target);
        }

        public virtual void OnEnter()
        {
            SetOutline(Color.yellow);
            _nei.ForEach(n => n.SetOutline(Color.green));
        }

        public void OnLeave()
        {
            SetOutline(Color.red);
            _nei.ForEach(n => n.SetOutline(Color.white));
        }

        public virtual void OnDayEnd()
        {
            SetOutline(Color.white);
        }

        public void SetOutline(Color color)
        {
            _outline.color = color;
        }
    }
}
