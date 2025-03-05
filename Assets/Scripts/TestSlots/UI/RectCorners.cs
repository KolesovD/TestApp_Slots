using UnityEngine;

namespace TestSlots.UI
{
    public readonly struct RectCorners
    {
        private readonly Vector3[] _corners;

        public RectCorners(Vector3[] corners)
        {
            _corners = corners;
        }

        public float MaxY()
        {
            return _corners[1].y;
        }

        public float MinY()
        {
            return _corners[0].y;
        }

        public float MaxX()
        {
            return _corners[2].x;
        }

        public float MinX()
        {
            return _corners[0].x;
        }

        public override bool Equals(object obj)
        {
            return obj is RectCorners other && Equals(other);
        }

        public bool Equals(RectCorners other)
        {
            //Сравниваем только по айди предмета для скорости
            return MinX() == other.MinX() && MaxX() == other.MaxX() &&
                MinY() == other.MinY() && MaxY() == other.MaxY();
        }

        public static bool operator ==(RectCorners first, RectCorners second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(RectCorners first, RectCorners second)
        {
            return !first.Equals(second);
        }
    }

    public static class RectCornersUtils
    {
        public static RectCorners GetCorners(this RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return new RectCorners(corners);
        }
    }
}
