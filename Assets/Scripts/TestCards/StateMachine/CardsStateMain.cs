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
        private DynamicListFixed<CardItemModel> _cardList1Saved;
        private DynamicListFixed<CardItemModel> _cardList2Saved;

        [Enter]
        private void Enter()
        {
            UpdateButton();

            _cardList1Saved = Model.Get<DynamicListFixed<CardItemModel>>($"{CardsFSM.CARD_LIST_1}");
            _cardList2Saved = Model.Get<DynamicListFixed<CardItemModel>>($"{CardsFSM.CARD_LIST_2}");
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
            if (_cardList1Saved.Any(x => x.Id == cardId))
                ChangeList(cardId, _cardList1Saved, _cardList2Saved);
            else if (_cardList2Saved.Any(x => x.Id == cardId))
                ChangeList(cardId, _cardList2Saved, Model.Get<DynamicListFixed<CardItemModel>>($"{CardsFSM.CARD_LIST_3}"));
        }

        private void ChangeList(int cardId, DynamicListFixed<CardItemModel> listFrom, DynamicListFixed<CardItemModel> listTo)
        {
            var cardData = listFrom.First(x => x.Id == cardId);

            listFrom.Remove(cardData);
            listTo.Add(cardData);

            Parent.Change(CardsFSM.CARDS_STATE_FULL_UPDATE_VISUAL);
        }
    }
}
