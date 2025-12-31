# RiseTrack_Project
# 📘 FluxPlan - Unity Üretkenlik Uygulaması

**FluxPlan**, yapılandırılmış görev yönetimini (Dashboard) serbest biçimli yaratıcı çalışma alanıyla (Sonsuz Canvas) birleştiren hibrit bir Unity mobil uygulamasıdır. Hem dokunmatik ekranlar (Android/iOS) hem de masaüstü editör kullanımı için optimize edilmiştir.

## 🚀 Özellikler

### 1. Görev Panosu (Dashboard)
Klasik "To-Do List" yapısı.
* **Görev Yönetimi:** Başlık ve öncelik seviyesi ile görev ekleme.
* **Kalıcılık (Persistence):** JSON tabanlı kayıt sistemi. Uygulama kapatılsa bile veriler cihazda saklanır (`fluxdata.json`).
* **Etkileşim:**
    * ✅ **Check:** Görev tamamlandığında üzeri çizilir ve rengi solar.
    * 🗑️ **Silme:** Görevler listeden kalıcı olarak silinebilir.
* **Dinamik UI:** Scroll View içerisinde otomatik genişleyen liste yapısı.

### 2. Sonsuz Canvas (Whiteboard)
Miro veya Freeform benzeri serbest çalışma alanı.
* **Kamera Kontrolü:**
    * 🖐 **Pan:** Tek parmakla veya Mouse sol tık ile sonsuz kaydırma.
    * 🔍 **Zoom:** İki parmakla (Pinch) veya Mouse tekerleği ile yakınlaşma/uzaklaşma.
* **Yapışkan Notlar (Sticky Notes):**
    * **Oluşturma (Spawn):** "+" butonu ile ekrana rastgele konumlarda sarı notlar fırlatma.
    * **Sürükle-Bırak:** Notları tutup istenilen yere taşıma.
    * **Düzenleme (Edit):** Nota kısa tıklayarak (Tap) düzenleme penceresini açma ve içeriği değiştirme.
    * **Akıllı Metin:** `TextMeshPro` entegrasyonu ile kutuya sığan dinamik metinler.

---

## 📂 Proje Mimarisi

Proje `Assets/_Game/` dizini altında modüler bir yapı kullanır:

_Game/ ├── Scripts/ │ ├── Core/ # Temel Mantık ve Yöneticiler │ │ ├── TaskManager.cs # Görev verilerini yönetir (Singleton) │ │ ├── SaveManager.cs # JSON Okuma/Yazma işlemleri │ │ ├── CanvasManager.cs # Not oluşturma (Spawn) mantığı │ │ ├── CanvasEditorManager.cs # Not düzenleme (Popup) mantığı │ │ ├── CanvasCameraController.cs # Pan & Zoom kamera kontrolü │ │ └── DraggableObject.cs # Sürükleme ve Tıklama ayrımını yapan script │ ├── UI/ # Arayüz Kontrolcüleri │ │ ├── DashboardController.cs # Liste görünümünü günceller │ │ └── TaskItemView.cs # Tekil görev prefabının kontrolü │ └── Models/ # Veri Yapıları │ ├── TaskData.cs # Görev verisi şablonu │ └── SaveData.cs # JSON serileştirme taşıyıcısı ├── Prefabs/ │ ├── TaskItemTemplate.prefab # Liste elemanı tasarımı │ └── StickyNote.prefab # Sarı not kağıdı (World Space Canvas & TMP) ├── Scenes/ │ ├── 01_Dashboard.unity # Liste Sahnesi │ └── 02_Canvas.unity # Sonsuz Canvas Sahnesi

---

## 🔧 Teknik Gereksinimler ve Kurulum

1.  **Unity Sürümü:** 2021.3 LTS veya üzeri önerilir.
2.  **Paketler:**
    * **TextMeshPro:** (Window > TextMeshPro > Import TMP Essentials).
3.  **Player Settings (Önemli):**
    * Input sistemi hibrit çalışmaktadır.
    * `Project Settings > Player > Other Settings > Configuration` altında **Active Input Handling** seçeneği **`Both`** olarak ayarlanmalıdır.

---

## 📝 Roadmap (Yapılacaklar Listesi)

Proje şu an "Geliştirme Aşamasında" (WIP) olup, aşağıdaki eksikliklerin giderilmesi planlanmaktadır:

- [ ] **Canvas UI Hatası:** Sahne geçişlerinde veya kod derlemelerinde `UI_Overlay` buton referanslarının kaybolması sorunu (DontDestroyOnLoad veya Singleton yapısı ile çözülecek).
- [ ] **Canvas Kayıt Sistemi:** Şu an sadece Dashboard verileri kaydediliyor. Canvas üzerindeki notların pozisyonları ve içerikleri henüz JSON sistemine dahil edilmedi.
- [ ] **Sahne Geçişi:** İki sahne arasında (Dashboard <-> Canvas) geçiş yapacak UI butonları eklenecek.
- [ ] **Renk Seçenekleri:** Notlar için sarı dışında renk paleti eklenecek.

---

## 🎮 Kontroller

| Eylem | Fare (PC) | Dokunmatik (Mobil) |
| :--- | :--- | :--- |
| **Gezinme** | Sol Tık + Sürükle | Tek Parmak Sürükle |
| **Zoom** | Mouse Tekerleği | İki Parmak (Kıstır/Aç) |
| **Not Taşıma** | Notun üzerine basılı tut + Sürükle | Parmağını basılı tut + Sürükle |
| **Not Düzenle** | Nota kısa tıkla | Nota dokun (Tap) |

---

*Son Güncelleme: 30 Aralık 2025*


