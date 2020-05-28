using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityLog.Core
{
    public class Bubble : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public GameObject messageCountCircle;
        public Text messageCountText;
        private int messageCount;

        private BubbleMovement movement;

        void Awake()
        {
            movement = GetComponent<BubbleMovement>();
            if (movement == null)
                movement = gameObject.AddComponent<BubbleMovement>();
        }

        void Start()
        {
            ClearMessageCountCircle();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * LogManager.bubbleScaleOnPress;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
            if (!movement.IsDragging && !movement.IsMoving)
            {
                LogManager.instance.Toggle(true);
            }
        }

        public void SnapTopRight()
        {
            movement.SnapTopRight();
        }

        public void SnapBack()
        {
            movement.SnapBack();
        }

        public void UpdateMessageCount()
        {
            messageCountCircle.SetActive(true);
            messageCount = Mathf.Min(messageCount + 1, 99);
            messageCountText.text = messageCount.ToString();
        }

        public void ClearMessageCountCircle()
        {
            messageCountCircle.SetActive(false);
            messageCount = 0;
        }
    }
}
