using I2.Loc;
using System;
using UnityEngine;

namespace Database
{
    [Serializable]
    public class StatsData : UpgradeItemData
    {
        [field: SerializeField] public float InitialStats { get; private set; }
        [field: SerializeField] public float StatsPerLevel { get; private set; }
        [field: SerializeField] public string DisplayTranslation { get; private set; }

        public string DisplayText => LocalizationManager.GetTranslation(this.DisplayTranslation);

        public override float GetValue() => this.InitialStats + (this.StatsPerLevel * base.Level);
    }
}