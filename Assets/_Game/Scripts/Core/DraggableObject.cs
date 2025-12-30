using UnityEngine;
using TMPro; // TextMeshPro'ya ulaþmak için

namespace FluxPlan.CanvasSystem
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DraggableObject : MonoBehaviour
    {
        private Vector3 _offset;
        private float _zCoord;
        private Vector3 _initialScale;

        // Týklama vs Sürükleme ayrýmý için
        private bool _isDragging = false;
        private Vector3 _startClickPos;

        private void Start()
        {
            _initialScale = transform.localScale;
        }

        private void OnMouseDown()
        {
            _zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            _offset = gameObject.transform.position - GetMouseAsWorldPoint();
            _startClickPos = Input.mousePosition; // Ýlk týkladýðým yer

            transform.localScale = _initialScale * 1.1f;
            _isDragging = false;
        }

        private void OnMouseDrag()
        {
            // Eðer mouse biraz bile oynadýysa, bu artýk bir sürüklemedir
            if (Vector3.Distance(_startClickPos, Input.mousePosition) > 10f) // 10 piksel tolerans
            {
                _isDragging = true;
                transform.position = GetMouseAsWorldPoint() + _offset;
            }
        }

        private void OnMouseUp()
        {
            transform.localScale = _initialScale;

            // Eðer hiç sürüklenmediyse, bu bir TIKLAMADIR -> Editörü Aç
            if (!_isDragging)
            {
                // Ýçimdeki TextMeshPro bileþenini bul
                TextMeshPro myText = GetComponentInChildren<TextMeshPro>();

                // Yöneticiye "Beni düzenle" de
                CanvasEditorManager.Instance.OpenEditor(gameObject, myText);
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