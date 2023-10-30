using System;
using UnityEngine.Events;

namespace Game
{
    [Serializable]
    internal struct CoachMarkInfo
    {
        public CoachMark Prefab;
        public UnityEvent OnShow;
    }
}