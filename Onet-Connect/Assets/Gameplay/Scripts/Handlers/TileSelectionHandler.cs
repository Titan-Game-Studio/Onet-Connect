using System;
using TGS.OnetConnect.Gameplay.Scripts.Boards;
using TGS.OnetConnect.Gameplay.Scripts.Managers;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Handlers
{
    public class TileSelectionHandler
    {
        [Inject] private readonly GameManager _gameManager;
        [Inject] private readonly TilePathFinder _matchingPathFinder;
        [Inject] private readonly BoardModel _board;
        [Inject] private readonly SignalBus _signalBus;

        private TileModel _firstSelectedTile = null;

        public static event Action OnTilesMatch;

        public void HandleTileSelection(TileModel selectedTile)
        {
            if (selectedTile == null || !selectedTile.IsInteractable)
            {
                return;
            }

            if (_firstSelectedTile == null)
            {
                // Chọn ô đầu tiên
                _firstSelectedTile = selectedTile;
                _firstSelectedTile.SetSelected(true);
            }
            else
            {
                // Chọn ô thứ hai
                if (_firstSelectedTile == selectedTile)
                {
                    // Nhấn cùng một ô -> Hủy chọn
                    // _firstSelectedTile.SetSelected(false);
                    // _firstSelectedTile = null;
                    return;
                }

                // Kiểm tra nếu hai ô có thể kết nối
                if (_matchingPathFinder.IsPathValid(_firstSelectedTile, selectedTile))
                {
                    // Gửi tín hiệu và kích hoạt sự kiện
                    _signalBus.Fire(new TileMatchedSignal(_firstSelectedTile, selectedTile));
                    OnTilesMatch?.Invoke();

                    // Xóa ô đã chọn
                    _firstSelectedTile.SetMatched();
                    selectedTile.SetMatched();

                    // Reset lựa chọn
                    _firstSelectedTile = null;
                }
                else
                {
                    // Nếu không khớp, hủy chọn ô đầu tiên và chọn ô mới
                    _firstSelectedTile.SetSelected(false);
                    _firstSelectedTile = selectedTile;
                    _firstSelectedTile.SetSelected(true);
                }
            }
        }
    }
}
