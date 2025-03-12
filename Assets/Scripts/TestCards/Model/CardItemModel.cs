namespace TestCards.Model
{
    public readonly struct CardItemModel
    {
        public readonly int Id;
        public readonly int Type;
        public readonly string CardName;
        public readonly string SpritePath;

        public CardItemModel(int id, int type, string cardName, string spritePath)
        {
            Id = id;
            Type = type;
            CardName = cardName;
            SpritePath = spritePath;
        }

        public CardItemModel(int id, CardType type, string cardName, string spritePath)
        {
            Id = id;
            Type = (int) type;
            CardName = cardName;
            SpritePath = spritePath;
        }
    }
}
