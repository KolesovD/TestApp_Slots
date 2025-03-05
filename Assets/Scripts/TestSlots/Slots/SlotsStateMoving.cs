using AxGrid.FSM;
using AxGrid.Model;
using AxGrid.Tools.Binders;

namespace TestSlots.Slots
{
    [State(SlotsFSM.SLOTS_STATE_MOVING)]
    public class SlotsStateMoving : FSMState
    {
        [Enter]
        public void Enter()
        {
            UpdateButtons();
        }

        public void UpdateButtons()
        {
            Model.Set($"Btn{SlotsFSM.BUTTON_START}{UIButtonDataBind.BUTTON_ENABLE_PART}", false);
            Model.Set($"Btn{SlotsFSM.BUTTON_STOP}{UIButtonDataBind.BUTTON_ENABLE_PART}", true);
        }

        [Bind("OnBtn")]
        private void OnBtn(string buttonName)
        {
            switch (buttonName)
            {
                case SlotsFSM.BUTTON_STOP:
                    Parent.Change(SlotsFSM.SLOTS_STATE_TO_STOP);
                    break;
            }
        }
    }
}
