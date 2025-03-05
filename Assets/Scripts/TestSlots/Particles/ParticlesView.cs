using AxGrid.Base;
using UnityEngine;

namespace TestSlots.Particles
{
    public class ParticlesView : MonoBehaviourExt
    {
        [SerializeField] private ParticleSystem[] _particles;
        [SerializeField] private TrailRenderer[] _trails;

        public void Init()
        {
            Pause();
        }

        public void Pause()
        {
            foreach (var particleSystem in _particles)
                if (particleSystem)
                    particleSystem.Stop();

            if (_trails != null)
                foreach (var trail in _trails)
                    trail.Clear();
        }

        public void Play()
        {
            foreach (var particleSystem in _particles)
                if (particleSystem)
                    particleSystem.Play();

            if (_trails != null)
                foreach (var trail in _trails)
                    trail.Clear();
        }

        public void Restart()
        {
            foreach (var particleSystem in _particles)
                if (particleSystem)
                    particleSystem.Simulate(0f, false, true);

            if (_trails != null)
                foreach (var trail in _trails)
                    trail.Clear();
        }
    }
}
