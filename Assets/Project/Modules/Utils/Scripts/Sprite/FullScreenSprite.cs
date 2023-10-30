using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal class FullScreenSprite : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void OnValidate()
        {
            if (this._spriteRenderer == null)
                this._spriteRenderer = base.GetComponent<SpriteRenderer>();
        }

        private void OnEnable() => this._spriteRenderer.size = CameraHandler.Instance.GetCameraSize();
    }
}