using AxGrid.FSM;
using AxGrid.Tools.Binders;

namespace TestSlots.Slots
{
    [State(SlotsFSM.SLOTS_STATE_TO_MOVING)]
    public class SlotsStateToMoving : FSMState
    {
        private const float WAIT_FOR_NEXT_STATE = 3f;

        [Enter]
        public void Enter()
        {
            UpdateButtons();

            Model.Set(SlotsFSM.MOVING_SLOTS_PROPERTY, true);
        }

        public void UpdateButtons()
        {
            Model.Set($"Btn{SlotsFSM.BUTTON_START}{UIButtonDataBind.BUTTON_ENABLE_PART}", false);
            Model.Set($"Btn{SlotsFSM.BUTTON_STOP}{UIButtonDataBind.BUTTON_ENABLE_PART}", false);
        }

        [One(WAIT_FOR_NEXT_STATE)]
        private void ToNextState()
        {
            Parent.Change(SlotsFSM.SLOTS_STATE_MOVING);
        }
    }
}
