using AxGrid.Base;
using UnityEngine;

namespace TestSlots.Particles
{
    public class ParticlesManager : MonoBehaviourExt
    {
        [SerializeField] private ParticlesView[] _spinEndParticles;

        [OnStart]
        private void Init()
        {
            Model.EventManager.AddAction("OnSpinEnd", OnSpinEnd);
        }

        private void OnSpinEnd()
        {
            foreach (var particle in _spinEndParticles)
                particle.Play();
        }

        [OnDestroy]
        private void OnDestroyInner()
        {
            Model.EventManager.RemoveAction("OnSpinEnd", OnSpinEnd);
        }
    }
}
