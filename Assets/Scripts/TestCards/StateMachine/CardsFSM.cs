using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

namespace TestCards.StateMachine
{
    public class CardsFSM : MonoBehaviourExt
    {
        internal const string CARDS_STATE_INIT = "CardsStateInit";
        internal const string CARDS_STATE_MAIN = "CardsStateMain";
        internal const string CARDS_STATE_GENERATE_CARD = "CardsStateGenerateCard";
        internal const string CARDS_STATE_FULL_UPDATE_VISUAL = "CardsStateFullUpdateVisual";
        internal const string CARDS_STATE_UPDATE_VISUAL_MAIN_LIST = "CardsStateUpdateVisualMainList";

        internal const string BUTTON_DRAW = "Draw";

        internal const string LAST_CARD_ID_FIELD = "LastCardId";
        internal const string TOTAL_CARDS_FIELD = "TotalCards";
        internal const string CARD_LIST_1 = "CardList1";
        internal const string CARD_LIST_2 = "CardList2";
        internal const string CARD_LIST_3 = "CardList3";

        internal const string CARD_UPDATE_POS_EVENT = "CardUpdatePos";

        [OnAwake]
        private void InitFSM()
        {
            FSM cardsFSM = new();
            cardsFSM.Add(
                new CardsStateInit(),
                new CardsStateMain(),
                new CardsStateGenerateCard(),
                new CardsStateUpdateVisualMainList(),
                new CardsStateFullUpdateVisual());

            Settings.Fsm = cardsFSM;
        }

        [OnStart]
        private void StartFSM()
        {
            Settings.Fsm.Start(CARDS_STATE_INIT);
        }

        [OnUpdate]
        private void UpdateFSM()
        {
            Settings.Fsm.Update(Time.deltaTime);
        }
    }
}