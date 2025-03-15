using System;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;

namespace TGS.OnetConnect.Gameplay.Scripts.Boards
{
    public interface IBoardModel
    {
        // TODO: TileModel => ITileModel
        TileModel[,] TileModelArray { get; }
        BoardTunables Tunables { get; }
        void SetTileArray(TileModel[,] tiles);
    }

    public abstract class BoardModelBase : IBoardModel, IDisposable
    {
        public abstract TileModel[,] TileModelArray { get; }
        public abstract BoardTunables Tunables { get; }
        public abstract void SetTileArray(TileModel[,] tiles);
        public abstract void Dispose();
    }
}