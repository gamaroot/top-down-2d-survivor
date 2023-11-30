using UnityEngine;
using Utils;
using static UnityEngine.Rendering.DebugUI;

namespace Game
{
    internal class EnemyMark : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private IBody _playerBody, _enemyBody;

        private void OnValidate()
        {
            if (this._spriteRenderer == null)
                this._spriteRenderer = base.GetComponent<SpriteRenderer>();
        }

        internal void Setup(IBody playerBody, IBody enemyBody)
        {
            this._playerBody = playerBody;
            this._enemyBody = enemyBody;
        }

        private void Update()
        {
            if (!this._enemyBody.IsAlive())
            {
                base.gameObject.SetActive(false);
                return;
            }

            Alignment alignment = this.UpdatePosition();
            bool isInVisibleArea = alignment != Alignment.NONE;
            this._spriteRenderer.enabled = isInVisibleArea;

            if (!isInVisibleArea)
            {
                return;
            }
            this.UpdateRotation(alignment);
        }

        private Alignment UpdatePosition()
        {
            Alignment alignment = Alignment.NONE;

            Vector2 playerPosition = this._playerBody.GetPosition();
            Vector2 enemyPosition = this._enemyBody.GetPosition();
            Vector2 markPosition = enemyPosition;

            CameraBounds bounds = CameraHandler.Instance.GetRelativeCameraBounds(playerPosition);

            if (enemyPosition.x < bounds.Left - 1f)
            {
                alignment = Alignment.LEFT;
                markPosition.x = bounds.Left + 0.5f;
            }
            else if (enemyPosition.x > bounds.Right + 1f)
            {
                alignment = Alignment.RIGHT;
                markPosition.x = bounds.Right - 0.5f;
            }
            else if (enemyPosition.y > bounds.Top + 1f)
            {
                alignment = Alignment.TOP;
                markPosition.y = bounds.Top - 0.5f;
            }
            else if (enemyPosition.y < bounds.Bottom - 1f)
            {
                alignment = Alignment.BOTTOM;
                markPosition.y = bounds.Bottom + 0.5f;
            }

            // Forces to stay inside camera bounds
            if (markPosition.x < bounds.Left)
                markPosition.x = bounds.Left + 0.5f;

            else if (markPosition.x > bounds.Right)
                markPosition.x = bounds.Right - 0.5f;

            else if (markPosition.y > bounds.Top)
                markPosition.y = bounds.Top - 0.5f;

            else if (markPosition.y < bounds.Bottom)
                markPosition.y = bounds.Bottom + 0.5f;

            base.transform.position = markPosition;
            return alignment;
        }

        private void UpdateRotation(Alignment alignment)
        {
            float angle = 0;
            switch (alignment)
            {
                case Alignment.LEFT:
                    angle = 90f;
                    break;
                case Alignment.RIGHT:
                    angle = 30f;
                    break;
                case Alignment.TOP:
                    angle = 0;
                    break;
                case Alignment.BOTTOM:
                    angle = 180f;
                    break;
            }

            base.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}