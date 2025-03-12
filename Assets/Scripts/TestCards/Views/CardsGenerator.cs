using AxGrid.Base;
using TestCards.Model;
using UnityEngine;

namespace TestCards.Views
{
    public class CardsGenerator : MonoBehaviourExt
    {
        [SerializeField] private CardView[] _cardsPrefabs;

        private CardListsContainer _cardListsContainer;

        public void Init(CardListsContainer cardListsContainer)
        {
            _cardListsContainer = cardListsContainer;

            Model.EventManager.AddAction<CardItemModel>("CardGenerated", GenerateNewCard);
        }

        private void GenerateNewCard(CardItemModel cardItemModel)
        {
            CardView nextCard = Instantiate(_cardsPrefabs[(int)cardItemModel.Type], transform);
            nextCard.gameObject.SetActive(true);
            nextCard.Init(_cardListsContainer, cardItemModel.Id, cardItemModel.CardName, cardItemModel.SpritePath);
        }

        [OnDestroy]
        private void Dispose()
        {
            Model.EventManager.RemoveAction<CardItemModel>("CardGenerated", GenerateNewCard);
        }
    }
}
