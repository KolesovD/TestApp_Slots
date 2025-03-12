using AxGrid.Path;
using AxGrid.Utils;

namespace TestSlots.Utils
{
    public static class EasingPathExtra
    {
        private delegate float DEasing(float delta, float from, float to, float time);

        private static CPath Apply(CPath path, CPathEasingAction action, DEasing method, float from, float to, float time)
        {
            return path.Add(delegate (CPath p)
            {
                if (p.DeltaF < time)
                {
                    action(method(p.DeltaF, from, to, time));
                    return Status.Continue;
                }

                action(to);
                return Status.OK;
            });
        }

        public static CPath EasingSineEaseOut(this CPath path, float time, float from, float to, CPathEasingAction action)
        {
            return Apply(path, action, EasingTo.SineEaseOut, from, to, time);
        }

        public static CPath EasingSineEaseInOut(this CPath path, float time, float from, float to, CPathEasingAction action)
        {
            return Apply(path, action, EasingTo.SineEaseInOut, from, to, time);
        }
    }
}
