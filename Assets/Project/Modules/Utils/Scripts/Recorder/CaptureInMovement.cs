using System.IO;
using UnityEngine;

namespace Recorder
{
    internal class CaptureInMovement : MonoBehaviour
    {
        [SerializeField] private bool _pauseOnEnd;

        private int _fileCounter;
        private float _timeElapsed;
        private float _totalTimeElapsed;

        private Camera _camera;

        private void Start()
        {
            this._camera = base.GetComponent<Camera>();
        }

        private void Update()
        {
            base.transform.Translate(10f * Vector3.right * Time.deltaTime);

            this._timeElapsed += Time.deltaTime;
            this._totalTimeElapsed += Time.deltaTime;

            if (this._totalTimeElapsed > 3f && this._timeElapsed >= 0.03)
            {
                this.Capture();
                this._timeElapsed = 0;
            }
        }

        private void Capture()
        {
            if (this._fileCounter >= 30)
            {
                if (this._pauseOnEnd) Debug.Break();
                return;
            }

            RenderTexture activeRenderTexture = RenderTexture.active;
            RenderTexture.active = this._camera.targetTexture;

            this._camera.Render();

            var image = new Texture2D(this._camera.targetTexture.width, this._camera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, this._camera.targetTexture.width, this._camera.targetTexture.height), 0, 0);
            image.Apply();
            RenderTexture.active = activeRenderTexture;

            byte[] bytes = image.EncodeToPNG();
            Destroy(image);

            File.WriteAllBytes($"{Application.dataPath}/../Recordings/{++this._fileCounter}.png", bytes);
        }
    }
}