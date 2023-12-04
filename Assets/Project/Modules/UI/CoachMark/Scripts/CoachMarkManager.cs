using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class CoachMarkManager : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private CoachMarkInfo[] _coachMarks;

        [SerializeField] private UnityAction _onScrollStatsPanelToTop,
                                             _onScrollWeaponsPanelToTop;
        private void Awake()
        {
            this.CheckItCanBeDisabled();
        }

        internal void Show(CoachMarkType type)
        {
            CoachMarkInfo info = this._coachMarks[(int)type];
            CoachMark coachMark = Instantiate(info.Prefab);
            coachMark.transform.SetParent(this._container, false);

            info.OnShow?.Invoke();

            this.SetAsShown(type);
        }

        internal void SetAsShown(CoachMarkType type)
        {
            PlayerPrefs.SetInt(this.GetKey(type), 1);
            PlayerPrefs.Save();

            this.CheckItCanBeDisabled();
        }

        internal void SetAsShown(CoachMarkType[] types)
        {
            for (int index = 0; index < types.Length; index++)
            {
                PlayerPrefs.SetInt(this.GetKey(types[index]), 1);
            }
            PlayerPrefs.Save();

            this.CheckItCanBeDisabled();
        }

        internal bool NeedToShow(CoachMarkType type)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return false;
#else
            return PlayerPrefs.GetInt(this.GetKey(type), 0) == 0;
#endif
        }

        internal bool NeedToShow(CoachMarkType[] types)
        {
            for (int index = 0; index < types.Length; index++)
            {
                if (this.NeedToShow(types[index]))
                    return true;
            }
            return false;
        }

        private string GetKey(CoachMarkType type)
        {
            return $"HAS_COACH_MARK_SHOWN_{type}";
        }

        private void CheckItCanBeDisabled()
        {
            bool anyCoachMarkToShow = false;
            int totalTypes = Enum.GetValues(typeof(CoachMarkType)).Length;
            for (int index = 0; index < totalTypes; index++)
            {
                if (this.NeedToShow((CoachMarkType)index))
                {
                    anyCoachMarkToShow = true;
                    break;
                }
            }
            if (!anyCoachMarkToShow)
                base.enabled = false;
        }
    }
}