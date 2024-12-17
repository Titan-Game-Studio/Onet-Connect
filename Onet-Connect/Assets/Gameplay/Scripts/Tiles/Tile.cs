using System;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect
{
    public class Tile : MonoBehaviour, IPoolable<int, int, int, IMemoryPool>, IDisposable
    {
        private TileView _view;
        private TileTunables _tunables;
        private TileStateManager _stateManager;
        private TileRegistry _registry;
        private IMemoryPool _pool;
        
        [Inject]
        public void Construct(
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
        
        public TileStates State
        {
            get { return _stateManager.CurrentState; }
        }
        
        public Vector3 Position
        {
            get { return _view.Position; }
            set { _view.Position = value; }
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            _registry.UnregisterTile(this);
            _pool = null;
        }

        public void OnSpawned(int x, int y, int type, IMemoryPool pool)
        {
            _pool = pool;
            _tunables.X = x;
            _tunables.Y = y;
            _tunables.Type = type;

            _registry.RegisterTile(this);

            OnBizz();
        }

        public void OnBizz()
        {
            Debug.Log($"{_tunables.Type}: {_tunables.X}, {_tunables.Y}");
        }

        public class Factory : PlaceholderFactory<int, int, int, Tile>
        {
        }
    }
}