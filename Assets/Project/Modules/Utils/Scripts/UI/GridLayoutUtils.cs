using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    [RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))]
    internal class GridLayoutUtils : MonoBehaviour
    {
        [SerializeField] private float _paddingWidthRatio = 0.03703704f;

        [Header("Components")]
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridLayoutGroup _gridLayout;

        private void OnValidate()
        {
            if (this._rectTransform == null)
                this._rectTransform = base.GetComponent<RectTransform>();

            if (this._gridLayout == null)
                this._gridLayout = base.GetComponent<GridLayoutGroup>();
        }

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            float padding = this._paddingWidthRatio * this._rectTransform.rect.width;
            float columns = this._gridLayout.constraintCount;

            float cellWidth = (this._rectTransform.rect.width - (columns * padding)) / columns;
            float cellHeight = cellWidth;

            this._gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
            this._gridLayout.spacing = new Vector2(padding + cellWidth, padding + cellHeight);

            int paddingAsInt = (int)padding;
            int topPadding = (int)((cellHeight / 2f) + (2f * padding));
            this._gridLayout.padding = new RectOffset(paddingAsInt, paddingAsInt, topPadding, paddingAsInt);
#else
            Destroy(this);
#endif
        }
    }
}
