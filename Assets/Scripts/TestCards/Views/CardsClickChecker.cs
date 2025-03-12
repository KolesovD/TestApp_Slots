using AxGrid;
using AxGrid.Base;
using TestSlots.Utils;
using UnityEngine;

namespace TestCards.Views
{
    public class CardsClickChecker : MonoBehaviourExt
    {
        private Camera _mainCamera;

        [OnAwake]
        private void Init()
        {
            _mainCamera = Camera.main;
        }

        [OnUpdate]
        private void ClickCheck()
        {
            bool isMouseButtonDown = Input.GetMouseButtonDown(0);
            bool isMouseButtonHolding = Input.GetMouseButton(0);
            bool isMouseButtonUp = Input.GetMouseButtonUp(0);

            //Иногда Юнити при переключении на экран считает, что все 3 ивента происходят одновременно
            //Откидываем такие случаи
            if (isMouseButtonDown && isMouseButtonHolding && isMouseButtonUp)
                return;

            if (!isMouseButtonDown)
                return;

            if (UIUtils.IsPointerOverUIObject())
                return;

            var hits = Physics2D.GetRayIntersectionAll(_mainCamera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
                return;

            Collider2D hitCollider = hits[0].collider;
            CardView hitCard = hitCollider.GetComponent<CardView>();

            if (!hitCard)
                return;

            Settings.Fsm?.Invoke("OnCardClick", hitCard.Id);
        }
    }
}
