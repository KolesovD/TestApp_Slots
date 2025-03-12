using AxGrid.FSM;
using AxGrid.Tools.Binders;
using TestCards.Model;
using TestSlots.Utils;
using UnityEngine;

namespace TestCards.StateMachine
{
    [State(CardsFSM.CARDS_STATE_GENERATE_CARD)]
    public class CardsStateGenerateCard : FSMState
    {
        [Enter]
        private void Enter()
        {
            UpdateButton();
            GenerateCard();

            Parent.Change(CardsFSM.CARDS_STATE_UPDATE_VISUAL_MAIN_LIST);
        }

        private void UpdateButton()
        {
            Model.Set($"Btn{CardsFSM.BUTTON_DRAW}{UIButtonDataBind.BUTTON_ENABLE_PART}", false);
        }

        private void GenerateCard()
        {
            int cardId = Model.Get($"{CardsFSM.LAST_CARD_ID_FIELD}", 0);
            CardType cardType = (CardType) Random.Range(0, 4);
            string cardName = CardNames.Names.RandomElement();
            string cardSprite = CardSprites.Pathes.RandomElement();

            Model.Set($"{CardsFSM.LAST_CARD_ID_FIELD}", ++cardId);

            int totalCards = Model.Get($"{CardsFSM.TOTAL_CARDS_FIELD}", 0);
            Model.Set($"{CardsFSM.TOTAL_CARDS_FIELD}", ++totalCards);

            ListWithEvents<CardItemModel> cardList = Model.Get<ListWithEvents<CardItemModel>>(CardsFSM.CARD_LIST_1);

            CardItemModel nextCard = new CardItemModel(cardId, cardType, cardName, cardSprite);
            cardList.Add(nextCard);

            Model.EventManager.Invoke("CardGenerated", nextCard);
        }
    }
}
