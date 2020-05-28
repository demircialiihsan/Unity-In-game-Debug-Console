using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace UnityLog.Core
{
    public class LogManager : MonoBehaviour
    {
        public const float animationDuration = 0.15f;
        public const float bubbleSnapDuration = 0.15f;
        public const float bubbleScaleOnPress = 0.9f;
        public const float bubbleDragSpeed = 20f;
        private const float bubbleSizePercent = 0.1f; //percentage of the bubble size with respect to longer dimention(width/height) of the screen

        public Text logText;
        public Bubble bubble;
        public PopUp logPanel;
        public PopUp clearButton;

        public bool IsOpen { get; private set; }

        public static LogManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (instance == this)
            {
                Log.Init(logText, bubble);
                SceneManager.sceneLoaded += CheckEventSystem;
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }

        void CheckEventSystem(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (EventSystem.current == null)
                new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        public void SetProperties(bool dontDestroyOnLoad, int fontSize, Color fontColor, Color backgroundColor)
        {
            logText.fontSize = fontSize;
            logText.color = fontColor;
            logPanel.GetComponent<Graphic>().color = backgroundColor;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(transform.root.gameObject);
        }

        public void Toggle(bool overrideBubble)
        {
            if (IsOpen)
            {
                IsOpen = false;
                logPanel.Close();
                clearButton.Close();

                if (overrideBubble)
                    bubble.SnapBack();
            }
            else
            {
                IsOpen = true;
                logPanel.Open();
                clearButton.Open();
                bubble.ClearMessageCountCircle();

                if (overrideBubble)
                    bubble.SnapTopRight();
            }
        }

        public void Clear()
        {
            logText.text = string.Empty;
        }

        void OnRectTransformDimensionsChange()
        {
            UpdateLayout();
        }

        void UpdateLayout()
        {
            RectTransform canvas = GetComponent<RectTransform>();
            RectTransform bubbleRect = bubble.GetComponent<RectTransform>();
            RectTransform logPanelRect = logPanel.GetComponent<RectTransform>();
            RectTransform clearButtonRect = clearButton.GetComponent<RectTransform>();

            float screenWidth = canvas.rect.width;
            float screenHeight = canvas.rect.height;

            //set anchors for bubble to take square area
            if (screenWidth >= screenHeight) //calculate the size based on screen width
            {
                bubbleRect.anchorMin = new Vector2(0, 1 - (screenWidth / screenHeight) * bubbleSizePercent);
                bubbleRect.anchorMax = new Vector2(bubbleSizePercent, 1);
            }
            else //calculate the size based on screen height
            {
                bubbleRect.anchorMin = new Vector2(0, 1 - bubbleSizePercent);
                bubbleRect.anchorMax = new Vector2((screenHeight / screenWidth) * bubbleSizePercent, 1);
            }

            //place the log panel right below the bubble
            logPanelRect.anchorMin = Vector2.zero;
            logPanelRect.anchorMax = new Vector2(1, bubbleRect.anchorMin.y);

            //place the clear button next to the bubble
            clearButtonRect.anchorMin = new Vector2(bubbleRect.anchorMax.x, bubbleRect.anchorMin.y);
            clearButtonRect.anchorMax = new Vector2(2 * bubbleRect.anchorMax.x - bubbleRect.anchorMin.x, 1);

            Stretch(bubbleRect);
            Stretch(logPanelRect);
            Stretch(clearButtonRect);
        }

        void Stretch(RectTransform content)
        {
            content.offsetMin = Vector2.zero;
            content.offsetMax = Vector2.zero;
        }
    }
}