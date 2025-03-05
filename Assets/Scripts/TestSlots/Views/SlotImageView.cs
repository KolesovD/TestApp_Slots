using AxGrid.Base;
using TestSlots.Animations;
using UnityEngine;

namespace TestSlots.Views
{
    public class SlotImageView : MonoBehaviourExt
    {
        [SerializeField] private UnityAnimationController _animationController;
        [Space]
        [SerializeField] private int _slotViewId;

        public int SlotViewId => _slotViewId;

        public SlotImageContainerView Container { get; private set; }

        public void PlayAnimation(AnimationData animationData)
        {
            _animationController.PlayAnimation(animationData);
        }

        internal void SetContainer(SlotImageContainerView container)
        {
            Container = container;
        }

        internal void ClearContainer()
        {
            Container = null;
        }
    }
}
