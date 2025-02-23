using System;
using TGS.OnetConnect;
using TGS.OnetConnect.Gameplay.Scripts.Boards;
using TGS.OnetConnect.Gameplay.Scripts.Managers;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using UnityEngine;
using Zenject;
using GameSignalsInstaller = TGS.OnetConnect.GameSignalsInstaller;

public class GameInstaller : MonoInstaller
{
    [Inject] private Settings _settings = null;

    [Inject] private AddressableLoader<GameObject> _addressableLoader;

    public override void InstallBindings()
    {
        #region SIGNALS

        GameSignalsInstaller.Install(Container);

        Container.DeclareSignal<TileMatchedSignal>();

        #endregion /SIGNALS

        #region TILE_SPAWNER

        Container.Bind<TileSpawner>().AsSingle();

        #endregion /TILE_SPAWNER

        #region BOARDS

        // Bind BoardModel
        Container.BindFactory<BoardTunables, BoardModel, BoardModel.Factory>().AsSingle();
        
        // Bind BoardView from Prefab
        Container.BindFactory<BoardModel, BoardView, BoardView.Factory>()
            .FromComponentInNewPrefab(_settings.BoardfPrefab)
            .UnderTransformGroup("BoardViews")
            .AsSingle();

        #endregion /BOARDS

        #region TILES

        // GameObject tilePrefab = await _addressableLoader.LoadAssetAsync("Tile");

        Container.BindFactory<int, int, int, TileModel, TileModel.Factory>()
            .FromPoolableMemoryPool<int, int, int, TileModel, TilePool>(poolBinder => poolBinder
                .WithInitialSize(5)
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<TileInstaller>(_settings.TilePrefab)
                .UnderTransformGroup("Tiles"));

        // Debug.Log($"Install Bindings: {tilePrefab}");

        Container.Bind<TileRegistry>().AsSingle();

        #endregion /TILES

        #region MANAGER

        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();

        #endregion
    }

    [Serializable]
    public class Settings
    {
        public GameObject TilePrefab;
        public GameObject BoardfPrefab;
    }

    public class TilePool : MonoPoolableMemoryPool<int, int, int, IMemoryPool, TileModel>
    {
    }
}