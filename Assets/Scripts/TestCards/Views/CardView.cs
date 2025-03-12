using AxGrid.Base;
using AxGrid.Path;
using AxGrid.Utils;
using System.Collections;
using TestSlots.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace TestCards.Views
{
    public class CardView : MonoBehaviourExt2D
    {
        [SerializeField] private TMP_Text _cardText;
        [SerializeField] private SpriteRenderer _cardImage;
        [SerializeField] private SortingGroup _sortingGroup;

        public int Id { get; private set; }

        private const float CARD_UPDATE_POS_TIME = .2f;
        private const float CARD_UPDATE_POS_CHANGE_PARENT_TIME = .6f;

        private const float CARDS_POS_DELTA = 1.5f;
        private const int CARDS_CHANGE_PARENT_SORTING = 500;

        public void Init(int id, string cardName, string spritePath)
        {
            Id = id;
            _cardText.text = cardName;

            StartCoroutine(LoadSprite(spritePath));
        }

        private IEnumerator LoadSprite(string spritePath)
        {
            _cardImage.color = _cardImage.color.SetA(0f);

            var spriteRequest = Resources.LoadAsync<Sprite>(spritePath);
            yield return spriteRequest;

            if (spriteRequest.asset && _cardImage)
            {
                _cardImage.sprite = spriteRequest.asset as Sprite;
                _cardImage.color = _cardImage.color.SetA(1f);
            }
        }

        public void CardUpdatePosition(Transform listTransform, int cardIndex, int maxIndex)
        {
            bool wasParentChanged = false;
            if (transform.parent != listTransform)
            {
                transform.SetParent(listTransform, true);
                wasParentChanged = true;
            }

            float minX = maxIndex * -.5f * CARDS_POS_DELTA;
            float cardPosX = minX + cardIndex * CARDS_POS_DELTA;
            float cardPosY = (cardIndex % 2 == 0) ? .05f : -.05f;

            Vector3 wasPos = transform.localPosition;

            Path?.StopPath();

            if (cardPosX.FEquals(wasPos.x) && cardPosY.FEquals(wasPos.y))
                return;

            Path = CPath.Create();

            if (wasParentChanged)
            {
                _sortingGroup.sortingOrder = CARDS_CHANGE_PARENT_SORTING;
                Path.EasingSineEaseOut(CARD_UPDATE_POS_CHANGE_PARENT_TIME, 0f, 1f, MoveCard);
            }
            else
                Path.EasingSineEaseOut(CARD_UPDATE_POS_TIME, 0f, 1f, MoveCard);

            Path.Action(() => _sortingGroup.sortingOrder = cardIndex);

            void MoveCard(float progress)
            {
                Vector3 needPos = new Vector3(Mathf.Lerp(wasPos.x, cardPosX, progress),
                            Mathf.Lerp(wasPos.y, cardPosY, progress), wasPos.z);

                transform.localPosition = needPos;
            }
        }
    }
}