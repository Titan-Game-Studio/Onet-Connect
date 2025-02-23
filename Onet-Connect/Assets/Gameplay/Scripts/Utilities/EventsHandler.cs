using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TGS.OnetConnect.Gameplay.Scripts.Utilities
{
    public class EventsHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        public Action AtcPointerDown;
        public Action AtcPointerUp;
        public Action AtcPointerClick;
        public Action AtcPointerEnter;
        public Action AtcPointerExit;

        private bool _isEnabled;

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            AtcPointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            AtcPointerUp?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            AtcPointerClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            AtcPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            AtcPointerExit?.Invoke();
        }
    }
}