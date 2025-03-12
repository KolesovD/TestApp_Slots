using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestSlots.Utils
{
    public static class UIUtils
    {
        private static List<RaycastResult> raycastResults = new List<RaycastResult>();
        private static PointerEventData eventDataCurrentPosition = null;

        /// <summary>
        /// Находится ли указатель поверх UI
        /// </summary>
        /// <returns>Истина если указатель поверх UI</returns>
        public static bool IsPointerOverUIObject()
        {
            if (eventDataCurrentPosition == null)
                eventDataCurrentPosition = new PointerEventData(EventSystem.current);

            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            EventSystem.current.RaycastAll(eventDataCurrentPosition, raycastResults);

            return raycastResults.Count > 0;
        }
    }
}
