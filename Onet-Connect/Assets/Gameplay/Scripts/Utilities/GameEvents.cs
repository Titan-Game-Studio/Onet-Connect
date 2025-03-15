using System.Collections.Generic;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;

namespace TGS.OnetConnect
{
    public struct PlayerDiedSignal
    {
    }

    public struct OpponentDiedSignal
    {
    }

    public struct TileSelectedSignal
    {
        public TileModel TileSelected;

        public TileSelectedSignal(TileModel tileSelected)
        {
            TileSelected = tileSelected;
        }
    }

    public struct TileMatchedSignal
    {
        public TileModel FirstTile { get; }
        public TileModel SecondTile { get; }

        public TileMatchedSignal(TileModel firstTile, TileModel secondTile)
        {
            FirstTile = firstTile;
            SecondTile = secondTile;
        }
    }
}