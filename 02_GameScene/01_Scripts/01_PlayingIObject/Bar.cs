using UnityEngine;

namespace SuperVeigar
{
    public class Bar : IngameObject
    {
        private const float LIMIT_LEFT = -5f;
        private const float LIMIT_RIGHT = 5f;
        private Vector3 startPosition;
        private bool isDragging = false;
        private Vector2 offset;

        private void Start()
        {
            startPosition = transform.position;

            Reset();
        }

        public override void Reset()
        {
            transform.position = startPosition;
        }

        public override void OnReadyState()
        {

        }

        public override void OnPlayState()
        {
            Drag();
        }

        private void Drag()
        {
            // 마우스 버튼이 클릭되었을 때
            if (Input.GetMouseButtonDown(0))
            {
                // Ray를 생성하여 마우스 위치에서 화면으로 쏴서 충돌한 위치의 객체를 찾음
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // 충돌한 객체가 있고, 그 객체가 현재 스크립트가 적용된 객체일 경우 드래그 시작
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }

            // 마우스 버튼이 떼어졌을 때
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            // 드래그 중일 때
            if (isDragging)
            {
                // 마우스 위치로 객체 이동
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float positionX = Mathf.Clamp(mousePos.x + offset.x, LIMIT_LEFT, LIMIT_RIGHT);                
                transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
            }
        }
    }
}

