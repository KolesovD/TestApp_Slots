using AxGrid.Base;
using UnityEngine;

namespace TestSlots.Views
{
    public class SlotImageContainerView : MonoBehaviourExt
    {
        public SlotImageView SlotImage { get; private set; }

        public void SetSlotImage(SlotImageView slotImage)
        {
            if (SlotImage != null && SlotImage.SlotViewId == slotImage.SlotViewId)
                return;

            RemoveSlotImage();
            if (slotImage.Container && slotImage.Container != this)
                slotImage.Container.RemoveSlotImage();

            SlotImage = slotImage;
            SlotImage.SetContainer(this);

            SlotImage.transform.SetParent(transform);
            SlotImage.transform.localPosition = Vector3.zero;
            SlotImage.gameObject.SetActive(true);
        }

        public void RemoveSlotImage()
        {
            if (!SlotImage)
                return;

            SlotImage.ClearContainer();
            SlotImage.gameObject.SetActive(false);
            SlotImage = null;
        }
    }
}
