using AxGrid.Base;
using TestCards.Model;
using UnityEngine;

namespace TestCards.Views
{
    public class CardListParentView : MonoBehaviourExt
    {
        [SerializeField] private string _listId;
        public string ListId => _listId;

        private CardsGenerator _cardsGenerator;

        public void Init(CardsGenerator cardsGenerator)
        {
            _cardsGenerator = cardsGenerator;

            Model.EventManager.AddAction<DynamicListFixed<CardItemModel>>($"On{_listId}Changed", OnModelListChanged);
        }

        private void OnModelListChanged(DynamicListFixed<CardItemModel> modelList)
        {
            int listLastIndex = modelList.Count - 1;
            for (int i = 0; i <= listLastIndex; i++)
            {
                CardItemModel nextCardModel = modelList[i];
                CardView nextCardView = _cardsGenerator.GetCardView(nextCardModel);

                int index = i;
                nextCardView.CardUpdatePosition(transform, index, listLastIndex);
            }
        }

        [OnDestroy]
        private void OnDestroyInner()
        {
            Model.EventManager.RemoveAction<DynamicListFixed<CardItemModel>>($"On{_listId}Changed", OnModelListChanged);
        }
    }
}
