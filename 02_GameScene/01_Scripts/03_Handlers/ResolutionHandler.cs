using UnityEngine;

namespace SuperVeigar
{
    public class ResolutionHandler : MonoBehaviour
    {
        private const float RESOLUTION_RATIO = 2f;
        private const float CAMERA_SIZE = 10f;

        private void Start()
        {
            float cameraSize = CAMERA_SIZE * ((float)Screen.height / (float)Screen.width) / RESOLUTION_RATIO;

            Camera.main.orthographicSize = cameraSize;
        }
    }
}