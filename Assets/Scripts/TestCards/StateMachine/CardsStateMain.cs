using AxGrid.FSM;
using AxGrid.Model;
using AxGrid.Tools.Binders;
using System.Linq;
using TestCards.Model;

namespace TestCards.StateMachine
{
    [State(CardsFSM.CARDS_STATE_MAIN)]
    public class CardsStateMain : FSMState
    {
        private ListWithEvents<CardItemModel> _cardList1Saved;

        [Enter]
        private void Enter()
        {
            UpdateButton();

            _cardList1Saved = Model.Get<ListWithEvents<CardItemModel>>($"{CardsFSM.CARD_LIST_1}");
        }

        private void UpdateButton()
        {
            Model.Set($"Btn{CardsFSM.BUTTON_DRAW}{UIButtonDataBind.BUTTON_ENABLE_PART}", true);
        }

        [Bind("OnBtn")]
        private void OnBtn(string buttonName)
        {
            switch (buttonName)
            {
                case CardsFSM.BUTTON_DRAW:
                    Parent.Change(CardsFSM.CARDS_STATE_GENERATE_CARD);
                    break;
            }
        }

        [Bind("OnCardClick")]
        private void OnCardClick(int cardId)
        {
            if (!_cardList1Saved.Any(x => x.Id == cardId))
                return;

            var cardData = _cardList1Saved.First(x => x.Id == cardId);

            _cardList1Saved.Remove(cardData);

            ListWithEvents<CardItemModel> cardList2Saved = Model.Get<ListWithEvents<CardItemModel>>($"{CardsFSM.CARD_LIST_2}");
            cardList2Saved.Add(cardData);

            Parent.Change(CardsFSM.CARDS_STATE_FULL_UPDATE_VISUAL);
        }
    }
}
