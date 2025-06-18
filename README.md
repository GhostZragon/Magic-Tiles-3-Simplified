# 🎵 Magic Tiles (Simplified) – Unity Developer Test

---

## ▶️ How to Run the Project


Controls:

---

## ⚙️ Technical Design Choices

- Built a lightweight custom editor to serialize rhythm chart data into JSON files, enabling quick iteration, easy debugging, and scalable level management. Enable for player create own custom rhythm charts
- Chose Unity's UI Canvas system to take advantage of flexible layout tools, responsive scaling, and component-based design — ensuring visual consistency across devices and resolutions.
- Synced gameplay timing using `AudioSource.Time` for frame-independent rhythm accuracy, and realtime synchronization with player when lose, setup for replay feature.

---

## 🎯 External Assets Used

### 🧩 Unity Packages
- **DOTween** – by Demigiant  
  https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676 – Used for UI tweening
- **Odin Inspector** by Sirenix
  https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041
- **Violet Theme UI** by Giniel Villacote
  https://assetstore.unity.com/packages/2d/gui/violet-themed-ui-235559

### 🧠 External Scripts
- **Object Pool, Singleton, Yielders** by MacacaGames
  https://github.com/MacacaGames/MacacaUtility/tree/master

### 🔤 Fonts
- **Press Start 2P** – by Codeman38  
  https://fonts.google.com/specimen/Bangers – License: SIL OFL – Used for UI
