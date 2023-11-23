using I2.Loc;

namespace Game
{
    internal class PopupStageCleared : Popup
    {
        private void Awake()
        {
            int display = Statistics.Instance.CurrentWave + 2;
            base.ButtonConfirmText = string.Format(ScriptLocalization.PopupStageCleared.STAGE, display);
        }
    }
}