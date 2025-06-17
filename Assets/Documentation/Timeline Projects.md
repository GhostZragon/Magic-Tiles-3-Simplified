# 📆 Unity Developer Test – Magic Tiles Clone Timeline (1.5 ngày)

**🕒 Thời gian làm: Bắt đầu từ 11h30 ngày 17/6 → hết ngày 19/6**  
**🎯 Mục tiêu: Gameplay hoàn chỉnh + Long Tap + Double Tap + Polish**

---

## ✅ Tổng quan giai đoạn

| Giai đoạn                  | Nội dung chính                                            | Ước lượng |
|---------------------------|-----------------------------------------------------------|-----------|
| Ngày 1 – 17/6 (chiều)     | Nghiên cứu rhythm game + core gameplay + generate tiles   | ~6 giờ    |
| Ngày 2 – 18/6 (full)      | Visual feedback, combo, long tap, double tap, polish      | ~7.5 giờ  |
| Ngày 3 – 19/6 (sáng)      | Clean code, README, GDD (nếu làm), final polish & nộp bài | ~3 giờ    |

---

## 🗓️ Ngày 1 – 17/6 (chiều)

### ⏰ 13h00 – 14h30 (1.5h): 🧠 Nghiên cứu rhythm game và lên kế hoạch
- [x] Xem kỹ lại video demo
- [x] Tìm hiểu Hiểu rõ các loại tile: Single, Long, Double
  - Single tile: Có thể miss nếu click vào hàng khác
  - Long tile: 
    - Thêm safe tap từ một nữa hoặc độ dài nhất định cho player
    - Nếu tap một phần và thả thì không tính là miss và không dẫn đến thua game
    - Không tap lại được nếu thả ra
- [ ] Ghi lại logic chấm điểm (Perfect / Good / Miss)
- [ ] Xác định cách spawn tile theo beat (dùng coroutine hay map)
- [ ] Note các yếu tố "feel" quan trọng: độ trễ, animation, UX
- [ ] Xác định cách generate map để spawn tile

### ⏰ 14h30 – 16h30 (2h): 🎮 Setup dự án + Tile cơ bản
- [ ] Tạo project Unity 2021.3 LTS
- [ ] Setup Canvas, UI tile prefab
- [ ] Tile spawn theo nhịp (giả lập beat bằng Coroutine)

### ⏰ 16h30 – 19h00 (2.5h): 🎯 Input + Scoring + Game Over
- [ ] Tap tile → xác định thời điểm (± delta)
- [ ] Phân loại Perfect / Good / Miss
- [ ] Tính điểm, cập nhật UI
- [ ] Miss → Game Over

---

## 🗓️ Ngày 2 – 18/6 (Full ngày phát triển nâng cao)

### ⏰ 08h00 – 12h00 (4h): 💥 Feedback + Combo + Long Tap
- [ ] Visual/audio feedback
- [ ] Combo logic (multiplier theo streak)
- [ ] **Double Tap**
- [ ] **Long Tap Tile**
- [ ] Build game
  - [ ] Kiểm tra input có hoạt động đúng hay có bị stuck/error không
  - [ ] Kiểm tra performance có bị sụt giảm hay không
  - [ ] Kiểm cảm giác chơi để cải thiện ở giai đoạn polish

### ⏰ 13h30 – 17h00 (3.5h): 🧪 Double Tap + UX Polish
- [ ] **Double Tap Tile**:
  - Spawn 2 tile cùng lúc (có PairID)
  - Phải tap cả 2 trong ~0.2s
- [ ] UI/UX nâng cao: combo text, hiệu ứng
- [ ] Background động (parallax, hiệu ứng nhẹ)


---

## 🗓️ Ngày 3 – 19/6 (sáng): 🧹 Dọn dẹp và nộp bài

### ⏰ 08h00 – 11h00 (3h): 🧼 Refactor + README + Nộp bài
- [ ] Refactor code, chia class, comment đầy đủ
- [ ] Cleanup prefab, asset, script
- [ ] Viết README.md:
  - Cách chạy game
  - Kiểm tra Attribution asset


---

## 💡 Notes:

- Object pooling sẽ được sử dụng để đảm bảo performance
- Có thể dùng `ScriptableObject` hoặc JSON để định nghĩa BeatMap thủ công (nếu có thời gian)
- Mọi prefab nên kế thừa từ `BaseTile` để dễ mở rộng về sau
- Tách input ra nếu có nhu cầu mở rộng sang pc hoặc console

---