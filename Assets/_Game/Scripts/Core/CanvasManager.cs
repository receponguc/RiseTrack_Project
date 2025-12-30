using UnityEngine;

namespace FluxPlan.CanvasSystem
{
    public class CanvasManager : MonoBehaviour
    {
        [Header("Prefablar")]
        [SerializeField] private GameObject stickyNotePrefab; // Sarý not prefabý

        // ÝÞTE BUTONUN ARADIÐI FONKSÝYON BU:
        public void SpawnStickyNote()
        {
            Vector3 spawnPos = Camera.main.transform.position;
            spawnPos.z = 0;

            // Rastgelelik (Üst üste binmesin)
            spawnPos.x += Random.Range(-1f, 1f);
            spawnPos.y += Random.Range(-1f, 1f);

            Instantiate(stickyNotePrefab, spawnPos, Quaternion.identity);
        }
    }
}