using Database;
using I2.Loc;
using ScreenNavigation;
using UnityEngine;

namespace Game
{
    internal class StageSelectionScreenEvents : MonoBehaviour
    {
        [SerializeField] private StageDatabase _database;
        [SerializeField] private StageSelectorView _selectorPrefab;
        [SerializeField] private Transform _scrolllistContainer;

        private void Awake()
        {
            for (int index = 0; index < this._database.Stages.Length; index++)
            {
                StageSelectorView item = Instantiate(this._selectorPrefab);
                item.name = $"{ScriptLocalization.ScreenStageSelection.STAGE} {index + 1}";

                StageData data = this._database.Stages[index];
                data.Label = item.name;

                int stageNumber = index;
                item.Setup(data, () =>
                {
                    SceneNavigator.Instance.UnloadSceneAsync(SceneID.STAGE_SELECTION);
                    SceneNavigator.Instance.LoadAdditiveSceneAsync(SceneID.GAME);

                    SceneNavigator.Instance.SetSceneParams(SceneID.GAME, data);
                });
                item.transform.SetParent(this._scrolllistContainer);
            }
        }
    }
}