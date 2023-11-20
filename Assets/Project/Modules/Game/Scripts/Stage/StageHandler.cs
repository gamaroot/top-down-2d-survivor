using Database;
using DG.Tweening;
using System;
using UnityEngine;
using Utils;

namespace Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StageHandler : MonoBehaviour
    {
        private const float BORDER_THICKNESS = 5f;

        [SerializeField] private SpriteRenderer _radar;
        [SerializeField] private SpriteRenderer _background;

        private IPlayerInfo _playerInfo;

        private void OnValidate()
        {
            if (this._background == null)
                this._background = base.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Vector2 offset = this._playerInfo.GetPosition() / (10f * base.transform.localScale.x);
            this._background.material.SetTextureOffset("_MainTex", offset);
        }

        internal void Setup(StageData data)
        {
            this._playerInfo = data.PlayerInfo;

            this._background.sprite = data.Sprite;
            this._background.size = CameraHandler.Instance.GetCameraSize();

            this._radar.DOColor(data.FrameColor, 2f);

            this.CreateScreenBounds();
        }

        private void CreateScreenBounds()
        {
            CameraBounds stageBounds = CameraHandler.Instance.CameraBounds;
            Vector2 stageSize = CameraHandler.Instance.GetCameraSize();
            stageSize.x *= 1.5f;

            for (int index = 0; index < Enum<Alignment>.Length(); index++)
            {
                var alignment = (Alignment)index;

                Border borderParams = this.GetBorderParams(stageBounds, stageSize, alignment);
                this.CreateBorder(borderParams);
            }
        }

        private void CreateBorder(Border borderParams)
        {
            var border = new GameObject(borderParams.Name)
            {
                tag = base.gameObject.tag,
                layer = base.gameObject.layer
            };

            Transform colliderTransform = border.transform;
            colliderTransform.SetParent(base.transform, false);

            colliderTransform.localPosition = borderParams.Position;
            colliderTransform.localScale = borderParams.Scale;

            border.AddComponent<BoxCollider2D>();
            Rigidbody2D rigidBody = border.AddComponent<Rigidbody2D>();
            rigidBody.simulated = true;
            rigidBody.useFullKinematicContacts = true;
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
            rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        private Border GetBorderParams(CameraBounds bounds, Vector2 stageSize, Alignment alignment)
        {
            string borderName = $"Border - {alignment}";
            float borderMargin = BORDER_THICKNESS / 2f;

            return alignment switch
            {
                Alignment.LEFT => new Border(borderName, alignment,
                                             new Vector2(bounds.Left - borderMargin, 0),
                                             new Vector2(BORDER_THICKNESS, stageSize.y)),

                Alignment.RIGHT => new Border(borderName, alignment,
                                              new Vector2(bounds.Right + borderMargin, 0),
                                              new Vector2(BORDER_THICKNESS, stageSize.y)),

                Alignment.TOP => new Border(borderName, alignment,
                                            new Vector2(0, bounds.Top + borderMargin),
                                            new Vector2(stageSize.x, BORDER_THICKNESS)),

                Alignment.BOTTOM => new Border(borderName, alignment,
                                               new Vector2(0, bounds.Bottom - borderMargin),
                                               new Vector2(stageSize.x, BORDER_THICKNESS)),

                _ => throw new Exception($"Invalid StageBorder.GetBorderParams alignment parameter: {alignment}"),
            };
        }
    }

    struct Border
    {
        internal string Name;
        internal Alignment Alignment;
        internal Vector2 Position;
        internal Vector2 Scale;

        internal Border(string name, Alignment alignment, Vector2 position, Vector2 scale)
        {
            this.Name = name;
            this.Alignment = alignment;
            this.Position = position;
            this.Scale = scale;
        }
    }
}