using System;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Boards
{
    public class BoardModel : BoardModelBase
    {
        private readonly BoardTunables _tunables;
        private TileModel[,] _tileModelArray;

        public BoardModel(BoardTunables tunables)
        {
            _tunables = tunables ?? throw new ArgumentNullException(nameof(tunables));
            InitializeTileArray();
        }

        private void InitializeTileArray()
        {
            if (_tunables == null)
                throw new InvalidOperationException("BoardTunables has not been initialized!");

            _tileModelArray = new TileModel[_tunables.Width, _tunables.Height];
            Debug.Log($"Tile array initialized: {_tunables.Width} x {_tunables.Height}");
        }

        public override TileModel[,] TileModelArray => _tileModelArray;

        public override void SetTileArray(TileModel[,] tiles)
        {
            if (tiles == null)
                throw new ArgumentNullException(nameof(tiles));

            if (tiles.GetLength(0) != _tunables.Width || tiles.GetLength(1) != _tunables.Height)
                throw new ArgumentException("Tile array dimensions do not match BoardTunables!");
            _tileModelArray = tiles;
        }

        public override void Dispose()
        {
            if (_tileModelArray == null)
            {
                return;
            }

            foreach (var tile in _tileModelArray)
            {
                tile?.Dispose();
            }

            _tileModelArray = null;
        }

        public override BoardTunables Tunables => _tunables;

        public class Factory : PlaceholderFactory<BoardTunables, BoardModel>
        {
        }
    }
}