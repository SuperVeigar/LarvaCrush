using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace SuperVeigar
{
    public class Popup : MonoBehaviour
    {
        public PopupType type;
        [SerializeField] private Button close;
        [SerializeField] private Button bottomRight;
        [SerializeField] private Button bottomLeft;
        [SerializeField] private Button bottomCenter;
        private Action OnClose;

        public void InitCommand(PopupButtonCommand bottomRight = null, PopupButtonCommand bottomLeft = null, PopupButtonCommand bottomCenter = null, PopupButtonCommand close = null)
        {
            SetButton(this.bottomRight, bottomRight);
            SetButton(this.bottomLeft, bottomLeft);
            SetButton(this.bottomCenter, bottomCenter);
            SetCloseButton(close);
        }

        private void SetButton(Button button, PopupButtonCommand command)
        {
            if (command != null)
            {
                button.gameObject.SetActive(true);

                button.OnClickAsObservable().Subscribe(_ =>
                {
                    command.Excute();
                }).AddTo(this);
            }
        }

        private void SetCloseButton(PopupButtonCommand close)
        {
            if (this.close != null)
            {
                this.close.gameObject.SetActive(true);
                
                OnClose += Close;

                if (close != null)
                {
                    OnClose += close.Excute;
                }

                this.close.OnClickAsObservable().Subscribe(_ =>
                {
                    OnClose?.Invoke();
                }).AddTo(this);
            }
        }

        protected virtual void Close()
        {
            PopupService.Instance.RemoveLastPopup();
        }
    }
}
