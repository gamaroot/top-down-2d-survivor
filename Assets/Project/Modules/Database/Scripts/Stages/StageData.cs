using Game;
using System;
using UnityEngine;

namespace Database
{
    [Serializable]
    public struct StageData
    {
        public int Level;
        public string Label;
        public Sprite Sprite;
        public Color FrameColor;
        public IBody PlayerInfo;
    }
}
