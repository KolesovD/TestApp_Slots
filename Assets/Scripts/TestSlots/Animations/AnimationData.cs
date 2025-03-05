using UnityEngine;

namespace TestSlots.Animations
{
    public readonly struct AnimationData
    {
        public readonly string Name;
        public readonly int Hash;

        public AnimationData(string name)
        {
            Name = name;
            Hash = Animator.StringToHash(name);
        }

        public override bool Equals(object obj)
        {
            return obj is AnimationData other && Equals(other);
        }

        public bool Equals(AnimationData other)
        {
            return Hash == other.Hash;
        }

        public static bool operator ==(AnimationData first, AnimationData second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(AnimationData first, AnimationData second)
        {
            return !first.Equals(second);
        }

        public override int GetHashCode() => Hash;
    }
}
