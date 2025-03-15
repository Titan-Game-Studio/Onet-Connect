using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TGS.OnetConnect.Gameplay.Scripts.Utilities
{
    public class EventsHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        public Action PointerDownAtc;
        public Action PointerUpAtc;
        public Action PointerClickAtc;
        public Action PointerEnterAtc;
        public Action PointerExitAtc;

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
            
            PointerDownAtc?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            PointerUpAtc?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            PointerClickAtc?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            PointerEnterAtc?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isEnabled)
            {
                return;
            }

            PointerExitAtc?.Invoke();
        }
    }
}