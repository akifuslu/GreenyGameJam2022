using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Grid
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _outline;

        private List<Tile> _nei;

        private Vector3 _sca;

        public virtual void Init(List<Tile> nei)
        {
            _nei = nei;
            _sca = transform.localScale;
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

        public void Show(float delay, float duration)
        {
            transform.DOKill(true);
            transform.DOScale(_sca, duration).SetDelay(delay).SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            transform.DOKill(true);
            transform.localScale = Vector3.zero;
        }
    }
}
