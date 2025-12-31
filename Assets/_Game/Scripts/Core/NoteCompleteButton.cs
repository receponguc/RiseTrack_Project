using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    public class NoteCompleteButton : MonoBehaviour
    {
        [Header("Ayarlar")]
        public Color completedColor = new Color(0.5f, 1f, 0.5f); // Açık Yeşil (Tamamlandı Rengi)

        private SpriteRenderer _noteRenderer; // Notun kendi boyası
        private Color _originalColor; // Notun orijinal rengi (Sarı)
        private bool _isCompleted = false;

        private void Start()
        {
            // 1. Üstteki Ana Notu (Parent) bul
            // Bu buton notun içinde olduğu için 'GetComponentInParent' ile babasını buluyoruz.
            _noteRenderer = GetComponentInParent<SpriteRenderer>();

            if (_noteRenderer != null)
            {
                // Başlangıç rengini hafızaya at (Geri dönmek gerekirse diye)
                _originalColor = _noteRenderer.color;
            }
        }

        private void OnMouseDown()
        {
            if (_noteRenderer == null) return;

            // Durumu Tersiyle Değiştir (Tamamlandı <-> Tamamlanmadı)
            _isCompleted = !_isCompleted;

            if (_isCompleted)
            {
                // İş bitti: Notu YEŞİL yap
                _noteRenderer.color = completedColor;
                Debug.Log("Görev Tamamlandı! ✅");
            }
            else
            {
                // Vazgeçildi: Notu ESKİ RENGİNE döndür
                _noteRenderer.color = _originalColor;
                Debug.Log("Görev Geri Alındı. ue");
            }
        }
    }
}