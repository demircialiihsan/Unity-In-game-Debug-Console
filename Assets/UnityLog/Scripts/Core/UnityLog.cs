using UnityEngine;

namespace UnityLog.Core
{
    public class UnityLog : MonoBehaviour
    {
        public bool dontDestroyOnLoad;
        [Header("Visual")]
        public int fontSize;
        public Color fontColor;
        public Color backgroundColor;

        void Start()
        {
            GetComponentInChildren<LogManager>().SetProperties(dontDestroyOnLoad, fontSize, fontColor, backgroundColor);
        }
    }
}