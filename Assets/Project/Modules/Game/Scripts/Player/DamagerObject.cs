using UnityEngine;

namespace Game
{
    public class DamagerObject : MonoBehaviour
    {
        [field: SerializeField] internal DamagerObjectType Type { get; private set; }

        internal float Damage
        {
            get => this.RawDamage + this.GetExtraDamage();
            set => this.RawDamage = value;
        }
        internal float RawDamage;

        internal virtual float GetExtraDamage() => 0;
    }
}