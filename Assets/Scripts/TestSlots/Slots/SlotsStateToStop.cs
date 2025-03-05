using AxGrid.FSM;
using AxGrid.Tools.Binders;

namespace TestSlots.Slots
{
    [State(SlotsFSM.SLOTS_STATE_TO_STOP)]
    public class SlotsStateToStop : FSMState
    {
        private const float WAIT_FOR_NEXT_STATE = 2f;

        [Enter]
        public void Enter()
        {
            UpdateButtons();

            Model.Set(SlotsFSM.MOVING_SLOTS_PROPERTY, false);
        }

        public void UpdateButtons()
        {
            Model.Set($"Btn{SlotsFSM.BUTTON_START}{UIButtonDataBind.BUTTON_ENABLE_PART}", false);
            Model.Set($"Btn{SlotsFSM.BUTTON_STOP}{UIButtonDataBind.BUTTON_ENABLE_PART}", false);
        }

        [One(WAIT_FOR_NEXT_STATE)]
        private void ToNextState()
        {
            Parent.Change(SlotsFSM.SLOTS_STATE_STOP);
        }
    }
}
