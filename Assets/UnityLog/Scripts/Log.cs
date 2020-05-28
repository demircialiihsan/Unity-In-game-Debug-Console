using UnityEngine;
using UnityEngine.UI;
using UnityLog.Core;

namespace UnityLog
{
    public static class Log
    {
        private static Text s_logText;
        private static Bubble s_bubble;

        public static void Init(Text logText, Bubble bubble)
        {
            s_logText = logText;
            s_bubble = bubble;
        }

        public static void DebugLog(object message)
        {
            if (LogManager.instance == null)
            {
                Debug.LogWarning("UnityLog is not present in this scene!");
                return;
            }
            s_logText.text += message;
            s_bubble.UpdateMessageCount();
        }
    }
}