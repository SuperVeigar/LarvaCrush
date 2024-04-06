using UnityEngine;
using DG.Tweening;

namespace SuperVeigar
{
    public abstract class Item : MonoBehaviour
    {
        private const float MOVE_TARGET_POSITION_Y = -15f;
        private const float MOVE_DISTANCE = 25f;
        private const float MOVE_DURATION = 7f;
        [SerializeField] private ItemType type;

        protected abstract void TakeItem();

        public void Init(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);

            StartMove();
        }

        private void StartMove()
        {
            transform.DOMoveY(MOVE_TARGET_POSITION_Y, GetDuration()).OnComplete(()=>
            {
                SetInactive();
            });
        }

        private float GetDuration()
        {
            return MOVE_DURATION * (transform.position.y - MOVE_TARGET_POSITION_Y) / MOVE_DISTANCE;
        }

        private void SetInactive()
        {
            DOTween.Kill(transform);
            gameObject.SetActive(false);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Define.Const.TAB_PLAYER)
            {
                TakeItem();
                SetInactive();
            }
        }
    }
}

