using System;
using TGS.OnetConnect.Gameplay.Scripts.Boards;
using TGS.OnetConnect.Gameplay.Scripts.Managers;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Handlers
{
    public class TileSelectionHandler
    {
        [Inject] private readonly SignalBus _signalBus;
        private GameManager _gameManager;
        private TilePathFinder _matchingPathFinder;
        private BoardModel _board;

        private TileModel _firstSelectedTile = null;

        public TileSelectionHandler(GameManager gameManager, BoardModel board)
        {
            _gameManager = gameManager;
            _board = board;
            _matchingPathFinder = new TilePathFinder(_board);
        }

        public static event Action OnTilesMatch;

        public void HandleTileSelection(TileModel selectedTile)
        {
            if (selectedTile == null || selectedTile.IsEmpty || !selectedTile.IsInteractable)
            {
                Debug.LogError($"Selected tile is not interactable: {selectedTile} >> {selectedTile.IsInteractable}");
                return;
            }

            if (_firstSelectedTile == null)
            {
                _firstSelectedTile = selectedTile;
                _firstSelectedTile.OnSelected();
                Debug.Log($"First Selected Tile {_firstSelectedTile}");
            }
            else
            {
                Debug.Log($"Second Selected Tile {_firstSelectedTile} || Selected Tile {selectedTile}");
                if (_firstSelectedTile.IsEquals(selectedTile))
                {
                    // Nhấn cùng một ô -> Hủy chọn
                    // _firstSelectedTile.OnDeselect();
                    // _firstSelectedTile = null;
                    Debug.Log($"Second Selected Tile XXX {_firstSelectedTile}");
                    return;
                }

                if (!_firstSelectedTile.IsSameType(selectedTile))
                {
                    _firstSelectedTile.OnDeselect();
                    _firstSelectedTile = selectedTile;
                    _firstSelectedTile.OnSelected();
                    return;
                }

                // Kiểm tra nếu hai ô có thể kết nối
                if (_matchingPathFinder.IsPathValid(_firstSelectedTile, selectedTile))
                {
                    // Gửi tín hiệu và kích hoạt sự kiện
                    // _signalBus.Fire(new TileMatchedSignal(_firstSelectedTile, selectedTile));
                    Debug.Log($"OnTilesMatch OK OK OK");
                    OnTilesMatch?.Invoke();

                    // Xóa ô đã chọn
                    selectedTile.SetMatched();
                    _firstSelectedTile.SetMatched();

                    // Reset lựa chọn
                    _firstSelectedTile = null;
                }
                else
                {
                    // Nếu không khớp, hủy chọn ô đầu tiên và chọn ô mới
                    _firstSelectedTile.OnDeselect();
                    _firstSelectedTile = selectedTile;
                    _firstSelectedTile.OnSelected();
                    Debug.Log($"OnTilesMatch XXX");
                }
            }
        }
    }
}
