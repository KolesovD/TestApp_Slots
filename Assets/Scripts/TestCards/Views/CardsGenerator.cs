using AxGrid.Base;
using System.Collections.Generic;
using TestCards.Model;
using UnityEngine;

namespace TestCards.Views
{
    public class CardsGenerator : MonoBehaviourExt
    {
        [SerializeField] private CardView[] _cardsPrefabs;

        private Dictionary<int, CardView> _cards = new();
        public IReadOnlyDictionary<int, CardView> AllCards => _cards;

        public CardView GetCardView(CardItemModel cardItemModel)
        {
            if (!_cards.ContainsKey(cardItemModel.Id))
                GenerateNewCard(cardItemModel);

            return _cards[cardItemModel.Id];
        }

        private void GenerateNewCard(CardItemModel cardItemModel)
        {
            CardView nextCard = Instantiate(_cardsPrefabs[(int)cardItemModel.Type], transform);
            nextCard.gameObject.SetActive(true);
            nextCard.Init(cardItemModel.Id, cardItemModel.CardName, cardItemModel.SpritePath);

            _cards[cardItemModel.Id] = nextCard;
        }
    }
}
