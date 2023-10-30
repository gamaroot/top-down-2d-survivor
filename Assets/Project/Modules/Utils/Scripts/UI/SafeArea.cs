using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    internal class SafeArea : MonoBehaviour
    {
#if !NO_SAFE_AREA
        [SerializeField] private RectTransform _content;

        [SerializeField] private Color _topFillColor, _bottomFillColor;
        [SerializeField] protected bool _enableTopSafeArea, _enableBottomSafeArea;

        private void OnValidate()
        {
            if (this._content == null)
                this._content = base.GetComponent<RectTransform>();
        }

        private void Awake()
        {
            bool hasSafeArea = (int)Screen.safeArea.width != (int)Screen.width || (int)Screen.safeArea.height != (int)Screen.height;
            if (hasSafeArea)
            {
                Vector2 anchorMin = Vector2.zero;
                Vector2 anchorMax = Vector2.one;

                Rect safeArea = Screen.safeArea;

                float contentHeight = Screen.height;

                if (this._enableBottomSafeArea)
                    anchorMin.y /= contentHeight;

                if (this._enableTopSafeArea)
                    anchorMax.y = (safeArea.position.y + safeArea.size.y) / contentHeight;

                this._content.anchorMin = anchorMin;
                this._content.anchorMax = anchorMax;

                this.ApplyFill(anchorMin, anchorMax);
            }
        }

        private void ApplyFill(Vector2 contentAnchorMin, Vector2 contentAnchorMax)
        {
            if (this._enableTopSafeArea && this._topFillColor.a > 0)
                this.ApplyTopFill(contentAnchorMin, contentAnchorMax);

            if (this._enableBottomSafeArea && this._bottomFillColor.a > 0)
                this.ApplyBottomFill(contentAnchorMin, contentAnchorMax);
        }

        private void ApplyTopFill(Vector2 contentAnchorMin, Vector2 contentAnchorMax)
        {
            var topFillAnchorMin = new Vector2(contentAnchorMin.x, contentAnchorMax.y);
            var topFillAnchorMax = new Vector2(contentAnchorMax.x, 1);

            this.IntantiateFill("TopFill", _topFillColor, topFillAnchorMin, topFillAnchorMax);
        }

        private void ApplyBottomFill(Vector2 contentAnchorMin, Vector2 contentAnchorMax)
        {
            var bottomFillAnchorMin = new Vector2(contentAnchorMin.x, 0);
            var bottomFillAnchorMax = new Vector2(contentAnchorMax.x, contentAnchorMin.y);

            this.IntantiateFill("BottomFill", _bottomFillColor, bottomFillAnchorMin, bottomFillAnchorMax);
        }

        private void IntantiateFill(string fillName, Color color, Vector2 anchorMin, Vector2 anchorMax)
        {
            var fill = new GameObject(fillName);
            fill.transform.SetParent(this.transform);
            fill.transform.SetAsFirstSibling();

            Image image = fill.AddComponent<Image>();
            image.color = color;

            RectTransform fillTransform = image.rectTransform;
            fillTransform.localScale = Vector3.one;
            fillTransform.anchorMin = anchorMin;
            fillTransform.anchorMax = anchorMax;
            fillTransform.anchoredPosition = Vector2.zero;
            fillTransform.offsetMin = Vector2.zero;
            fillTransform.offsetMax = Vector2.zero;
        }
#endif
    }
}