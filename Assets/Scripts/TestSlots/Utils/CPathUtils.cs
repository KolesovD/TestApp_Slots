using AxGrid.Path;

namespace TestSlots.Utils
{
    public static class CPathUtils
    {
        public static CPath WaitForFrames(this CPath cpath, int frames)
        {
            int currentFrames = frames;
            return cpath.Add(() => --currentFrames <= 0 ? Status.OK : Status.Continue);
        }
    }
}
