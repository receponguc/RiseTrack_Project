using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    public class CanvasCameraController : MonoBehaviour
    {
        [Header("Ayarlar")]
        [SerializeField] private float zoomMin = 2f;  // En yakýna ne kadar girsin?
        [SerializeField] private float zoomMax = 15f; // En uzaða ne kadar çýksýn?
        [SerializeField] private float zoomSpeed = 0.5f; // Zoom hassasiyeti

        private Camera _cam;
        private Vector3 _dragOrigin; // Sürüklemeye baþladýðým ilk nokta

        private void Awake()
        {
            _cam = GetComponent<Camera>();
        }

        private void Update()
        {
            HandleMouseInput(); // Editörde test için
            HandleTouchInput(); // Mobilde gerçek kullaným için
        }

        // --- BÝLGÝSAYAR (MOUSE) KONTROLLERÝ ---
        private void HandleMouseInput()
        {
            // 1. PAN (Kaydýrma) - Orta Tuþ veya Sol Týk ile
            if (Input.GetMouseButtonDown(0))
            {
                // Týkladýðým yerin dünya koordinatýný al
                _dragOrigin = _cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = _dragOrigin - _cam.ScreenToWorldPoint(Input.mousePosition);

                // Fark kadar kamerayý hareket ettir
                transform.position += difference;
            }

            // 2. ZOOM (Tekerlek)
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                float newSize = _cam.orthographicSize - (scroll * zoomSpeed * 5f);
                _cam.orthographicSize = Mathf.Clamp(newSize, zoomMin, zoomMax);
            }
        }

        // --- MOBÝL (DOKUNMATÝK) KONTROLLERÝ ---
        private void HandleTouchInput()
        {
            if (Input.touchCount == 1)
            {
                // Tek parmak = PAN (Kaydýrma)
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

                    // Not: DragOrigin'i güncellemek gerekebilir, basit modda býrakýyoruz
                }
            }
            else if (Input.touchCount == 2)
            {
                // Ýki parmak = ZOOM (Pinch)
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Bir önceki karedeki pozisyonlarý
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Parmaklarýn önceki ve þimdiki açýklýðý (mesafesi)
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Farký bul (Yaklaþtý mý uzaklaþtý mý?)
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // Kameranýn boyutunu güncelle
                float newSize = _cam.orthographicSize + (deltaMagnitudeDiff * zoomSpeed * 0.01f);
                _cam.orthographicSize = Mathf.Clamp(newSize, zoomMin, zoomMax);
            }
        }
    }
}