using System.Collections.Generic;
using TGS.OnetConnect.Gameplay.Scripts.Boards;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Tiles
{
    public class TileSpawner
    {
        private readonly TileModel.Factory _tileFactory;
        private BoardModel _board;
        private TileModel[,] _tileModels;

        [Inject]
        public TileSpawner(TileModel.Factory tileFactory)
        {
            _tileFactory = tileFactory;
        }

        public TileModel[,] GenerateTileModels(BoardModel board, int[] tileTypes, float tileWidth, float tileHeight)
        {
            _board = board;
            int boardWidth = _board.Tunables.Width;
            int boardHeight = _board.Tunables.Height;
            _tileModels = new TileModel[boardWidth, boardHeight];

            int totalTiles = (boardWidth - 2) * (boardHeight - 2);
            int eachTypeCount = totalTiles / tileTypes.Length;
            int extraCount = totalTiles % tileTypes.Length;

            List<TileModel> tileList = new List<TileModel>();

            foreach (var type in tileTypes)
            {
                for (int i = 0; i < eachTypeCount + (extraCount > 0 ? 1 : 0); i++)
                {
                    TileModel tile = _tileFactory.Create(0, 0, type);
                    tileList.Add(tile);
                }

                if (extraCount > 0)
                {
                    extraCount--;
                }
            }

            Shuffle(tileList);

            // TODO: REFACTOR
            float center = 0f;
            float halfBoardWidth = (boardWidth * tileWidth) / 2;
            float halfBoardHeight = (boardHeight * tileHeight) / 2;

            int index = 0;
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    if(IsBorderTile(x, y,boardWidth,boardHeight))
                    {
                        _tileModels[x, y] = _tileFactory.Create(x, y, -1);
                        _tileModels[x, y].OnSpawned(x, y, -1, null);
                    }
                    else
                    {
                        _tileModels[x, y] = tileList[index++];
                    }

                    TileModel tile = _tileModels[x, y];
                    tile.Tunables.X = x;
                    tile.Tunables.Y = y;
                    // tile.Tunables.Width = tileWidth;
                    // tile.Tunables.Height = tileHeight;

                    Vector3 tilePosition = new Vector3(
                        x * tileWidth - halfBoardWidth + (tileWidth / 2),
                        center,
                        y * tileHeight - halfBoardHeight + (tileHeight / 2)
                    );

                    tile.UpdateViewPosition(tilePosition);
                    tile.UpdateName();
                    tile.Initialize();
                }
            }

            return _tileModels;
        }

        private void Shuffle(List<TileModel> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        private bool IsBorderTile(int x, int y, int gridWidth, int gridHeight)
        {
            return x == 0 || x == gridWidth - 1 || y == 0 || y == gridHeight - 1;
        }

        private bool IsWithinSquare(Vector3 position, float halfBoardWidth, float halfBoardHeight)
        {
            return Mathf.Abs(position.x) <= halfBoardWidth && Mathf.Abs(position.z) <= halfBoardHeight;
        }
    }
}