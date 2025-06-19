# 🎵 Magic Tiles (Simplified) – Unity Developer Test

---

## ▶️ How to Run the Project

### ✅ Requirements
- **Unity 2022.3 LTS** (or newer)
- **DOTween** installed (via Package Manager or Assets)
- Target platform: **PC, Mobile** (Unity Editor)

### 🚀 Steps to Run
1. **Clone** or **download** the project.
2. Open the project using **Unity Hub** or directly in **Unity Editor**.
3. Wait for all assets and packages to load completely.
4. Ensure DOTween is initialized:
  - Go to `Tools > Demigiant > DOTween Utility Panel`
  - Click `Setup DOTween...` if not already done.
5. Click Scenes/Open Scene Browser
  - Open Gameplay Scene
6. Press the **Play** button in the Editor to start the game.


---

## ⚙️ Technical Design Choices
- 
- I used a custom State Pattern to cleanly separate game phases (Menu, Gameplay, Result), making the codebase easier to extend and maintain.
- To ensure precise rhythm alignment, tile movement and scoring logic are synced using `AudioSource.time` instead of relying on frame-based timing, enabling consistent gameplay even under framerate drops.
- I use a generic `GameEvent<T>` system was implemented to allow modular communication between systems without direct references.

---

## 🎯 External Assets Used

### 🧩 Unity Packages
- **DOTween** – by Demigiant  
  https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676 – Used for UI tweening
- **Odin Inspector** by Sirenix
  https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041
- **Violet Theme UI** by Giniel Villacote
  https://assetstore.unity.com/packages/2d/gui/violet-themed-ui-235559
- **UI Particle** by Mob-Sakai
  https://github.com/mob-sakai/UIEffect

### 🧠 External Scripts
- **Object Pool, Singleton, Yielders** by MacacaGames
  https://github.com/MacacaGames/MacacaUtility/tree/master

### 🔤 Fonts
- **Press Start 2P** – by Codeman38  
  https://fonts.google.com/specimen/Bangers – License: SIL OFL – Used for UI
### Music/SFX
- From https://pixabay.com/