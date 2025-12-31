using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ConnectionAnchor : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Color _originalColor;
        public Color hoverColor = Color.cyan; // Üzerine gelince parlasýn

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _originalColor = _renderer.color;
        }

        // Mouse üzerine gelince parlasýn
        private void OnMouseEnter()
        {
            _renderer.color = hoverColor;
            // Yöneticiye haber ver: "Þu an benim üzerimdesin"
            if (ConnectionManager.Instance != null)
                ConnectionManager.Instance.SetCurrentHoveredAnchor(this);
        }

        // Mouse gidince eski rengine dönsün
        private void OnMouseExit()
        {
            _renderer.color = _originalColor;

            if (ConnectionManager.Instance != null)
                ConnectionManager.Instance.SetCurrentHoveredAnchor(null);
        }

        // Týklanýnca çizim baþlasýn
        private void OnMouseDown()
        {
            if (ConnectionManager.Instance != null)
            {
                // Kamerayý kilitle (Sürükleme yaparken dünya kaymasýn)
                CanvasCameraController.IsLocked = true;
                ConnectionManager.Instance.StartDrawingLine(this);
            }
        }

        // Býrakýnca (Eðer baþka bir nokta üzerindeysek) baðlantý bitsin
        private void OnMouseUp()
        {
            CanvasCameraController.IsLocked = false;
            if (ConnectionManager.Instance != null)
            {
                ConnectionManager.Instance.FinishDrawingLine();
            }
        }
    }
}