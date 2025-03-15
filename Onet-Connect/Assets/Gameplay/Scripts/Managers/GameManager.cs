using TGS.OnetConnect.Gameplay.Scripts.Boards;
using TGS.OnetConnect.Gameplay.Scripts.Handlers;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Managers
{
    public class GameManager : IInitializable
    {
        private readonly DiContainer _container;
        private readonly TileSpawner _tileSpawner;
        private readonly TileModel.Factory _tileFactory;
        private readonly BoardModel.Factory _boardModelFactory;
        private readonly BoardView.Factory _boardViewFactory;
        private readonly SignalBus _signalBus;

        private BoardModel _boardModel;
        private BoardView _boardView;
        private TileModel[,] _tileModels;

        private TileSelectionHandler _tileSelectionHandler;

        [Inject]
        public GameManager(DiContainer container, SignalBus signalBus, TileSpawner tileSpawner,
            TileModel.Factory tileFactory,
            BoardModel.Factory boardModelFactory, BoardView.Factory boardViewFactory)
        {
            _container = container;
            _signalBus = signalBus;
            _tileSpawner = tileSpawner;
            _tileFactory = tileFactory;
            _boardModelFactory = boardModelFactory;
            _boardViewFactory = boardViewFactory;
        }

        public void Initialize()
        {
            BoardTunables boardTunables = new BoardTunables()
            {
                Height = 10, Width = 10, Level = 1, SkinID = 1
            };
            _boardModel = _boardModelFactory.Create(boardTunables);

            // Instantiate BoardView from Prefab
            _boardView = _boardViewFactory.Create(_boardModel);
            _boardView.Construct(_boardModel);

            // Tile types (assuming there are 6 types)
            int[] tileTypes = { 0, 1, 2, 3, 4 };

            // Size of each TileModel
            float tileWidth = 1.2f;
            float tileHeight = 1.2f;

            // Generate TileModel with custom size
            _tileModels = _tileSpawner.GenerateTileModels(_boardModel, tileTypes, tileWidth, tileHeight);
            _boardModel.SetTileArray(_tileModels);

            _tileSelectionHandler = new TileSelectionHandler(this, _boardModel);
            _signalBus.Subscribe<TileSelectedSignal>(OnTileSelected);
        }
        
        private void OnTileSelected(TileSelectedSignal eventData)
        {
            if (eventData.TileSelected == null)
            {
                Debug.LogError($"Event data is null.");
                return;
            }

            if (_tileSelectionHandler == null)
            {
                Debug.LogError($"Tile selection handler is null.");
                return;
            }

            if (eventData.TileSelected.Tunables.IsEmpty || eventData.TileSelected.Tunables.IsBlocked)
            {
                Debug.LogError($"No tiles selected.");
                return;
            }

            _tileSelectionHandler.HandleTileSelection(eventData.TileSelected);
            Debug.Log($"OnTileSelected. Tile selected: {eventData.TileSelected}");
        }
    }
}