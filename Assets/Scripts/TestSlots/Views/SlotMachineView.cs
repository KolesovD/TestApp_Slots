using AxGrid.Base;
using AxGrid.Path;
using System;
using TestSlots.Animations;
using TestSlots.UI;
using UnityEngine;

namespace TestSlots.Views
{
    public class SlotMachineView : MonoBehaviourExt
    {
        [SerializeField] private Transform _slotMoveContainer;
        [SerializeField] private SlotImageContainerView[] _slotContainers;
        [SerializeField] private SlotImageView[] _slotsViews;
        [Space]
        [SerializeField, Range(0f, 1f)] private float _slotProgress;

        private float _lastProgress;

        private const string MOVING_SLOTS_PROPERTY = "MovingSlots";
        private const float SLOT_MOVE_MAX_Y_DELTA = -1.6f;

        private const float ACCELERATION_TIME = 2f;
        private const float ACCELERATION_DELTA = 3f;

        private const float FULL_CICLE_TIME = .35f;
        private const float FULL_CICLE_DELTA = 1f;

        private const float STOP_TIME = 1.15f;
        private const float STOP_DELTA_MIN = 1.3f;
        private const float STOP_DELTA_MAX = 1.7f;

        private const int MAX_ANIM_STATE = 4;

        private const string SCREEN_PARAMS_PROPERTY = "ScreenParams";
        private const float HUD_TOP_HEIGHT = 50f;
        private const float HUD_BOTTOM_HEIGHT = 300f;
        private const float HUD_FROM_SIZES_WIDTH = 50f;

        private const float SLOTS_GLOBAL_WIDTH = 4.6f;
        private const float SLOTS_GLOBAL_HEIGHT = 7.8f;

        private float _oneViewProgressDelta;
        private bool _isMoving;
        private CPath _slotsStartStopMovingPath;
        private CPath _slotsMovingPath;

        [OnAwake]
        private void Init()
        {
            _oneViewProgressDelta = 1f / _slotsViews.Length;
            UpdateProgress(0f, 0);
            _isMoving = false;
        }

        [OnStart]
        private void SetupMoving()
        {
            Model.EventManager.AddAction<bool>($"On{MOVING_SLOTS_PROPERTY}Changed", OnMovingStateChanged);
            OnMovingStateChanged(Model.GetBool(MOVING_SLOTS_PROPERTY, false));

            Model.EventManager.AddAction<ScreenParams>($"On{SCREEN_PARAMS_PROPERTY}Changed", Resize);
            Resize(Model.Get<ScreenParams>(SCREEN_PARAMS_PROPERTY));
        }

        private void OnMovingStateChanged(bool isMoving)
        {
            if (_isMoving == isMoving)
                return;

            _isMoving = isMoving;
            if (_isMoving)
                SetStartMovingPath();
            else
                SetStopMovingPath();
        }

        private void SetStartMovingPath()
        {
            DisposeStartStopMovingPath();
            DisposeMovingPath();

            float startProgress = _lastProgress;
            float endProgress = startProgress + ACCELERATION_DELTA;

            _slotsStartStopMovingPath = CreateNewPath()
                .EasingQuadEaseIn(ACCELERATION_TIME, startProgress, endProgress, progress =>
                {
                    int animationState = Mathf.RoundToInt((progress - startProgress) / ACCELERATION_DELTA * MAX_ANIM_STATE);
                    UpdateProgress(progress, animationState);
                })
                .Action(() => UpdateProgress(endProgress, MAX_ANIM_STATE))
                .Action(SetMovingPath);
        }

        private void SetMovingPath()
        {
            DisposeMovingPath();

            float startProgress = _lastProgress;
            float endProgress = startProgress + FULL_CICLE_DELTA;

            _slotsMovingPath = CPath.Create();
            _slotsMovingPath.Loop = true;

            _slotsMovingPath.EasingLinear(FULL_CICLE_TIME, startProgress, endProgress, progress => UpdateProgress(progress, MAX_ANIM_STATE));

            Path = _slotsMovingPath;
        }

