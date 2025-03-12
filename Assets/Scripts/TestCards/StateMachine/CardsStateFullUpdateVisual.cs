using AxGrid.FSM;
using AxGrid.Tools.Binders;
using TestCards.Model;

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
            UpdateCardsPositions();
        }

        private void UpdateButton()
        {
            Model.Set($"Btn{CardsFSM.BUTTON_DRAW}{UIButtonDataBind.BUTTON_ENABLE_PART}", false);
        }

        private void UpdateCardsPositions()
        {
            SendUpdateCardsPositionsEventForList(CardsFSM.CARD_LIST_1);
            SendUpdateCardsPositionsEventForList(CardsFSM.CARD_LIST_2);
            SendUpdateCardsPositionsEventForList(CardsFSM.CARD_LIST_3);
        }

        private void SendUpdateCardsPositionsEventForList(string listId)
        {
            ListWithEvents<CardItemModel> cardList1 = Model.Get<ListWithEvents<CardItemModel>>(listId);
            int cardList1MaxIndex = cardList1.Count - 1;
            for (int i = 0; i <= cardList1MaxIndex; i++)
                Model.EventManager.Invoke(CardsFSM.CARD_UPDATE_POS_EVENT, cardList1[i].Id, i, cardList1MaxIndex, listId);
        }

        [One(WAIT_FOR_NEXT_STATE)]
        private void ToNextState()
        {
            Parent.Change(CardsFSM.CARDS_STATE_MAIN);
        }
    }
}
