using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace SuperVeigar
{
    public class Ball : IngameObject
    {
        private const float MOVE_SPEED = 9;
        private Vector2 startPosition;
        private Vector2 direction;
        private Vector2 previousMoveStartPosition;
        private Rigidbody2D rigidbodyBall;

        // Tail
        private Tail tail;
        private int currentOrderInLayer = 0;

        private void Start()
        {
            startPosition = transform.position;
            rigidbodyBall = GetComponent<Rigidbody2D>();
            
            AddEvent();
            Reset();
        }

        private void OnDestroy()
        {
            RemoveEvent();
        }

        private void AddEvent()
        {
            GameEventService.OnAddTail += HandleOnAddTail;
        }

        private void RemoveEvent()
        {
            GameEventService.OnAddTail -= HandleOnAddTail;
        }

        public override void Reset()
        {
            transform.position = startPosition;
            currentOrderInLayer = 0;
        }

        public override void OnReadyState()
        {
            
        }

        public override void OnPlayState()
        {
            
        }

        public override void OnPlayFixedState()
        {
            tail?.UpdateTail(transform.position);
        }

#region Fire

        public void Fire(Vector2 previousMoveStartPosition, Vector2 direction)
        {
            SoundService.Instance.PlaySound(SoundReferenceType.BALL_START);

            this.direction = direction;
            this.previousMoveStartPosition = previousMoveStartPosition;

            rigidbodyBall.velocity = direction * MOVE_SPEED;

            Rotate();
        }

        public void Rotate(Vector2 direction)
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }

        private void Rotate()
        {
            float angle = Vector2.SignedAngle(Vector2.up, rigidbodyBall.velocity);
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }

#endregion

#region Play state

        private void OnCollisionEnter2D(Collision2D coll)
        {
            BreakBrick(coll);

            BoundBoundable(coll).Forget();
        }

        private void BreakBrick(Collision2D coll)
        {
            if (coll.gameObject.tag == Define.Const.TAG_BRICK)
            {
                BrickBG brick = coll.gameObject.GetComponent<BrickBG>();
                brick.Touch();

                tail?.AddAction(() => brick.Touch());
            }
        }

        private async UniTask BoundBoundable(Collision2D coll)
        {
            if (coll.gameObject.layer == Define.Const.LAYER_BOUNDABLE)
            {
                if (coll.gameObject.tag == Define.Const.TAG_BRICK)
                {
                    SoundService.Instance.PlaySound(SoundReferenceType.BRICK_BOUND);
                }
                else
                {
                    SoundService.Instance.PlaySound(SoundReferenceType.BOUND);
                }

                await UniTask.DelayFrame(1);

                Rotate();
            }
        }

#endregion

#region Tail

        public void HandleOnAddTail()
        {
            if (tail == null)
            {
                tail = ObjectPoolService.Instance.GetTail();
                tail.Init(transform.position, --currentOrderInLayer);
            }
            else
            {
                tail.AddTail();
            }
        }

#endregion
    }
}
