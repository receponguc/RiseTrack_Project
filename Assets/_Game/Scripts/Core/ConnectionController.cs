using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class ConnectionController : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Transform _startTarget;
        private Transform _endTarget;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            // Çizgi köþe sayýsý 2 (Baþ ve Son)
            _lineRenderer.positionCount = 2;
        }

        // Bu fonksiyonu dýþarýdan çaðýrýp hedefleri vereceðiz
        public void SetTargets(Transform start, Transform end)
        {
            _startTarget = start;
            _endTarget = end;
        }

        private void Update()
        {
            // Eðer hedeflerden biri silindiyse çizgiyi de yok et
            if (_startTarget == null || _endTarget == null)
            {
                Destroy(gameObject);
                return;
            }

            // Çizginin uçlarýný sürekli notlarýn pozisyonuna güncelle
            _lineRenderer.SetPosition(0, _startTarget.position);
            _lineRenderer.SetPosition(1, _endTarget.position);
        }
    }
}