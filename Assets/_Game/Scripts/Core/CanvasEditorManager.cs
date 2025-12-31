using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro kullanýyoruz

namespace FluxPlan.CanvasSystem
{
    public class CanvasEditorManager : MonoBehaviour
    {
        public static CanvasEditorManager Instance { get; private set; }

        [Header("UI Elemanlarý")]
        [SerializeField] private GameObject editPanel;
        [SerializeField] private InputField inputField; // Eðer TMP Input kullanýyorsan 'TMP_InputField' yap

        private TextMeshPro _currentSelectedText; // Þu an düzenlediðimiz notun yazýsý
        private GameObject _currentSelectedNote;  // Þu an düzenlediðimiz notun kendisi (silmek için)

        private void Awake()
        {
            Instance = this;
            editPanel.SetActive(false);
        }

        // 1. Not tarafýndan çaðrýlan fonksiyon (Pencereyi Aç)
        public void OpenEditor(GameObject noteObj, TextMeshPro textComponent)
        {
            _currentSelectedNote = noteObj;
            _currentSelectedText = textComponent;

            // Mevcut yazýyý Input Field'a kopyala
            if (textComponent != null)
            {
                inputField.text = textComponent.text;
            }

            // Paneli aç
            editPanel.SetActive(true);
        }

        // 2. Kaydet Butonuna baðlanacak
        public void SaveAndClose()
        {
            if (_currentSelectedText != null)
            {
                // Yeni yazýyý nota geri gönder
                _currentSelectedText.text = inputField.text;

                // --- YENÝ EKLENEN: DÝSKE KAYDET ---
                // CanvasManager'a seslenip "Dosyayý güncelle" diyoruz.
                if (CanvasManager.Instance != null)
                {
                    CanvasManager.Instance.SaveNotes();
                }
                // ----------------------------------
            }
            editPanel.SetActive(false);
        }

        // 3. Sil Butonuna baðlanacak
        public void DeleteAndClose()
        {
            if (_currentSelectedNote != null)
            {
                Destroy(_currentSelectedNote);

                // --- YENÝ EKLENEN: SÝLÝNDÝÐÝNÝ KAYDET ---
                // Notun sahneden silinmesi kare sonunu bulur. 
                // O yüzden kaydetmeyi 0.1 saniye gecikmeli yapýyoruz ki silinmiþ halini kaydetsin.
                Invoke(nameof(TriggerSave), 0.1f);
            }
            editPanel.SetActive(false);
        }

        // Gecikmeli kayýt için yardýmcý fonksiyon
        private void TriggerSave()
        {
            if (CanvasManager.Instance != null)
            {
                CanvasManager.Instance.SaveNotes();
            }
        }

        // 4. Ýptal/Boþluða/X'e týklama
        public void ClosePanel()
        {
            editPanel.SetActive(false);
        }
    }
}