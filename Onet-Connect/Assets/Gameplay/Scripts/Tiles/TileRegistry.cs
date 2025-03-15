using System.Collections.Generic;

namespace TGS.OnetConnect.Gameplay.Scripts.Tiles
{
    public class TileRegistry
    {
        readonly List<TileModel> _tiles = new List<TileModel>();
        
        public IEnumerable<TileModel> Tiles => _tiles;

        public void RegisterTile(TileModel tileModel)
        {
            _tiles.Add(tileModel);
        }

        public void UnregisterTile(TileModel tileModel)
        {
            _tiles.Remove(tileModel);
        }
    }
}
