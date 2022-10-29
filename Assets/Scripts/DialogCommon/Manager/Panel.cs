using UnityEngine;

namespace DialogCommon.Manager
{
    public class Panel : MonoBehaviour, IPanel
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual bool IsOpened()
        {
            return gameObject.activeSelf;
        }
    }
}