        private void SetStopMovingPath()
        {
            DisposeMovingPath();
            DisposeStartStopMovingPath();

            float startProgress = _lastProgress;
            float endProgress = startProgress + UnityEngine.Random.Range(STOP_DELTA_MIN, STOP_DELTA_MAX);

            endProgress -= endProgress % 1f % _oneViewProgressDelta;

            float progressDelta = endProgress - startProgress;

            _slotsStartStopMovingPath = CreateNewPath()
                .EasingQuadEaseOut(STOP_TIME, startProgress, endProgress, progress =>
                {
                    int animationState = MAX_ANIM_STATE - Mathf.RoundToInt((progress - startProgress) / progressDelta * MAX_ANIM_STATE);
                    UpdateProgress(progress, animationState);
                })
                .Action(() => UpdateProgress(endProgress, 0))
                .Action(() => Model.EventManager.Invoke("OnSpinEnd"));
        }

        private void DisposeStartStopMovingPath()
        {
            if (_slotsStartStopMovingPath == null)
                return;

            _slotsStartStopMovingPath.StopPath();
            _slotsStartStopMovingPath = null;
        }

        private void DisposeMovingPath()
        {
            if (_slotsMovingPath == null)
                return;

            _slotsMovingPath.StopPath();
            _slotsMovingPath = null;

            Path = null;
        }

        private void UpdateProgress(float progress, int animationState)
        {
            float realProgress;
            if (progress > 1f)
                realProgress = progress % 1f;
            else if (progress < 0f)
            {
                realProgress = progress * -1f;
                realProgress = Mathf.Ceil(realProgress) - realProgress;
            }
            else
                realProgress = progress;

            _lastProgress = realProgress;

            realProgress /= _oneViewProgressDelta;

            float moveSlotValue = realProgress % 1;
            int startSlotIndex = Mathf.RoundToInt(realProgress - moveSlotValue);

            SetSlotsMoveProgress(moveSlotValue);
            SetSlotsImages(startSlotIndex);
            UpdateAnimations(GetAnimByState(animationState));
        }

        private void SetSlotsMoveProgress(float progress)
        {
            _slotMoveContainer.localPosition = new Vector3(0f, progress * SLOT_MOVE_MAX_Y_DELTA, 0f);
        }

        private void SetSlotsImages(int startIndex)
        {
            int slotContainersLenght = _slotContainers.Length;
            int slotsViewsLenght = _slotsViews.Length;

            for (int i = 0; i < slotContainersLenght; i++)
                _slotContainers[i].SetSlotImage(_slotsViews[(i + startIndex) % slotsViewsLenght]);
        }

        private void UpdateAnimations(AnimationData animationData)
        {
            foreach (var slotView in _slotsViews)
                slotView.PlayAnimation(animationData);
        }

        private AnimationData GetAnimByState(int state)
        {
            switch (state)
            {
                case 0:
                    return AnimationStates.STATE_0;

                case 1:
                    return AnimationStates.STATE_1;

                case 2:
                    return AnimationStates.STATE_2;

                case 3:
                    return AnimationStates.STATE_3;

                case 4:
                    return AnimationStates.STATE_4;

                default:
                    if (state < 0)
                        return AnimationStates.STATE_0;
                    else
                        return AnimationStates.STATE_4;
            }
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

            float scaleByWidth = _worldScreenWidth / SLOTS_GLOBAL_WIDTH;
            float scaleByHeight = _worldScreenHeight / SLOTS_GLOBAL_HEIGHT;

            float slotsScale = Mathf.Min(scaleByWidth, scaleByHeight);
            transform.localScale = Vector3.one * slotsScale;

            float centerY = (maxY + minY) * .5f;
            float centerX = (maxX + minX) * .5f;

            transform.localPosition = new Vector3(centerX, centerY);
        }

        [OnDestroy]
        private void OnDestroyInner()
        {
            Model.EventManager.RemoveAction<bool>($"On{MOVING_SLOTS_PROPERTY}Changed", OnMovingStateChanged);
            Model.EventManager.RemoveAction<ScreenParams>(SCREEN_PARAMS_PROPERTY, Resize);

            DisposeStartStopMovingPath();
            DisposeMovingPath();
        }
    }
}
