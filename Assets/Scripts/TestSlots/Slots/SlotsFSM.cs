using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

namespace TestSlots.Slots
{
    public class SlotsFSM : MonoBehaviourExt
    {
        internal const string SLOTS_STATE_INIT = "SlotsStateInit";
        internal const string SLOTS_STATE_STOP = "SlotsStateStop";
        internal const string SLOTS_STATE_TO_MOVING = "SlotsStateToMoving";
        internal const string SLOTS_STATE_MOVING = "SlotsStateMoving";
        internal const string SLOTS_STATE_TO_STOP = "SlotsStateToStop";

        internal const string BUTTON_START = "Start";
        internal const string BUTTON_STOP = "Stop";

        internal const string MOVING_SLOTS_PROPERTY = "MovingSlots";

        [OnAwake]
        private void Init()
        {
            FSM slotsFSM = new();
            slotsFSM.Add(
                new SlotsStateInit(),
                new SlotsStateStop(),
                new SlotsStateToMoving(),
                new SlotsStateMoving(),
                new SlotsStateToStop());

            Settings.Fsm = slotsFSM;
        }

        [OnStart]
        private void StartFSM()
        {
            Settings.Fsm.Start(SLOTS_STATE_INIT);
        }

        [OnUpdate]
        private void UpdateFSM()
        {
            Settings.Fsm.Update(Time.deltaTime);
        }
    }
}
