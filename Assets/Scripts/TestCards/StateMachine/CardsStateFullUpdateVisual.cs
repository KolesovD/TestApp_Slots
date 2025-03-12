using AxGrid.FSM;
using AxGrid.Tools.Binders;

namespace TestCards.StateMachine
{
    [State(CardsFSM.CARDS_STATE_FULL_UPDATE_VISUAL)]
    public class CardsStateFullUpdateVisual : FSMState
    {
        private const float WAIT_FOR_NEXT_STATE = .6f;

        [Enter]
        private void Enter()
        {
            UpdateButton();
        }

        private void UpdateButton()
        {
            Model.Set($"Btn{CardsFSM.BUTTON_DRAW}{UIButtonDataBind.BUTTON_ENABLE_PART}", false);
        }

        [One(WAIT_FOR_NEXT_STATE)]
        private void ToNextState()
        {
            Parent.Change(CardsFSM.CARDS_STATE_MAIN);
        }
    }
}
