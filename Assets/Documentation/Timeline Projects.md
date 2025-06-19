# 📆 Unity Developer Test – Magic Tiles Clone Timeline (1.5 ngày)

**🕒 Thời gian làm: Bắt đầu từ 11h30 ngày 17/6 → hết ngày 19/6**  
**🎯 Mục tiêu: Gameplay hoàn chỉnh + Long Tap + Double Tap + Polish**

---

## ✅ Tổng quan giai đoạn

| Giai đoạn                  | Nội dung chính                                            | Ước lượng |
|---------------------------|-----------------------------------------------------------|-----------|
| Ngày 1 – 17/6 (chiều)     | Nghiên cứu rhythm game + core gameplay + generate tiles   | ~6 giờ    |
| Ngày 2 – 18/6 (full)      | Input + Scoring + Visual feedback + Long/Double Tap       | ~7 giờ    |
| Ngày 3 – 19/6 (sáng)      | Clean code, README, GDD (nếu làm), final polish & nộp bài | ~3 giờ    |

---

## 🗓️ Ngày 1 – 17/6 (chiều)

### ⏰ 13h00 – 14h30 (1.5h): 🧠 Nghiên cứu rhythm game và lên kế hoạch
- [x] Xem kỹ lại video demo
- [x] Tìm hiểu Hiểu rõ các loại tile: Single, Long, Double
- [x] Ghi lại logic chấm điểm (Perfect / Good / Miss)
- [x] Xác định cách spawn tile theo beat
- [x] Note các yếu tố "feel" quan trọng
- [x] Xác định cách generate map

### ⏰ 14h30 – 16h30 (2h): 🎮 Setup dự án + Tile cơ bản
- [x] Tạo project Unity 2021.3 LTS
- [x] Setup Canvas, UI tile prefab
- [x] Tile spawn theo nhịp
- [x] Generate beatmap theo bpm
- [x] Tạo cấu trúc data cho nhạc

### ⏰ 16h30 – 19h00 (2.5h): 🎯 Core Gameplay
- [x] Thử nghiệm nhạc tốc độ cao
- [ ] ~~Tap tile + Scoring~~ *(chuyển sang ngày 2)*
- [ ] ~~Game Over logic~~ *(chuyển sang ngày 2)*

---

## 🗓️ Ngày 2 – 18/6 (11h00 - 18h00)

### ⏰ 11h00 – 13h30 (2.5h): 🎯 Input + Scoring + Game Over
- [x] Tap tile → xác định thời điểm (± delta)
- [x] Phân loại Perfect / Good / Miss
- [x] Tính điểm, cập nhật UI
- [x] Miss → Game Over

### ⏰ 13h30 – 15h30 (2h): 💥 Feedback + Combo + Long Tap
- [x] Visual/audio feedback
- [x] Combo logic (multiplier theo streak)
- [ ] **Long Tap Tile**
  - Safe tap từ 50% độ dài
  - Thả tay không tính miss
  - Không tap lại được

### ⏰ 15h45 – 17h45 (2h): 🧪 Double Tap + Polish
- [ ] **Double Tap Tile**:
  - Spawn 2 tile cùng lúc (PairID)
  - Yêu cầu tap cả 2 trong 0.2s
- [ ] Background động (parallax)
- [x] Hiệu ứng combo text
- [x] Kiểm tra performance

### ⏰ 17h45 – 18h00 (0.5h): 🧪 Build test
- [x] Kiểm tra input/stuck error
- [x] Đo FPS & memory
- [x] Đánh giá cảm giác chơi

---

## 🗓️ Ngày 3 – 19/6 (sáng): 🧹 Dọn dẹp và nộp bài

### ⏰ 08h00 – 11h00 (3h): 🧼 Refactor + README + Nộp bài
- [x] Refactor code, chia class
- [x] Cleanup asset & prefab
- [x] Viết README.md:
  - Hướng dẫn chạy game
  - Attribution asset
  - Kiểm tra cuối cùng

---

## 💡 Notes:
- Pooling object cho performance
- `BaseTile` cho các loại tile kế thừa
- Tách input system cho đa nền tảng
- Ưu tiên: Gameplay > Long/Double Tap > Polish