using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace SuperVeigar
{
    public enum PopupType
    {
        Alert = 0,

    }

    public class PopupService : SingletonBehaviour<PopupService>
    {
        [SerializeField] private List<Popup> popupPrefabs;
        [SerializeField] private List<Popup> popups;
        [SerializeField] private Transform popupParent;
        [SerializeField] private Button blackBG;

        private void Start()
        {
            popups = new List<Popup>();
        }

        public void CreatePopup(PopupType type, bool isSingle = true, PopupButtonCommand bottomRight = null, PopupButtonCommand bottomLeft = null, PopupButtonCommand bottomCenter = null, Action<Popup> OnSuccess = null)
        {
            for (int i = 0; i < popupPrefabs.Count; i++)
            {
                if (popupPrefabs[i].type == type)
                {
                    SetBlackBG();

                    if (isSingle == true && popups.Count > 0)
                    {
                        popups[popups.Count - 1].gameObject.SetActive(false);
                    }

                    Popup popup = Instantiate(popupPrefabs[i].gameObject, popupParent).GetComponent<Popup>();

                    popup.InitCommand(bottomRight, bottomLeft, bottomCenter);

                    popups.Add(popup);

                    OnSuccess?.Invoke(popup);
                    break;
                }
            }
        }

        public void RemoveLastPopup()
        {
            if (popups.Count > 0)
            {
                int index = popups.Count - 1;

                Popup popup = popups[index];
                popups.RemoveAt(index);
                
                Destroy(popup.gameObject);

                if (popups.Count > 1)
                {
                    popups[index].transform.SetAsLastSibling();
                    popups[index].gameObject.SetActive(true);
                }
                else
                {
                    blackBG.gameObject.SetActive(false);
                }
            }
        }

        private void SetBlackBG()
        {
            blackBG.transform.SetAsLastSibling();
            blackBG.gameObject.SetActive(true);
        }

        public bool IsPopupNow()
        {
            return popups.Count > 0;
        }

#region Common popup

        public void CreateAlertPopupWithCenterButton(string message)
        {
            PopupService.Instance.CreatePopup(PopupType.Alert, 
                bottomCenter: new PopupButtonCommand(() => PopupService.Instance.RemoveLastPopup()),
                OnSuccess: popup =>
                {
                    PopupAlert popupAlert = popup as PopupAlert;
                    popupAlert.Init(message);
                });
        }

#endregion
    }
}