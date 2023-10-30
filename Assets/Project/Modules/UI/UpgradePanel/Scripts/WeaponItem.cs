using Database;
using TMPro;
using UnityEngine;

namespace Game
{
    internal class WeaponItem : UpgradePanelItem
    {
        [SerializeField] private GameObject _content;

        [Header("Locker")]
        [SerializeField] private GameObject _locker;
        [SerializeField] private TextMeshProUGUI _textLocked;

        internal override void Load(UpgradeItemData data)
        {
            base.Load(data);

            var weaponData = data as WeaponData;

            if (Statistics.Instance.HighestWave >= weaponData.UnlockLevel)
            {
                this.RemoveLocker();
            }
            else
            {
                this._locker.SetActive(true);
                this._textLocked.text = string.Format(_textLocked.text, weaponData.UnlockLevel);
                Statistics.Instance.OnHighestWaveChange += OnHighestLevelChange;

                void OnHighestLevelChange(int highestLevel)
                {
                    if (highestLevel >= weaponData.UnlockLevel)
                    {
                        this.RemoveLocker();
                        Statistics.Instance.OnHighestWaveChange -= OnHighestLevelChange;
                    }
                }
            }
        }

        private void RemoveLocker()
        {
            this._content.SetActive(true);
            Destroy(this._locker);
        }
    }
}