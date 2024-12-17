using TGS.OnetConnect;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public TileSpawner.Settings TileSpawnerSettings;
    public GameInstaller.Settings GameInstallerSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(TileSpawnerSettings).IfNotBound();
        Container.BindInstance(GameInstallerSettings).IfNotBound();
    }
}