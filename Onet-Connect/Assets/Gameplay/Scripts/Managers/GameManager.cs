using TGS.OnetConnect.Gameplay.Scripts.Boards;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Managers
{
    public class GameManager : IInitializable
    {
        private readonly TileSpawner _tileSpawner;
        private readonly TileModel.Factory _tileFactory;
        private readonly BoardModel.Factory _boardModelFactory;
        private readonly BoardView.Factory _boardViewFactory;
        readonly SignalBus _signalBus;

        private BoardModel _boardModel;
        private BoardView _boardView;
        private TileModel[,] _tileModels;

        [Inject]
        public GameManager(SignalBus signalBus, TileSpawner tileSpawner, TileModel.Factory tileFactory,
            BoardModel.Factory boardModelFactory, BoardView.Factory boardViewFactory)
        {
            _signalBus = signalBus;
            _tileSpawner = tileSpawner;
            _tileFactory = tileFactory;
            _boardModelFactory = boardModelFactory;
            _boardViewFactory = boardViewFactory;
        }

        public void Initialize()
        {
            // _signalBus.Subscribe<TileMatchedSignal>(OnTileMatched);

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
        }
    }
}