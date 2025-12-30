using UnityEngine;
using UnityEngine.UI; // Text ve Toggle için gerekli
using FluxPlan.Models; // TaskData'yý tanimasi için
using System; // Tarih iþlemleri için

namespace FluxPlan.UI
{
    public class TaskItemView : MonoBehaviour
    {
        [Header("UI Baðlantýlarý")]
        [SerializeField] private Text titleText;
        [SerializeField] private Text dateText;
        [SerializeField] private Toggle completeToggle;

        private TaskData _currentData; // Þu an gösterdiðim veriyi aklýmda tutayým

        // Bu fonksiyonu dýþarýdan çaðýrýp veriyi vereceðiz
        public void Setup(TaskData data)
        {
            _currentData = data;

            // 1. Baþlýðý ayarla
            titleText.text = data.title;

            // 2. Checkbox durumunu ayarla
            completeToggle.isOn = data.isCompleted;

            // 3. Tarihi okunabilir formata çevir (Unix Timestamp -> String)
            DateTime date = DateTimeOffset.FromUnixTimeSeconds(data.scheduledDate).DateTime.ToLocalTime();
            dateText.text = date.ToString("dd MMM, ddd"); // Örn: 30 Ara, Sal

            // Eðer tamamlandýysa üzerini çizmeyi daha sonra ekleyeceðiz (Görsel cila)
        }
    }
}