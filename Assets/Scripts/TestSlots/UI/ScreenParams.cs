using UnityEngine;

namespace TestSlots.UI
{
    public readonly struct ScreenParams
    {
        public readonly Rect HudRect;
        public readonly RectCorners Corners;

        public ScreenParams(Rect hudRect, RectCorners corners)
        {
            HudRect = hudRect;
            Corners = corners;
        }
    }
}
