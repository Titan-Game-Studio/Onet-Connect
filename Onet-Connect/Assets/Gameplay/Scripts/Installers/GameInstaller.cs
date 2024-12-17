using System;
using TGS.OnetConnect;
using UnityEngine;
using Zenject;
using GameSignalsInstaller = TGS.OnetConnect.GameSignalsInstaller;

public class GameInstaller : MonoInstaller
{
    [Inject]
    Settings _settings = null;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TileSpawner>().AsSingle();
        
        Container.BindFactory<int, int, int, Tile, Tile.Factory>()
            .FromPoolableMemoryPool<int, int, int, Tile, TilePool>(poolBinder => poolBinder
                .WithInitialSize(5)
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<TileInstaller>(_settings.TilePrefab)
                .UnderTransformGroup("Tiles"));
        
        Container.Bind<TileRegistry>().AsSingle();
        
        GameSignalsInstaller.Install(Container);
    }
    
    [Serializable]
    public class Settings
    {
        public GameObject TilePrefab;
    }
    
    class TilePool : MonoPoolableMemoryPool<int, int, int, IMemoryPool, Tile>
    {
    }
}