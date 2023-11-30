using Database;
using I2.Loc;
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

            int unlockLevel = (data as WeaponData).UnlockLevel;

#if DEBUG_MODE
            this.RemoveLocker();
#else
            if (Statistics.Instance.TotalEnemiesDestroyed >= unlockLevel)
            {
                this.RemoveLocker();
            }
            else
            {
                this._locker.SetActive(true);

                UpdateLockedText();
                Statistics.Instance.OnTotalEnemiesDestroyedChange += OnTotalEnemiesDestroyedChange;

                void UpdateLockedText()
                {
                    int enemiesLeft = unlockLevel - Statistics.Instance.TotalEnemiesDestroyed;
                    string formatKey = enemiesLeft > 1 ? ScriptLocalization.UpgradePanel.UNLOCK_AT : 
                                                         ScriptLocalization.UpgradePanel.UNLOCK_AT_SINGULAR;

                    this._textLocked.text = string.Format(formatKey, enemiesLeft);
                }

                void OnTotalEnemiesDestroyedChange(int enemiesDestroyed)
                {
                    UpdateLockedText();
                    if (enemiesDestroyed >= unlockLevel)
                    {
                        this.RemoveLocker();
                        Statistics.Instance.OnTotalEnemiesDestroyedChange -= OnTotalEnemiesDestroyedChange;
                    }
                }
            }
#endif
        }

        private void RemoveLocker()
        {
            if (!gameObject) return;

            this._content.SetActive(true);
            Destroy(this._locker);
        }
    }
}