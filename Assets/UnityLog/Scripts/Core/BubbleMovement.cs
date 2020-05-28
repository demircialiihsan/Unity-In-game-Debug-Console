using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityLog.Core
{
    public class BubbleMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private RectTransform canvas;

        private Vector2 closedPosition;

        public bool IsDragging { get; private set; }
        public bool IsMoving { get; private set; }

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = transform.parent.GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsMoving)
                return;

            if (LogManager.instance.IsOpen)
                LogManager.instance.Toggle(false);
            IsDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;
            SnapNearest();
        }

        void Update()
        {
            if (IsDragging)
            {
                transform.position = Vector3.Lerp(transform.position, Input.mousePosition, Time.deltaTime * LogManager.bubbleDragSpeed);
            }
        }

        public void SnapTopRight()
        {
            closedPosition = rectTransform.anchoredPosition;

            float left = rectTransform.anchorMin.x * canvas.rect.width + rectTransform.offsetMin.x;
            float up = (1 - rectTransform.anchorMax.y) * canvas.rect.height - rectTransform.offsetMax.y;
            Vector2 target = rectTransform.anchoredPosition + Vector2.up * up + Vector2.left * left;

            StartCoroutine(MoveAnchoredPosition(target, LogManager.bubbleSnapDuration));
        }

        private void SnapNearest()
        {
            if (IsMoving)
                return;

            Vector2 target = rectTransform.anchoredPosition;
            float shortest = Mathf.Infinity;

            float left = rectTransform.anchorMin.x * canvas.rect.width + rectTransform.offsetMin.x;
            float right = (1 - rectTransform.anchorMax.x) * canvas.rect.width - rectTransform.offsetMax.x;
            float up = (1 - rectTransform.anchorMax.y) * canvas.rect.height - rectTransform.offsetMax.y;
            float down = rectTransform.anchorMin.y * canvas.rect.height + rectTransform.offsetMin.y;

            if (IsOutOfBounds(left, right, up, down, out Vector2 nearestCorner))
            {
                target = nearestCorner;
            }
            else
            {
                if (left < shortest)
                {
                    shortest = left;
                    target = rectTransform.anchoredPosition + Vector2.left * left;
                }
                if (right < shortest)
                {
                    shortest = right;
                    target = rectTransform.anchoredPosition + Vector2.right * right;
                }
                if (up < shortest)
                {
                    shortest = up;
                    target = rectTransform.anchoredPosition + Vector2.up * up;
                }
                if (down < shortest)
                {
                    shortest = down;
                    target = rectTransform.anchoredPosition + Vector2.down * down;
                }
            }

            StartCoroutine(MoveAnchoredPosition(target, LogManager.bubbleSnapDuration));
        }

        private bool IsOutOfBounds(float left, float right, float up, float down, out Vector2 nearestCorner)
        {
            nearestCorner = rectTransform.anchoredPosition;

            if (left > 0 && right > 0 && up > 0 && down > 0)
                return false;

            if (left < 0)
                nearestCorner += Vector2.left * left;
            if (right < 0)
                nearestCorner += Vector2.right * right;
            if (up < 0)
                nearestCorner += Vector2.up * up;
            if (down < 0)
                nearestCorner += Vector2.down * down;

            return true;
        }

        void OnRectTransformDimensionsChange()
        {
            if (!IsMoving)
            {
                Invoke("SnapNearest", LogManager.bubbleSnapDuration);
            }

            closedPosition = Vector2.zero; //reset the go-back position to prevent from going off the screen
        }

        public void SnapBack()
        {
            StartCoroutine(MoveAnchoredPosition(closedPosition, LogManager.bubbleSnapDuration));
        }

        IEnumerator MoveAnchoredPosition(Vector2 to, float duration)
        {
            IsMoving = true;
            Vector2 from = rectTransform.anchoredPosition;
            float percent = 0;

            while (percent < 1)
            {
                percent += Time.unscaledDeltaTime / duration;
                rectTransform.anchoredPosition = Vector2.Lerp(from, to, percent);
                yield return null;
            }
            IsMoving = false;
        }
    }
}