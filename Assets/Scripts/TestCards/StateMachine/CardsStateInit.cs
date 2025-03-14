﻿using AxGrid.FSM;
using TestCards.Model;

namespace TestCards.StateMachine
{
    [State(CardsFSM.CARDS_STATE_INIT)]
    public class CardsStateInit : FSMState
    {
        [Enter]
        private void Enter()
        {
            Model.Set($"{CardsFSM.LAST_CARD_ID_FIELD}", 0);
            Model.Set($"{CardsFSM.TOTAL_CARDS_FIELD}", 0);

            //Видел в библиотеке DynamicList, но там нет конструктора с созданием внутреннего списка
            Model.Set($"{CardsFSM.CARD_LIST_1}", new DynamicListFixed<CardItemModel>());
            Model.Set($"{CardsFSM.CARD_LIST_2}", new DynamicListFixed<CardItemModel>());
            Model.Set($"{CardsFSM.CARD_LIST_3}", new DynamicListFixed<CardItemModel>());

            Parent.Change(CardsFSM.CARDS_STATE_MAIN);
        }
    }
}
