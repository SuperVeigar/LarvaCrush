using UnityEngine;
using System;

namespace SuperVeigar
{
    public class Arrow : IngameObject
    {
        [SerializeField] private GameObject arrowRenderer;
        [SerializeField] private Ball ball;
        private bool isDragging;
        private Vector3 touchPosition = Vector3.zero;

        public override void Reset()
        {
            isDragging = false;
            touchPosition = Vector3.zero;
            ResetArrow();
        }

        public override void OnReadyState()
        {
            DragFirePosition(() =>
            {
                RotateArrow();
            });
        }

        public override void OnPlayState()
        {
            
        }

        private void ResetArrow()
        {
            RotateArrow();
            arrowRenderer.SetActive(true);
        }

        private void RotateArrow()
        {
            Vector2 direction = (Vector2)touchPosition - (Vector2)transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            transform.eulerAngles = new Vector3(0f, 0f, angle);

            ball.Rotate(direction);
        }

        private void DragFirePosition(Action OnDrag)
        {
            if (isDragging == false)
            {
                if (Input.GetMouseButtonDown(0) == true)
                {
                    isDragging = true;
                }
            }
            else
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPosition.z = 0;

                OnDrag?.Invoke();

                if (Input.GetMouseButtonUp(0) == true)
                {
                    isDragging = false;

                    Fire();
                }
            }
        }

        private void Fire()
        {
            GameService.Instance.SetGameState(GameState.Play);

            arrowRenderer.SetActive(false);

            ball.Fire(transform.position, (touchPosition - transform.position).normalized);
        }
    }
}