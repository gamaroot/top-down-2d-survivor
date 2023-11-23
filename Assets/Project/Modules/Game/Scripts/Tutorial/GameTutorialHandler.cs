using Database;
using UnityEngine;

namespace Game
{
    internal class GameTutorialHandler : MonoBehaviour
    {
        [SerializeField] private UpgradeItemDatabase _itemDatabase;
        [SerializeField] private CoachMarkManager _coachMark;

        private void Awake()
        {
            this.SetupCoachMarks();
        }

        private void SetupCoachMarks()
        {
            var coachMarks = new CoachMarkType[]
            {
                CoachMarkType.DESTROY_ENEMIES,
                CoachMarkType.IMPROVE_YOUR_WEAPON,
                CoachMarkType.IMPROVE_YOUR_WEAPON_AGAIN
            };
            if (this._itemDatabase.GetDPS() >= 15)
            {
                this._coachMark.SetAsShown(coachMarks);
            }
            else if (this._coachMark.NeedToShow(coachMarks))
            {
                Wallet.Instance.OnChange += this.CheckCoachMarkWhenGetCoins;
            }
        }

        private void CheckCoachMarkWhenGetCoins(float _, float currCoins)
        {
            if (!this._coachMark.isActiveAndEnabled)
                return;

            if (currCoins >= 3 &&
                this._coachMark.NeedToShow(CoachMarkType.DESTROY_ENEMIES))
            {
                this._coachMark.Show(CoachMarkType.DESTROY_ENEMIES);
            }
            else if (currCoins >= 10 &&
                    this._coachMark.NeedToShow(CoachMarkType.IMPROVE_YOUR_WEAPON))
            {
                this._coachMark.Show(CoachMarkType.IMPROVE_YOUR_WEAPON);
            }
            else if (currCoins >= 22 &&
                     this._coachMark.NeedToShow(CoachMarkType.IMPROVE_YOUR_WEAPON_AGAIN))
            {
                this._coachMark.Show(CoachMarkType.IMPROVE_YOUR_WEAPON_AGAIN);
                Wallet.Instance.OnChange -= this.CheckCoachMarkWhenGetCoins;
            }
        }

        // Called through inspector
        public void OnPlayerLose()
        {
            if (!this._coachMark.isActiveAndEnabled)
                return;

            if (this._itemDatabase.GetDPS() <= 15 &&
                this._coachMark.NeedToShow(CoachMarkType.BUY_STRONGER_WEAPON) &&
               !this._coachMark.NeedToShow(CoachMarkType.IMPROVE_YOUR_WEAPON) &&
               !this._coachMark.NeedToShow(CoachMarkType.IMPROVE_YOUR_WEAPON_AGAIN))
            {
                this._coachMark.Show(CoachMarkType.BUY_STRONGER_WEAPON);
            }
        }
    }
}