using UnityEngine;
using UnityEngine.UI;

namespace FluxPlan.CanvasSystem
{
    public class NoteToggleCompletion : MonoBehaviour
    {
        [Header("Senin Butonların")]
        public GameObject onaysizButton; // Boş/Gri olan
        public GameObject onayliButton;  // Dolu/Yeşil olan

        [Header("Renk Değişimi (İsteğe Bağlı)")]
        public bool rengiDegistir = true;
        public Color tamamlandiRengi = new Color(0.7f, 1f, 0.7f); // Açık Yeşil

        private SpriteRenderer _notKagidi;
        private Color _eskiRenk;
        private bool _isCompleted = false;

        private void Start()
        {
            // Not kağıdının boyasını bul
            _notKagidi = GetComponentInParent<SpriteRenderer>();
            if (_notKagidi) _eskiRenk = _notKagidi.color;

            // Başlangıç: İş yapılmadı, Onaysız butonu açık olsun
            DurumuGuncelle();
        }

        public void Tiklandi()
        {
            // Durumu tersine çevir
            _isCompleted = !_isCompleted;
            DurumuGuncelle();
        }

        private void DurumuGuncelle()
        {
            if (_isCompleted)
            {
                // -- ONAYLANDI --
                onaysizButton.SetActive(false); // Griyi gizle
                onayliButton.SetActive(true);   // Yeşili aç

                if (rengiDegistir && _notKagidi) _notKagidi.color = tamamlandiRengi;
            }
            else
            {
                // -- GERİ ALINDI --
                onaysizButton.SetActive(true);  // Griyi aç
                onayliButton.SetActive(false);  // Yeşili gizle

                if (rengiDegistir && _notKagidi) _notKagidi.color = _eskiRenk;
            }
        }
    }
}