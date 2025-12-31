using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    public class ConnectionManager : MonoBehaviour
    {
        public static ConnectionManager Instance { get; private set; }

        [Header("Prefablar")]
        [SerializeField] private GameObject connectionLinePrefab;
        // Yeni: Ok Ucu Prefabý için kutu
        [SerializeField] private GameObject arrowHeadPrefab;

        // Anlýk Durumlar
        private ConnectionAnchor _startAnchor;
        private ConnectionAnchor _currentHoveredAnchor;
        private GameObject _tempLineObject;
        private LineRenderer _tempLineRenderer;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            // Geçici çizgi (mouse ile çekerken)
            if (_tempLineObject != null && _startAnchor != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                _tempLineRenderer.SetPosition(0, _startAnchor.transform.position);
                _tempLineRenderer.SetPosition(1, mousePos);
            }
        }

        public void StartDrawingLine(ConnectionAnchor startPoint)
        {
            _startAnchor = startPoint;

            // Geçici çizgi oluþtur (Bunda ok ucu olmasýna gerek yok)
            _tempLineObject = Instantiate(connectionLinePrefab);
            _tempLineRenderer = _tempLineObject.GetComponent<LineRenderer>();

            var ctrl = _tempLineObject.GetComponent<ConnectionController>();
            if (ctrl) ctrl.enabled = false;

            _tempLineRenderer.positionCount = 2;
        }

        public void FinishDrawingLine()
        {
            if (_currentHoveredAnchor != null && _currentHoveredAnchor != _startAnchor)
            {
                // BAÐLANTIYI KUR!
                Destroy(_tempLineObject);

                // 1. Kalýcý çizgiyi yarat
                GameObject permanentLine = Instantiate(connectionLinePrefab);

                // 2. Ok Ucunu yarat (Eðer prefab atanmýþsa)
                GameObject arrowObj = null;
                if (arrowHeadPrefab != null)
                {
                    arrowObj = Instantiate(arrowHeadPrefab);
                }

                // 3. Denetleyiciye hepsini teslim et
                ConnectionController controller = permanentLine.GetComponent<ConnectionController>();
                if (controller != null)
                {
                    controller.enabled = true;
                    // Artýk 3 parametre gönderiyoruz: Baþlangýç, Bitiþ ve Ok Objesi
                    controller.SetTargets(_startAnchor.transform, _currentHoveredAnchor.transform, arrowObj);
                }
            }
            else
            {
                // Ýptal
                Destroy(_tempLineObject);
            }

            _startAnchor = null;
            _tempLineObject = null;
            _currentHoveredAnchor = null;
        }

        public void SetCurrentHoveredAnchor(ConnectionAnchor anchor)
        {
            _currentHoveredAnchor = anchor;
        }
    }
}