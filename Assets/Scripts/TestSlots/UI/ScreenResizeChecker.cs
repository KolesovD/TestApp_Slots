using AxGrid.Base;
using TestSlots.Utils;
using UnityEngine;

namespace TestSlots.UI
{
    public class ScreenResizeChecker : MonoBehaviourExt
    {
        private RectTransform _rectTransform;

        private int _width;
        private int _height;
        private ScreenOrientation _orientation;

        private Rect _hudRect;
        private RectCorners _corners;

        private bool _needUpdateParams = false;

        [OnStart]
        private void Init()
        {
            _rectTransform = GetComponent<RectTransform>();

            _width = Screen.width;
            _height = Screen.height;
            _orientation = Screen.orientation;

            _hudRect = _rectTransform.rect;
            _corners = _rectTransform.GetCorners();

            Model.Set("ScreenParams", new ScreenParams(_hudRect, _corners));
        }

        [OnUpdate]
        private void OnUpdate()
        {
            if (_width != Screen.width || _height != Screen.height || _orientation != Screen.orientation || _corners != _rectTransform.GetCorners())
            {
                _width = Screen.width;
                _height = Screen.height;
                _orientation = Screen.orientation;

                //Ждём скейл канваса
                Path?.StopPath();
                Path = new AxGrid.Path.CPath()
                    .WaitForFrames(1)
                    .Action(() => _needUpdateParams = true);
            }
        }

        //В MonoBehaviourExt нет такого атрибута
        private void LateUpdate()
        {
            if (_needUpdateParams)
            {
                _needUpdateParams = false;

                _hudRect = _rectTransform.rect;
                _corners = _rectTransform.GetCorners();

                Model.Set("ScreenParams", new ScreenParams(_hudRect, _corners));
            }
        }
    }
}
