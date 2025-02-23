using System;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Boards
{
    public class BoardModel : IDisposable
    {
        private readonly BoardTunables _tunables;
        private TileModel[,] _tileModelArray;

        public BoardModel(BoardTunables tunables)
        {
            _tunables = tunables;
            InitializeTileArray();
        }

        private void InitializeTileArray()
        {
            if (_tunables == null)
                throw new InvalidOperationException("BoardTunables has not been initialized!");

            _tileModelArray = new TileModel[_tunables.Width, _tunables.Height];
            Debug.Log($"Tile array initialized: {_tunables.Width} x {_tunables.Height}");
        }

        public TileModel[,] TileModelArray => _tileModelArray;

        public void SetTileArray(TileModel[,] tiles)
        {
            _tileModelArray = tiles;
        }

        public void Dispose()
        {
            foreach (var tile in _tileModelArray)
            {
                tile?.Dispose();
            }
            _tileModelArray = null;
        }
        
        public BoardTunables Tunables => _tunables;

        public class Factory : PlaceholderFactory<BoardTunables, BoardModel> { }
    }
}