using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameInstaller.Settings GameInstallerSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(GameInstallerSettings).IfNotBound();
        }
    }
}