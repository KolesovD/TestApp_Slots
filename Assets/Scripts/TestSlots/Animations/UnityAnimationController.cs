using AxGrid.Base;
using UnityEngine;

namespace TestSlots.Animations
{
    public class UnityAnimationController : MonoBehaviourExt
    {
        public AnimationData CurrentPlayingAnimation { get; private set; }

        [OnAwake]
        private void OnAwake()
        {
            Init();
        }

        public void Init()
        {
            if (Animator)
                Animator.keepAnimatorStateOnDisable = true;
        }

        public void SetChildVisible(string boneName, bool visible)
        {
            if (!Animator)
                return;

            Transform bone = Animator.transform.Find(boneName);
            if (!bone)
                return;

            bone.gameObject.SetActive(visible);
        }

        public void PlayAnimation(AnimationData animation, float normalizedTime = 0f)
        {
            if (!Animator)
                return;

            CurrentPlayingAnimation = animation;

            if (!Animator.gameObject.activeInHierarchy)
            {
                return;
            }

            var wasPlayOnInvisible = PlayOnInvisible;

            PlayOnInvisible = true;
            Animator.enabled = true;
            Animator.Play(animation.Hash, -1, normalizedTime);

            //Без этого, два вызова Play с разными анимациями в одном кадре запустит первую анимацию, а не последнюю
            Animator.Update(0f);

            PlayOnInvisible = wasPlayOnInvisible;
        }

        public bool Enabled
        {
            get => Animator && Animator.enabled;

            set
            {
                if (Animator)
                    Animator.enabled = value;
            }
        }

        private SpriteRenderer[] _renderers;
        private ParticleSystem[] _particles;

        protected float _alpha = 1f;
        protected bool _alphaIsValid = true;

        public float Alpha
        {
            get => _alpha;
            set
            {
                if (!_alpha.Equals(value))
                    _alphaIsValid = false;

                if (_alphaIsValid)
                    return;

                _alpha = value;

                if (!gameObject)
                    return;

                _renderers ??= GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
                foreach (var r in _renderers)
                    if (r)
                        r.color = new Color(r.color.r, r.color.g, r.color.b, value);

                if (Animator)
                {
                    var wasAnimatorEnabled = Animator.enabled;
                    Animator.enabled = Alpha.Equals(1f);

                    if (wasAnimatorEnabled != Animator.enabled)
                    {
                        _alphaIsValid = false;
                        _renderers = null;
                        Alpha = Alpha;
                    }
                }

                _particles ??= gameObject.GetComponentsInChildren<ParticleSystem>(includeInactive: true);
                foreach (var particle in _particles)
                {
                    if (!Alpha.Equals(1f))
                        particle.Stop();
                    else
                        particle.Play();
                }
            }
        }


        protected Color _color = Color.white;
        public Color Color
        {
            get => _color;
            set
            {
                if (_color.Equals(value))
                    return;

                _color = value;

                if (!gameObject)
                    return;

                _renderers ??= GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
                foreach (var r in _renderers)
                    if (r)
                        r.color = _color;
            }
        }

        public void SetInteger(AnimationData name, int value)
        {
            if (!Animator.gameObject.activeInHierarchy)
                return;

            if (Animator)
                Animator.SetInteger(name.Hash, value);
        }

        public void SetFloat(AnimationData name, float value)
        {
            if (!Animator.gameObject.activeInHierarchy)
                return;

            if (Animator)
                Animator.SetFloat(name.Hash, value);
        }

        public void SetBoolean(AnimationData name, bool value)
        {
            if (!Animator.gameObject.activeInHierarchy)
                return;

            if (Animator)
                Animator.SetBool(name.Hash, value);
        }

        public bool PlayOnInvisible
        {
            get
            {
                if (Animator)
                    return Animator.cullingMode == AnimatorCullingMode.AlwaysAnimate;

                return false;
            }
            set
            {
                if (Animator)
                    Animator.cullingMode =
                        value ? AnimatorCullingMode.AlwaysAnimate : AnimatorCullingMode.CullCompletely;
            }
        }
    }
}
