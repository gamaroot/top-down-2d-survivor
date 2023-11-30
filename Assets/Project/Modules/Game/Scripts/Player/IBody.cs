using UnityEngine;

namespace Game
{
    public interface IBody
    {
        public Vector3 GetPosition();
        public bool IsAlive();
    }
}