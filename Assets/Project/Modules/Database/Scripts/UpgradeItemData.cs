using Game;
using System;
using UnityEngine;

namespace Database
{
    [Serializable]
    public class UpgradeItemData
    {
        public int ID;

        [field: SerializeField] public UpgradeType Type { get; private set; }
        [field: SerializeField] public float BaseCost { get; private set; } = 10f;
        [field: SerializeField] public string ValueSuffix { get; private set; } = "DPS";

        public static Action<UpgradeItemData> OnLevelChange;

        public int Level
        {
            get => PlayerPrefs.GetInt($"ITEM_{this.ID}", this.level);
            set
            {
                this.level = value;
                PlayerPrefs.SetInt($"ITEM_{this.ID}", value);
                PlayerPrefs.Save();

                OnLevelChange?.Invoke(this);
            }
        }
        private int level = 0;
        public float GetCost()
        {
            return (int)Math.Max(this.BaseCost, this.BaseCost * (float)Math.Pow(this.Level, 1.2));
        }

        public virtual float GetValue()
        {
            return 0;
        }
    }
}