using UnityEngine;
using UnityEngine.UI; // UI elemanlarý için
using System.Collections.Generic;
using FluxPlan.Core;
using FluxPlan.Models;

namespace FluxPlan.UI
{
    public class DashboardController : MonoBehaviour
    {
        [Header("Prefab & Liste")]
        [SerializeField] private TaskItemView taskItemPrefab;
        [SerializeField] private Transform scrollContent;

        [Header("Yeni Task Ekleme UI")]
        [SerializeField] private GameObject addTaskPanel; // Açýp kapatacaðýmýz panel
        [SerializeField] private InputField taskInput;    // Yazýyý alacaðýmýz yer

        private void Start()
        {
            RefreshTaskList();
            // Baþlangýçta paneli zorla kapat (açýk unuttuysak diye)
            if (addTaskPanel != null) addTaskPanel.SetActive(false);
        }

        public void RefreshTaskList()
        {
            // Temizlik
            foreach (Transform child in scrollContent) Destroy(child.gameObject);

            // Veriyi Çek
            List<TaskData> tasks = TaskManager.Instance.GetTasksForDate(System.DateTime.Now);

            // Listele
            foreach (TaskData task in tasks)
            {
                TaskItemView newItem = Instantiate(taskItemPrefab, scrollContent);
                newItem.Setup(task);
            }
        }

        // --- BUTON FONKSÝYONLARI ---

        // 1. "+" butonuna basýnca çalýþacak
        public void OpenAddTaskPanel()
        {
            addTaskPanel.SetActive(true);
            taskInput.text = ""; // Eski yazýyý temizle
        }

        // 2. "Ýptal" butonuna basýnca çalýþacak
        public void CloseAddTaskPanel()
        {
            addTaskPanel.SetActive(false);
        }

        // 3. "Kaydet" butonuna basýnca çalýþacak
        public void SaveNewTask()
        {
            // Boþ yazý girilmesini engelle
            if (string.IsNullOrEmpty(taskInput.text)) return;

            // TaskManager'a yeni görevi gönder
            TaskManager.Instance.CreateTask(taskInput.text, TaskPriority.Medium);

            // Listeyi yenile ki yeni gelen görünsün
            RefreshTaskList();

            // Paneli kapat
            CloseAddTaskPanel();
        }
    }
}