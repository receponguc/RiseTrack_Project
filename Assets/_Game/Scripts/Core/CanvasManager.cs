using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;

namespace FluxPlan.CanvasSystem
{
    public class CanvasManager : MonoBehaviour
    {
        public static CanvasManager Instance { get; private set; }

        [Header("Prefablar")]
        [SerializeField] private GameObject stickyNotePrefab;
        [SerializeField] private Transform contentContainer;

        private string saveFilePath;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            saveFilePath = Path.Combine(Application.persistentDataPath, "sticky_notes.json");
        }

        private void Start()
        {
            LoadNotes();
        }

        public void SpawnStickyNote()
        {
            Vector3 spawnPos = Camera.main.transform.position;
            spawnPos.z = 0;
            spawnPos.x += Random.Range(-3f, 3f);
            spawnPos.y += Random.Range(-4f, 4f);

            CreateNoteObject(spawnPos, "Yeni Not");
        }

        private void CreateNoteObject(Vector3 position, string content)
        {
            GameObject newNote = Instantiate(stickyNotePrefab, position, Quaternion.identity);

            if (contentContainer != null)
                newNote.transform.SetParent(contentContainer);

            TextMeshPro textComp = newNote.GetComponentInChildren<TextMeshPro>();
            if (textComp != null)
            {
                textComp.text = content;
            }
        }

        // --- GÜNCELLENEN KISIM BURASI ---
        public void SaveNotes()
        {
            // ESKÝSÝ: FindObjectsOfType<DraggableObject>();
            // YENÝSÝ (Daha Hýzlý): FindObjectsByType... (SortMode.None)

            DraggableObject[] allNotes = FindObjectsByType<DraggableObject>(FindObjectsSortMode.None);

            List<NoteData> dataList = new List<NoteData>();

            foreach (var note in allNotes)
            {
                NoteData data = new NoteData();
                data.position = note.transform.position;

                TextMeshPro textComp = note.GetComponentInChildren<TextMeshPro>();
                data.content = (textComp != null) ? textComp.text : "";

                dataList.Add(data);
            }

            NoteListWrapper wrapper = new NoteListWrapper { notes = dataList };
            string json = JsonUtility.ToJson(wrapper, true);

            File.WriteAllText(saveFilePath, json);
        }
        // --------------------------------

        public void LoadNotes()
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                NoteListWrapper wrapper = JsonUtility.FromJson<NoteListWrapper>(json);

                if (wrapper != null && wrapper.notes != null)
                {
                    // Eski notlarý temizlemeyi buraya ekleyebilirsin (isteðe baðlý)
                    foreach (var noteData in wrapper.notes)
                    {
                        CreateNoteObject(noteData.position, noteData.content);
                    }
                }
            }
        }

        private void OnApplicationQuit()
        {
            SaveNotes();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) SaveNotes();
        }
    }

    [System.Serializable]
    public class NoteData
    {
        public Vector3 position;
        public string content;
    }

    [System.Serializable]
    public class NoteListWrapper
    {
        public List<NoteData> notes;
    }
}