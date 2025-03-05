using AxGrid.FSM;

namespace TestSlots.Slots
{
    [State(SlotsFSM.SLOTS_STATE_INIT)]
    public class SlotsStateInit : FSMState
    {
        [Enter]
        public void Enter()
        {
            Parent.Change(SlotsFSM.SLOTS_STATE_STOP);
        }
    }
}
