using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    public class CanvasCameraController : MonoBehaviour
    {
        // --- ÝÞTE BU SATIR EKSÝK OLDUÐU ÝÇÝN HATA ALIYORSUN ---
        public static bool IsLocked = false;
        // -----------------------------------------------------

        [Header("Ayarlar")]
        [SerializeField] private float zoomMin = 2f;
        [SerializeField] private float zoomMax = 15f;
        [SerializeField] private float zoomSpeed = 0.5f;

        private Camera _cam;
        private Vector3 _dragOrigin;

        private void Awake()
        {
            _cam = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            // Kilitliyse (Not taþýyorsak veya çizgi çekiyorsak) kamera kýmýldamasýn
            if (IsLocked) return;

            HandleMouseInput();
            HandleTouchInput();
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dragOrigin = _cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = _dragOrigin - _cam.ScreenToWorldPoint(Input.mousePosition);
                transform.position += difference;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                float newSize = _cam.orthographicSize - (scroll * zoomSpeed * 5f);
                _cam.orthographicSize = Mathf.Clamp(newSize, zoomMin, zoomMax);
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _dragOrigin = _cam.ScreenToWorldPoint(touch.position);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 currentPos = _cam.ScreenToWorldPoint(touch.position);
                    Vector3 difference = _dragOrigin - currentPos;
                    transform.position += difference;
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                float newSize = _cam.orthographicSize + (deltaMagnitudeDiff * zoomSpeed * 0.01f);
                _cam.orthographicSize = Mathf.Clamp(newSize, zoomMin, zoomMax);
            }
        }
    }
}