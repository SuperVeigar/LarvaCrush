using UnityEngine;
using UnityEngine.UI;

namespace SuperVeigar
{
    public class PopupAlert : Popup
    {
        [SerializeField] Text message;

        public void Init(string message)
        {
            this.message.text = message;
        }
    }
}
