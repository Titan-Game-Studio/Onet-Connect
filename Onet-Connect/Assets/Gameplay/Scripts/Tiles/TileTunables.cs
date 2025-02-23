namespace TGS.OnetConnect.Gameplay.Scripts.Tiles
{
    public enum ETileType : int
    {
        Block = -2,
        None = -1,
        Angel = 0,
        Death = 1,
        Fire = 2,
        Ice = 3,
        Wind = 4
    }

    [System.Serializable]
    public class TileTunables
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ETileType Type { get; set; }
        public bool IsEmpty => Type == ETileType.None;
        public bool IsBlocked => Type == ETileType.Block;

        public void CopyFrom(TileTunables other)
        {
            X = other.X;
            Y = other.Y;
            Type = other.Type;
        }
    }
}