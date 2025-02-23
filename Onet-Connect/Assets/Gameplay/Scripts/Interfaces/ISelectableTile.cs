namespace TGS.OnetConnect.Gameplay.Scripts.Interfaces
{
    public interface ISelectableTile
    {
        void SetSelected(bool selected);
        void OnTileSelected();
        void OnTileDeselected();
    }
}
