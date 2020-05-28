using System.Collections;
using UnityEngine;

namespace UnityLog.Core
{
    public class PopUp : MonoBehaviour
    {
        void Start()
        {
            transform.localScale = Vector3.zero;
        }

        public void Open()
        {
            StartCoroutine(ScaleTo(Vector3.one, LogManager.animationDuration));
        }

        public void Close()
        {
            StartCoroutine(ScaleTo(Vector3.zero, LogManager.animationDuration));
        }

        IEnumerator ScaleTo(Vector3 to, float duration)
        {
            Vector3 from = transform.localScale;
            float percent = 0;

            while (percent < 1)
            {
                percent += Time.unscaledDeltaTime / duration;
                transform.localScale = Vector3.Lerp(from, to, percent);
                yield return null;
            }
        }
    }
}