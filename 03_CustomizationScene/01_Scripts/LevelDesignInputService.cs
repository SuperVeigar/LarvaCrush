using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperVeigar
{
    public class LevelDesignInputService : SingletonBehaviour<LevelDesignInputService>
    {
        private const float IGNORING_ROW_POSITION_Y = 9f;
        private bool isDraggingLeft;
        private bool isDraggingRight;

        private void Update()
        {
            if (PopupService.Instance.IsPopupNow() == false)
            {
                CheckClick();
            }
        }

        private void CheckClick()
        {
            // 좌클릭
            if (Input.GetMouseButtonDown(0) == true)
            {
                isDraggingLeft = true;

                EventOnLeftClick();
            }
            if (Input.GetMouseButtonUp(0) == true)
            {
                isDraggingLeft = false;
            }
            if (isDraggingLeft == true)
            {
                EventOnLeftClick();
            }

            // 우클릭
            if (Input.GetMouseButtonDown(1) == true)
            {
                isDraggingRight = true;

                EventOnRightClick();
            }
            if (Input.GetMouseButtonUp(1) == true)
            {
                isDraggingRight = false;
            }
            if (isDraggingRight == true)
            {
                EventOnRightClick();
            }
        }

        private void EventOnLeftClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                BrickBG brick = hit.collider.gameObject.GetComponent<BrickBG>();

                if (brick != null && IsAvailableButton(brick) == true)
                {
                    brick.EventOnLeftClick();
                }
            }
        }

        private void EventOnRightClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                BrickBG brick = hit.collider.gameObject.GetComponent<BrickBG>();

                if (brick != null && IsAvailableButton(brick) == true)
                {
                    brick.EventOnRightClick();
                }
            }
        }

        private bool IsAvailableButton(BrickBG brick)
        {
            return brick.transform.position.y < IGNORING_ROW_POSITION_Y;
        }
    }
}
