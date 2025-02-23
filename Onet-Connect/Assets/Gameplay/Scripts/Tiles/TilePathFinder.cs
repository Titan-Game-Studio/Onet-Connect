using System.Collections.Generic;
using TGS.OnetConnect.Gameplay.Scripts.Boards;
using TGS.OnetConnect.Gameplay.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Tiles
{
    public class TilePathFinder
    {
        private readonly BoardModel _board;
        private readonly PathRenderer _line;
        public bool RenderLine { get; set; }
    
        public List<TileModel> FirstPassEmptyTiles { get; private set; } = new List<TileModel>();
        public List<TileModel> SecondPassEmptyTiles { get; private set; } = new List<TileModel>();
        public List<Vector3> LinePoints { get; private set; } = new List<Vector3>();

        [Inject]
        public TilePathFinder(BoardModel board, PathRenderer line)
        {
            _board = board;
            _line = line;
        }

        public bool IsPathValid(TileModel selectedTile1, TileModel selectedTile2)
        {
            FirstPassEmptyTiles.Clear();
            SecondPassEmptyTiles.Clear();
            LinePoints.Clear();
        
            return CheckStraightLine(selectedTile1, selectedTile2, FirstPassEmptyTiles, selectedTile2.Position) ||
                   CheckOneBendLine(selectedTile2, selectedTile1, FirstPassEmptyTiles, SecondPassEmptyTiles, selectedTile1.Position) ||
                   CheckTwoBendsLine(FirstPassEmptyTiles, SecondPassEmptyTiles, selectedTile1.Position, selectedTile2.Position);
        }

        private bool CheckStraightLine(TileModel firstTile, TileModel secondTile, List<TileModel> emptyTiles, Vector3 secondTilePos)
        {
            return IsStraightDirectionMatch(firstTile, secondTile, emptyTiles, 1, 0) ||
                   IsStraightDirectionMatch(firstTile, secondTile, emptyTiles, -1, 0) ||
                   IsStraightDirectionMatch(firstTile, secondTile, emptyTiles, 0, 1) ||
                   IsStraightDirectionMatch(firstTile, secondTile, emptyTiles, 0, -1);
        }

        private bool CheckOneBendLine(TileModel firstTile, TileModel secondTile, List<TileModel> firstList, List<TileModel> emptyTiles, Vector3 firstTilePos)
        {
            return IsOneBendDirectionMatch(firstTile, secondTile, firstList, emptyTiles, 1, 0) ||
                   IsOneBendDirectionMatch(firstTile, secondTile, firstList, emptyTiles, -1, 0) ||
                   IsOneBendDirectionMatch(firstTile, secondTile, firstList, emptyTiles, 0, 1) ||
                   IsOneBendDirectionMatch(firstTile, secondTile, firstList, emptyTiles, 0, -1);
        }

        private bool CheckTwoBendsLine(List<TileModel> firstList, List<TileModel> secondList, Vector3 firstTilePos, Vector3 secondTilePos)
        {
            return IsTwoBendsDirectionMatch(firstList, secondList, firstTilePos, secondTilePos, 1, 0) ||
                   IsTwoBendsDirectionMatch(firstList, secondList, firstTilePos, secondTilePos, -1, 0) ||
                   IsTwoBendsDirectionMatch(firstList, secondList, firstTilePos, secondTilePos, 0, 1) ||
                   IsTwoBendsDirectionMatch(firstList, secondList, firstTilePos, secondTilePos, 0, -1);
        }

        private bool IsStraightDirectionMatch(TileModel firstTile, TileModel secondTile, List<TileModel> emptyTilesInPath, int dx, int dy)
        {
            int x = (int)firstTile.Position.x;
            int y = (int)firstTile.Position.y;

            for (int i = x + dx, j = y + dy; i >= 0 && i < _board.Tunables.Width && j >= 0 && j < _board.Tunables.Height; i += dx, j += dy)
            {
                if (_board.TileModelArray[i, j] == secondTile)
                {
                    RenderMatchingLine(firstTile.Position, secondTile.Position);
                    return true;
                }
                else if (!_board.TileModelArray[i, j].IsEmpty)
                {
                    break;
                }
                emptyTilesInPath.Add(_board.TileModelArray[i, j]);
            }
            return false;
        }

        private bool IsOneBendDirectionMatch(TileModel firstTile, TileModel secondTile, List<TileModel> firstList, List<TileModel> emptyTilesInPath, int dx, int dy)
        {
            int x = (int)firstTile.Position.x;
            int y = (int)firstTile.Position.y;

            for (int i = x + dx, j = y + dy; i >= 0 && i < _board.Tunables.Width && j >= 0 && j < _board.Tunables.Height; i += dx, j += dy)
            {
                if (firstList.Contains(_board.TileModelArray[i, j]))
                {
                    RenderMatchingLine(firstTile.Position, _board.TileModelArray[i, j].Position);
                    RenderMatchingLine(_board.TileModelArray[i, j].Position, secondTile.Position);
                    return true;
                }
                else if (!_board.TileModelArray[i, j].IsEmpty)
                {
                    break;
                }
                emptyTilesInPath.Add(_board.TileModelArray[i, j]);
            }
            return false;
        }

        private bool IsTwoBendsDirectionMatch(List<TileModel> firstList, List<TileModel> secondList, Vector3 firstTilePos, Vector3 secondTilePos, int dx, int dy)
        {
            foreach (TileModel tile in firstList)
            {
                int x = (int)tile.Position.x;
                int y = (int)tile.Position.y;

                for (int i = x + dx, j = y + dy; i >= 0 && i < _board.Tunables.Width && j >= 0 && j < _board.Tunables.Height; i += dx, j += dy)
                {
                    if (secondList.Contains(_board.TileModelArray[i, j]))
                    {
                        RenderMatchingLine(firstTilePos, tile.Position);
                        RenderMatchingLine(tile.Position, _board.TileModelArray[i, j].Position);
                        RenderMatchingLine(_board.TileModelArray[i, j].Position, secondTilePos);
                        return true;
                    }
                    else if (!_board.TileModelArray[i, j].IsEmpty)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        private void RenderMatchingLine(Vector3 startPoint, Vector3 endPoint)
        {
            if (RenderLine && _line != null)
            {
                var lineInstance = Object.Instantiate(_line, Vector3.zero, Quaternion.identity);
                lineInstance.startPoint = startPoint;
                lineInstance.endPoint = endPoint;
            }
        }
    }
}
