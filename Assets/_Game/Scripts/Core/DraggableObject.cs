using UnityEngine;
using TMPro;

namespace FluxPlan.CanvasSystem
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DraggableObject : MonoBehaviour
    {
        private Vector3 _offset;
        private float _zCoord;
        private Vector3 _initialScale;

        private bool _isDragging = false;
        private Vector3 _startClickPos;

        // Statik sayaç: Hangi nota tıklarsan o en öne gelir
        private static int globalSortingOrder = 10;

        private SpriteRenderer _myRenderer;
        private TextMeshPro _myText;

        private void Start()
        {
            _initialScale = transform.localScale;
            _myRenderer = GetComponent<SpriteRenderer>();
            _myText = GetComponentInChildren<TextMeshPro>();
        }

        private void OnMouseDown()
        {
            // 1. Kamerayı Kilitle (Dünya kaymasın)
            CanvasCameraController.IsLocked = true;

            // 2. Öne Getirme (Sorting)
            globalSortingOrder++;
            if (_myRenderer != null) _myRenderer.sortingOrder = globalSortingOrder;
            if (_myText != null) _myText.sortingOrder = globalSortingOrder + 1;

            // 3. Taşıma Hazırlığı
            _zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            _offset = gameObject.transform.position - GetMouseAsWorldPoint();
            _startClickPos = Input.mousePosition;

            transform.localScale = _initialScale * 1.1f;
            _isDragging = false;
        }

        private void OnMouseDrag()
        {
            // Notu taşı
            transform.position = GetMouseAsWorldPoint() + _offset;

            if (Vector3.Distance(_startClickPos, Input.mousePosition) > 10f)
            {
                _isDragging = true;
            }
        }

        private void OnMouseUp()
        {
            // 1. Kamerayı Serbest Bırak
            CanvasCameraController.IsLocked = false;

            transform.localScale = _initialScale;

            // Eğer sürükleme yapmadıysak (Sadece Tıklama ise) -> SADECE EDİTÖRÜ AÇ
            if (!_isDragging)
            {
                // Artık burada "Link Modu" kontrolü yapmıyoruz.
                // Çünkü bağlantıyı notun göbeğinden değil, kenarındaki noktalardan yapıyoruz.

                TextMeshPro textComp = GetComponentInChildren<TextMeshPro>();
                CanvasEditorManager.Instance.OpenEditor(gameObject, textComp);
            }
        }

        private Vector3 GetMouseAsWorldPoint()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _zCoord;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}