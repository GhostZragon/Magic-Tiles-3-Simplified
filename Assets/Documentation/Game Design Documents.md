# 🎮 Game Design Document – Magic Tiles (Simplified)

## 1. Gameplay Overview
- Nhịp game: các tile rơi theo beat, người chơi tap đúng lúc để ghi điểm.
- Mục tiêu: ghi điểm cao nhất có thể, tránh miss tile.

## 2. Tile Types
| Loại Tile    | Mô tả                                                        |
|--------------|--------------------------------------------------------------|
| **Normal**   | Tap đúng lúc rơi vào vùng chạm                                |
| **Long Tap** | Giữ tay từ khi tile chạm vùng cho tới khi kết thúc           |
| **Double Tap**| 2 tile rơi cùng lúc – phải tap cả 2 gần như đồng thời       |

## 3. Scoring System
- **Perfect**: ±0.1s → +100 điểm
- **Good**: ±0.2s → +50 điểm
- **Miss**: Ngoài vùng thời gian → 25 điểm, mất combo

## 4. Combo System
- Mỗi hit Perfect tăng combo
- Mỗi 10 combo → nhân điểm x2, x3...
- Nếu miss → reset combo

## 5. Game Over
- Khi tile rơi chạm đáy mà chưa được tap
- Hiển thị điểm + combo cao nhất cho mỗi bài hát

## 6. UI/UX
- Menu/ Level Selection/ Gameplay/ Settings
- UI dùng Canvas (scale tự động mobile)
- Mỗi tile là 1 prefab với màu sắc khác nhau
- Có hiệu ứng particle và âm thanh khi tap

## 7. Kỹ thuật
- Unity 2021.3 LTS
- Target: Android/iOS
- Object pooling cho tile
- Tile spawn theo data map

## 8. Stretch Features (Nếu kịp)
- Background động
- FX cho combo cao