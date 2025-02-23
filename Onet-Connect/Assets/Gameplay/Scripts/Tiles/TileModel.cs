using System;
using TGS.OnetConnect.Gameplay.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Tiles
{
    public class TileModel : MonoBehaviour, IPoolable<int, int, int, IMemoryPool>, IDisposable
    {
        private TileTunables _tunables;
        private TileView _view;
        private TileStateManager _stateManager;
        private TileRegistry _registry;
        private IMemoryPool _pool;

        private bool _isInteractable;
        
        [Inject]
        private GameManager _gameManager;

        [Inject]
        public void Construct(
            SignalBus signalBus,
            TileView view,
            TileTunables tunables,
            TileStateManager stateManager,
            TileRegistry registry)
        {
            _view = view;
            _tunables = tunables;
            _stateManager = stateManager;
            _registry = registry;
        }

        public TileView View => _view;

        public TileTunables Tunables
        {
            get => _tunables;
            set => _tunables = value;
        }
        
        public bool IsEmpty => _tunables.IsEmpty;

        public TileStates State => _stateManager.CurrentState;

        public Vector3 Position
        {
            get => _view.Position;
            set => _view.Position = value;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            _isInteractable = false;
            _registry.UnregisterTile(this);
            _pool = null;
        }

        public void OnSpawned(int x, int y, int type, IMemoryPool pool)
        {
            _pool = pool;
            _tunables.X = x;
            _tunables.Y = y;
            _tunables.Type = (ETileType)type;
            
            _registry.RegisterTile(this);
        }

        public void UpdateViewPosition(Vector3 position)
        {
            _view.Position = position;
        }

        public void Initialize()
        {
            _view.Initialize();
            _view.OnTileDeselected();
            _isInteractable = true;
        }
        
        public bool IsInteractable => _isInteractable;

        public void SetSelected(bool selected)
        {
            Debug.Log($"SetSelected: {selected}");
        }

        public void SetMatched()
        {
            Debug.Log($"SetMatched");
        }

        public void OnSelected()
        {
            _view.OnTileSelected();
        }

        public void OnDeselected()
        {
            _view.OnTileDeselected();
        }

        public void UpdateName()
        {
            _view.gameObject.name = $"Tile_{_tunables.X}_{_tunables.Y}_{_tunables.Type}";
        }

        public class Factory : PlaceholderFactory<int, int, int, TileModel>
        {
        }
    }
}