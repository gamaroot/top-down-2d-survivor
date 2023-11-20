using UnityEngine;

namespace Utils
{
    internal class CameraHandler
    {
        internal static CameraHandler Instance { get; private set; }

        internal Camera MainCamera { get; private set; }
        internal CameraBounds CameraBounds { get; private set; }

        internal static void Load()
        {
            Instance = new CameraHandler();
            Instance.MainCamera = Camera.main;
            Instance.CameraBounds = Instance.GetCameraBounds();
        }

        internal Vector2 GetCameraSize()
        {
            return 2f * this.MainCamera.orthographicSize * new Vector2(this.MainCamera.aspect, 1f);
        }

        private CameraBounds GetCameraBounds()
        {
            var bounds = new Vector3(this.MainCamera.pixelWidth, this.MainCamera.pixelHeight, this.MainCamera.transform.position.z);
            Vector3 worldBasedBounds = this.MainCamera.ScreenToWorldPoint(bounds);

            float verticalSize = worldBasedBounds.y;
            float horizontalSize = worldBasedBounds.x;

            return new CameraBounds(-horizontalSize, horizontalSize, verticalSize, -verticalSize);
        }
    }
}