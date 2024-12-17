using Zenject;

namespace TGS.OnetConnect
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}