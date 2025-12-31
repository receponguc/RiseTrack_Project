using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    public class ConnectionManager : MonoBehaviour
    {
        public static ConnectionManager Instance { get; private set; }

        [Header("Prefablar")]
        [SerializeField] private GameObject connectionLinePrefab;

        // Anlýk Durumlar
        private ConnectionAnchor _startAnchor; // Çizgiye baþladýðýmýz nokta
        private ConnectionAnchor _currentHoveredAnchor; // Þu an mouse'un üzerindeki nokta
        private GameObject _tempLineObject; // O an çizilen hayalet çizgi
        private LineRenderer _tempLineRenderer;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            // Eðer þu an bir çizgi çekiyorsak, ucunu mouse'a yapýþtýr
            if (_tempLineObject != null && _startAnchor != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                _tempLineRenderer.SetPosition(0, _startAnchor.transform.position);
                _tempLineRenderer.SetPosition(1, mousePos);
            }
        }

        // 1. Noktaya týklandýðýnda (ConnectionAnchor çaðýrýr)
        public void StartDrawingLine(ConnectionAnchor startPoint)
        {
            _startAnchor = startPoint;

            // Geçici bir çizgi oluþtur
            _tempLineObject = Instantiate(connectionLinePrefab);
            _tempLineRenderer = _tempLineObject.GetComponent<LineRenderer>();

            // Eðer ConnectionController varsa þimdilik devre dýþý býrak, elle yönetiyoruz
            var ctrl = _tempLineObject.GetComponent<ConnectionController>();
            if (ctrl) ctrl.enabled = false;

            _tempLineRenderer.positionCount = 2;
        }

        // 2. Mouse býrakýldýðýnda (ConnectionAnchor çaðýrýr - ÝÞTE ARANAN FONKSÝYON)
        public void FinishDrawingLine()
        {
            // Eðer geçerli bir bitiþ noktasýnýn üzerindeysek VE baþladýðýmýz yer deðilse
            if (_currentHoveredAnchor != null && _currentHoveredAnchor != _startAnchor)
            {
                // BAÐLANTIYI KUR!
                // 1. Geçici çizgiyi yok et
                Destroy(_tempLineObject);

                // 2. Kalýcý çizgiyi yarat
                GameObject permanentLine = Instantiate(connectionLinePrefab);
                ConnectionController controller = permanentLine.GetComponent<ConnectionController>();

                // 3. Hedefleri ver (Sürekli takip etsinler)
                if (controller != null)
                {
                    controller.enabled = true; // Takip scriptini aç
                    controller.SetTargets(_startAnchor.transform, _currentHoveredAnchor.transform);
                }
            }
            else
            {
                // Boþluða býrakýldýysa iptal et
                Destroy(_tempLineObject);
            }

            // Temizlik
            _startAnchor = null;
            _tempLineObject = null;
        }

        // Anchor scriptleri buraya haber verir: "Mouse þu an benim üzerimde"
        public void SetCurrentHoveredAnchor(ConnectionAnchor anchor)
        {
            _currentHoveredAnchor = anchor;
        }
    }
}