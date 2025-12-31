using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class ConnectionController : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Transform _startTarget;
        private Transform _endTarget;

        // Yeni: Ok Ucu objesi
        private GameObject _arrowHeadObject;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            // Çizgi ayarlarý
            _lineRenderer.positionCount = 2;
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.useWorldSpace = true;
            _lineRenderer.sortingOrder = 50; // Çizgi notlarýn önünde olsun
        }

        // Hedefleri ve Ok Ucunu dýþarýdan alýyoruz
        public void SetTargets(Transform start, Transform end, GameObject arrowHead)
        {
            _startTarget = start;
            _endTarget = end;
            _arrowHeadObject = arrowHead;
        }

        private void Update()
        {
            // Hedeflerden biri silindiyse (Not silindiyse) çizgiyi ve oku da yok et
            if (_startTarget == null || _endTarget == null)
            {
                if (_arrowHeadObject != null) Destroy(_arrowHeadObject);
                Destroy(gameObject);
                return;
            }

            // 1. Çizginin pozisyonlarýný güncelle
            Vector3 startPos = _startTarget.position;
            Vector3 endPos = _endTarget.position;

            // Z pozisyonlarýný sýfýrla ki 2D düzlemde kalsýnlar
            startPos.z = 0;
            endPos.z = 0;

            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, endPos);

            // 2. Ok Ucunu güncelle
            if (_arrowHeadObject != null)
            {
                UpdateArrowHead(startPos, endPos);
            }
        }

        // Okun pozisyonunu ve dönüþünü hesaplayan matematik
        private void UpdateArrowHead(Vector3 start, Vector3 end)
        {
            // Pozisyon: Ok tam olarak bitiþ noktasýnda dursun
            _arrowHeadObject.transform.position = end;

            // Yön Hesaplama: Baþlangýçtan bitiþe giden vektör
            Vector3 direction = end - start;

            // Açýyý bul (Atan2 fonksiyonu bu iþin ustasýdýr)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Oku bu açýya göre döndür
            // (Eðer ok görselin saða bakýyorsa bu çalýþýr. Yukarý bakýyorsa 'angle - 90' yapman gerekebilir)
            _arrowHeadObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}