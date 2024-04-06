using UnityEngine;

namespace SuperVeigar
{
    public abstract class IngameObject : MonoBehaviour
    {
        public abstract void Reset();
        public abstract void OnReadyState();
        public abstract void OnPlayState();
        public virtual void OnPlayFixedState() {}
    }
}