# RiseTrack_Project
# 📘 FluxPlan - Unity Üretkenlik Uygulaması

**FluxPlan**, yapılandırılmış görev yönetimini (Dashboard) serbest biçimli yaratıcı çalışma alanıyla (Sonsuz Canvas) birleştiren hibrit bir Unity mobil uygulamasıdır. Hem dokunmatik ekranlar (Android/iOS) hem de masaüstü editör kullanımı için optimize edilmiştir.

## 🚀 Özellikler

### 1. Görev Panosu (Dashboard)
Klasik "To-Do List" yapısı.
* **Görev Yönetimi:** Başlık ve öncelik seviyesi ile görev ekleme.
* **Kalıcılık (Persistence):** JSON tabanlı kayıt sistemi. Uygulama kapatılsa bile veriler cihazda saklanır (`fluxdata.json`).
* **Etkileşim:**
    * ✅ **Check:** Görev tamamlandığında üzeri çizilir ve rengi solar.
    * 🗑️ **Silme:** Görevler listeden kalıcı olarak silinebilir.
* **Dinamik UI:** Scroll View içerisinde otomatik genişleyen liste yapısı.

### 2. Sonsuz Canvas (Whiteboard)
Miro veya Freeform benzeri serbest çalışma alanı.
* **Kamera Kontrolü:**
    * 🖐 **Pan:** Tek parmakla veya Mouse sol tık ile sonsuz kaydırma.
    * 🔍 **Zoom:** İki parmakla (Pinch) veya Mouse tekerleği ile yakınlaşma/uzaklaşma.
* **Yapışkan Notlar (Sticky Notes):**
    * **Oluşturma (Spawn):** "+" butonu ile ekrana rastgele konumlarda sarı notlar fırlatma.
    * **Sürükle-Bırak:** Notları tutup istenilen yere taşıma.
    * **Düzenleme (Edit):** Nota kısa tıklayarak (Tap) düzenleme penceresini açma ve içeriği değiştirme.
    * **Akıllı Metin:** `TextMeshPro` entegrasyonu ile kutuya sığan dinamik metinler.

---

## 📂 Proje Mimarisi

Proje `Assets/_Game/` dizini altında modüler bir yapı kullanır:

_Game/ ├── Scripts/ │ ├── Core/ # Temel Mantık ve Yöneticiler │ │ ├── TaskManager.cs # Görev verilerini yönetir (Singleton) │ │ ├── SaveManager.cs # JSON Okuma/Yazma işlemleri │ │ ├── CanvasManager.cs # Not oluşturma (Spawn) mantığı │ │ ├── CanvasEditorManager.cs # Not düzenleme (Popup) mantığı │ │ ├── CanvasCameraController.cs # Pan & Zoom kamera kontrolü │ │ └── DraggableObject.cs # Sürükleme ve Tıklama ayrımını yapan script │ ├── UI/ # Arayüz Kontrolcüleri │ │ ├── DashboardController.cs # Liste görünümünü günceller │ │ └── TaskItemView.cs # Tekil görev prefabının kontrolü │ └── Models/ # Veri Yapıları │ ├── TaskData.cs # Görev verisi şablonu │ └── SaveData.cs # JSON serileştirme taşıyıcısı ├── Prefabs/ │ ├── TaskItemTemplate.prefab # Liste elemanı tasarımı │ └── StickyNote.prefab # Sarı not kağıdı (World Space Canvas & TMP) ├── Scenes/ │ ├── 01_Dashboard.unity # Liste Sahnesi │ └── 02_Canvas.unity # Sonsuz Canvas Sahnesi


---

## 🔧 Teknik Gereksinimler ve Kurulum

1.  **Unity Sürümü:** 2021.3 LTS veya üzeri önerilir.
2.  **Paketler:**
    * **TextMeshPro:** (Window > TextMeshPro > Import TMP Essentials).
3.  **Player Settings (Önemli):**
    * Input sistemi hibrit çalışmaktadır.
    * `Project Settings > Player > Other Settings > Configuration` altında **Active Input Handling** seçeneği **`Both`** olarak ayarlanmalıdır.

---

## 📝 Roadmap (Yapılacaklar Listesi)

Proje şu an "Geliştirme Aşamasında" (WIP) olup, aşağıdaki eksikliklerin giderilmesi planlanmaktadır:

- [ ] **Canvas UI Hatası:** Sahne geçişlerinde veya kod derlemelerinde `UI_Overlay` buton referanslarının kaybolması sorunu (DontDestroyOnLoad veya Singleton yapısı ile çözülecek).
- [ ] **Canvas Kayıt Sistemi:** Şu an sadece Dashboard verileri kaydediliyor. Canvas üzerindeki notların pozisyonları ve içerikleri henüz JSON sistemine dahil edilmedi.
- [ ] **Sahne Geçişi:** İki sahne arasında (Dashboard <-> Canvas) geçiş yapacak UI butonları eklenecek.
- [ ] **Renk Seçenekleri:** Notlar için sarı dışında renk paleti eklenecek.

---

## 🎮 Kontroller

| Eylem | Fare (PC) | Dokunmatik (Mobil) |
| :--- | :--- | :--- |
| **Gezinme** | Sol Tık + Sürükle | Tek Parmak Sürükle |
| **Zoom** | Mouse Tekerleği | İki Parmak (Kıstır/Aç) |
| **Not Taşıma** | Notun üzerine basılı tut + Sürükle | Parmağını basılı tut + Sürükle |
| **Not Düzenle** | Nota kısa tıkla | Nota dokun (Tap) |

---

*Son Güncelleme: 30 Aralık 2025*



