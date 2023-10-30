using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    internal class UpgradePanelTabs : MonoBehaviour
    {
        private const string CHANGE_TAB_TRIGGER = "ChangeTab";

        [SerializeField] private Animator animator;

        private int _activeTabIndex;

        private void OnValidate()
        {
            if (this.animator == null)
                this.animator = base.GetComponent<Animator>();
        }

        public void OnTabClick(int tabIndex)
        {
            if (tabIndex != this._activeTabIndex)
            {
                this._activeTabIndex = tabIndex;
                this.animator.SetTrigger(CHANGE_TAB_TRIGGER);
            }
        }
    }
}