using UnityEngine;

namespace Database
{
    public class StageDatabase : ScriptableObject
    {
        [field: SerializeField] public StageData[] Stages { get; private set; }
    }
}