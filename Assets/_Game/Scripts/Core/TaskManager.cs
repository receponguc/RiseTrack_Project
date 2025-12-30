using System.Collections.Generic;
using UnityEngine;
using System;
using FluxPlan.Models; // TaskData modelini tanýmasý için
using System.Linq;     // Liste iþlemleri için (Find vb.)

namespace FluxPlan.Core
{
    public class TaskManager : MonoBehaviour
    {
        // Singleton: Her yerden ulaþýlabilmesi için (TaskManager.Instance)
        public static TaskManager Instance { get; private set; }

        [Header("Görev Listesi")]
        [SerializeField] private List<TaskData> allTasks = new List<TaskData>();

        private void Awake()
        {
            // Sahneler arasý geçiþte yok olmasýn
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // 1. Uygulama açýlýnca kayýtlý verileri yükle
            allTasks = SaveManager.LoadTasks();

            // 2. Eðer hiç veri yoksa (Ýlk kurulumsa) örnek bir tane at
            if (allTasks.Count == 0)
            {
                CreateTask("FluxPlan'a Hoþgeldin!", TaskPriority.High);
            }
        }

        // --- GÖREV OLUÞTURMA ---
        public void CreateTask(string title, TaskPriority priority)
        {
            long dateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            TaskData newTask = new TaskData(title, dateTimestamp, priority);

            allTasks.Add(newTask);

            Debug.Log($"Yeni Task Eklendi: {title}");
            Save(); // Deðiþikliði kaydet
        }

        // --- LÝSTELEME ---
        public List<TaskData> GetTasksForDate(DateTime date)
        {
            // Þimdilik tarih fark etmeksizin hepsini gönderiyoruz
            return allTasks;
        }

        // --- DURUM DEÐÝÞTÝRME (CHECK/UNCHECK) ---
        public void ToggleTaskCompletion(string taskId, bool isComplete)
        {
            TaskData task = allTasks.Find(t => t.id == taskId);

            if (task != null)
            {
                task.isCompleted = isComplete;
                Save(); // Durum deðiþti, kaydet
            }
        }

        // --- SÝLME ---
        public void DeleteTask(string taskId)
        {
            TaskData task = allTasks.Find(t => t.id == taskId);

            if (task != null)
            {
                allTasks.Remove(task);
                Save(); // Silindi, kaydet
            }
        }

        // --- KAYDETME YARDIMCISI ---
        private void Save()
        {
            // SaveManager aracýlýðýyla diske yaz
            SaveManager.SaveTasks(allTasks);
        }

        // Uygulama alta atýlýrsa veya kapanýrsa garanti kaydet
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }
    }
}