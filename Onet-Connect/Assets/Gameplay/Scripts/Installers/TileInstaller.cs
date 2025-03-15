using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using Zenject;

namespace TGS.OnetConnect
{
    public class TileInstaller : Installer<TileInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<TileTunables>().AsSingle();
            Container.BindInterfacesAndSelfTo<TileStateManager>().AsSingle();
        }
    }
}