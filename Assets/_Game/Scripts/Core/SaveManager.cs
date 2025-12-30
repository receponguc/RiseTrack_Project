using UnityEngine;
using System.IO; // Dosya iþlemleri için gerekli
using FluxPlan.Models;
using System.Collections.Generic;

namespace FluxPlan.Core
{
    public static class SaveManager
    {
        // Dosyanýn adý
        private static string fileName = "fluxdata.json";

        // Kaydetme Fonksiyonu
        public static void SaveTasks(List<TaskData> tasksToSave)
        {
            // 1. Veriyi kutuya koy
            SaveData data = new SaveData(tasksToSave);

            // 2. Kutuyu metne (JSON) çevir
            string json = JsonUtility.ToJson(data, true); // 'true' okunabilir format yapar

            // 3. Dosya yolunu belirle (Android/iOS/PC uyumlu yol)
            string path = Path.Combine(Application.persistentDataPath, fileName);

            // 4. Yaz
            File.WriteAllText(path, json);

            Debug.Log($"<color=cyan>KAYDEDÝLDÝ:</color> {path}");
        }

        // Yükleme Fonksiyonu
        public static List<TaskData> LoadTasks()
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);

            // Eðer dosya varsa oku
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                Debug.Log($"<color=green>YÜKLENDÝ:</color> {data.savedTasks.Count} görev geldi.");
                return data.savedTasks;
            }

            // Dosya yoksa boþ liste döndür (Ýlk açýlýþ)
            return new List<TaskData>();
        }
    }
}