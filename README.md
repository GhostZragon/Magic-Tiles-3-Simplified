# 🎵 Magic Tiles (Simplified) – Unity Developer Test

---

## ▶️ How to Run the Project

### ✅ Requirements
- **Unity 2022.3 LTS** (or newer)
- **DOTween** installed (via Package Manager or Assets)
- Target platform: **PC, Mobile** (Unity Editor)

### 🚀 Steps to Run
1. **Clone** or **download** the repository.
2. Open the project using **Unity Hub** or directly with the **Unity Editor**.
3. Wait for all assets and packages to load.
4. **Initialize DOTween**:
  - Go to `Tools > Demigiant > DOTween Utility Panel`
  - Click `Setup DOTween...` if not already done.
5. Open the gameplay scene:
  - Click `Scenes > Open Scene Browser`
  - Select `GameplayScene`
6. **Adjust resolution**:
  - In Game View, set resolution to `Portrait`
  - If testing mobile UI, choose a preset like `iPhone 12` from the dropdown
7. Hit the **Play** button to run the game!

---

## 🧠 Design Choices

- **Custom Beatmap Editor**  
  I built a lightweight in-Unity editor to generate beatmaps as JSON files. It allows for easy modification and extension, supporting long-term scalability for new levels or songs.

- **Clean State Management**  
  I implemented a custom State Pattern to clearly separate game phases (Menu, Gameplay, Result). This improves code organization and makes future features easier to integrate.

- **Frame-Rate Independent Timing**  
  Tile movement and scoring are synced to `AudioSource.time` rather than frame-based timing. This ensures precise rhythm alignment and consistent gameplay even during frame drops.

- **Rhythm-Based Scoring System**  
  Player input is evaluated by comparing the tile’s expected beat time with `AudioSource.time` at the moment of interaction. Based on this delta, scores like *Perfect*, *Good*, or *Miss* are awarded. This system ensures accuracy is rewarded in a musically meaningful way.

- **Decoupled Communication via Game Events**  
  A generic `GameEvent<T>` system enables modular, event-driven communication between systems without tight coupling. This keeps the architecture flexible and maintainable.

- **Enhanced Gamefeel**  
  To improve player satisfaction, I added responsive feedback when interacting with tiles:
  - **Audio**: A clap "click" sound is played on successful tile hits, enhance rhythm immersion.
  - **Visual**: Scaling animations emphasize hit confirmation.



---

## 🎯 External Assets Used

### 🧩 Unity Packages
- **[DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)** by Demigiant  
  Used for tweening animations in UI and gameplay elements.

- **[Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)** by Sirenix  
  Used to enhance editor workflows and improve inspector customization.

- **[Violet Theme UI](https://assetstore.unity.com/packages/2d/gui/violet-themed-ui-235559)** by Giniel Villacote  
  Used for UI visuals and button assets.

- **[UI Particle](https://github.com/mob-sakai/UIEffect)** by Mob-Sakai  
  Used for implementing particle effects in UI elements (e.g., hit feedback).

### 🧠 External Scripts
- **[MacacaUtility](https://github.com/MacacaGames/MacacaUtility)** by MacacaGames  
  Used object pooling, singleton base class, and coroutine/yield helpers.

### 🔤 Fonts
- **[Press Start 2P](https://fonts.google.com/specimen/Press+Start+2P)** by Codeman38  
  License: SIL Open Font License (OFL) – Used in UI text elements.

### 🔊 Music & Sound Effects
- **From [Pixabay](https://pixabay.com/)** – royalty-free sounds and music used under Pixabay license.
