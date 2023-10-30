using Database;
using TMPro;
using UnityEngine;

namespace Game
{
    internal class StatsItem : UpgradePanelItem
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI display;

        internal override void Load(UpgradeItemData data)
        {
            base.Load(data);

            this.display.text = ((StatsData)data).DisplayText;
        }
    }
}