using Database;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    internal class UpgradePanel : MonoBehaviour
    {
        [Header("Database")]
        [SerializeField] private UpgradeItemDatabase _database;

        [Header("Items")]
        [SerializeField] private StatsItem _statsItemPrefab;
        [SerializeField] private WeaponItem _weaponItemPrefab;

        [Header("Layout")]
        [SerializeField] private float _listRowHeight = 86f;
        [SerializeField] private float _listCellMargin = 10f;

        [Header("Components")]
        [SerializeField] private ScrollRect _statsScrollList;
        [SerializeField] private ScrollRect _weaponScrollList;
        [SerializeField] private RectTransform _statsScrollListContent;
        [SerializeField] private RectTransform _weaponScrollListContent;

        private readonly List<UpgradePanelItem> _items = new();

        private void Start()
        {
            this.LoadItemList(this._database.Stats, this._statsItemPrefab, this._statsScrollListContent, true);
            this.LoadItemList(this._database.Weapons, this._weaponItemPrefab, this._weaponScrollListContent);
        }

        public void ScrollToTop(int tabIndex)
        {
            ScrollRect scroll = tabIndex == 0 ? this._weaponScrollList : this._statsScrollList;
            scroll.DOVerticalNormalizedPos(1f, 0.5f).SetUpdate(true);
        }

        private void LoadItemList(UpgradeItemData[] itemList, UpgradePanelItem prefab, RectTransform container, bool isStat = false)
        {
            int totalItems = itemList.Length;

            Vector2 newSize = container.sizeDelta;
            newSize.y = this._listRowHeight * (totalItems / 2);
            container.sizeDelta = newSize;

            for (int index = 0; index < totalItems; index++)
                this.LoadItemLayout(index, this.LoadItem(prefab, itemList[index], isStat), container);
        }

        private RectTransform LoadItem(UpgradePanelItem prefab, UpgradeItemData data, bool updateValue = false)
        {
            UpgradePanelItem item = Instantiate(prefab);
            item.Load(data);
            item.SetOnButtonBuyClick(() =>
            {
                bool success = this.OnItemPurchase(data);
                item.OnUpgrade(success, data);
                if (success && updateValue)
                    item.Value = data.GetValue().ToAlphabeticNotation();
            });
            this._items.Add(item);

            return item.GetComponent<RectTransform>();
        }

        private bool OnItemPurchase(UpgradeItemData data)
        {
            Wallet instance = Wallet.Instance;
            float cost = data.GetCost();

            if (instance.Coins >= cost)
            {
                instance.SpendCoins(cost);
                ++data.Level;

                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadItemLayout(int index, RectTransform item, RectTransform container)
        {
            item.SetParent(container, false);

            var anchorMin = new Vector2(0.5f, 1f);
            var anchorMax = new Vector2(1f, 1f);
            var offsetMin = new Vector2(0, item.offsetMin.y);
            var offsetMax = new Vector2(0, item.offsetMax.y);
            if (index % 2 == 0)
            {
                anchorMin.x = 0;
                anchorMax.x = 0.5f;
            }
            item.anchorMin = anchorMin;
            item.anchorMax = anchorMax;
            item.offsetMin = offsetMin;
            item.offsetMax = offsetMax;
            item.sizeDelta = new Vector2(item.sizeDelta.x - this._listCellMargin, this._listRowHeight);
            item.anchoredPosition = new Vector2(item.anchoredPosition.x,
                                                        -((index / 2 * this._listRowHeight) + (this._listRowHeight / 2f)));
        }
    }
}