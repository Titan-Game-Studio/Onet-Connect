
using System.Collections.Generic;

namespace TGS.OnetConnect
{
    public class TileRegistry
    {
        readonly List<Tile> _tiles = new List<Tile>();
        
        public IEnumerable<Tile> Tiles => _tiles;

        public void RegisterTile(Tile tile)
        {
            _tiles.Add(tile);
        }

        public void UnregisterTile(Tile tile)
        {
            _tiles.Remove(tile);
        }
    }
}
