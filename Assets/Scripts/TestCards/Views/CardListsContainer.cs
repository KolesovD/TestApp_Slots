using AxGrid.Base;
using System.Collections.Generic;
using TestSlots.UI;
using UnityEngine;

namespace TestCards.Views
{
    public class CardListsContainer : MonoBehaviourExt
    {
        [SerializeField] private CardsGenerator _cardsGenerator;
        [SerializeField] private CardListParentView[] _cardLists;

        public IReadOnlyList<CardListParentView> CardLists => _cardLists;

        private const string SCREEN_PARAMS_PROPERTY = "ScreenParams";
        private const float HUD_TOP_HEIGHT = 50f;
        private const float HUD_BOTTOM_HEIGHT = 250f;
        private const float HUD_FROM_SIZES_WIDTH = 50f;

        private const float CARDS_GLOBAL_WIDTH = 16f;
        private const float CARDS_GLOBAL_HEIGHT = 13f;

        [OnStart]
        private void Init()
        {
            _cardsGenerator.Init(this);

            Model.EventManager.AddAction<ScreenParams>($"On{SCREEN_PARAMS_PROPERTY}Changed", Resize);
            Resize(Model.Get<ScreenParams>(SCREEN_PARAMS_PROPERTY));
        }

        private void Resize(ScreenParams screenParams)
        {
            float minX = screenParams.Corners.MinX();
            float maxX = screenParams.Corners.MaxX();
            float minY = screenParams.Corners.MinY();
            float maxY = screenParams.Corners.MaxY();

            float bottomBorder = screenParams.HudRect.yMin;
            float topBorder = screenParams.HudRect.yMax;
            float leftBorder = screenParams.HudRect.xMin;
            float rightBorder = screenParams.HudRect.xMax;

            minY = (bottomBorder + HUD_BOTTOM_HEIGHT) / bottomBorder * minY;
            maxY = (topBorder - HUD_TOP_HEIGHT) / topBorder * maxY;
            minX = (leftBorder + HUD_FROM_SIZES_WIDTH) / leftBorder * minX;
            maxX = (rightBorder - HUD_FROM_SIZES_WIDTH) / rightBorder * maxX;

            float _worldScreenHeight = maxY - minY;
            float _worldScreenWidth = maxX - minX;

            float scaleByWidth = _worldScreenWidth / CARDS_GLOBAL_WIDTH;
            float scaleByHeight = _worldScreenHeight / CARDS_GLOBAL_HEIGHT;

            float slotsScale = Mathf.Min(scaleByWidth, scaleByHeight);
            transform.localScale = Vector3.one * slotsScale;

            float centerY = (maxY + minY) * .5f;
            float centerX = (maxX + minX) * .5f;

            transform.localPosition = new Vector3(centerX, centerY);
        }

        [OnDestroy]
        private void OnDestroyInner()
        {
            Model.EventManager.RemoveAction<ScreenParams>(SCREEN_PARAMS_PROPERTY, Resize);
        }
    }
}
