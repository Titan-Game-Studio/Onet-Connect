using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TGS.OnetConnect
{
    public class TileSpawner : ITickable, IInitializable
    {
        readonly Tile.Factory _tileFactory;
        readonly SignalBus _signalBus;
        readonly Settings _settings;

        private int _desiredNumTiles;
        private int _tileCount;
        private float _lastSpawnTime;

        public TileSpawner(SignalBus signalBus, Tile.Factory tileFactory, Settings settings)
        {
            _signalBus = signalBus;
            _tileFactory = tileFactory;
            _settings = settings;
        }

        public void Initialize()
        {
            // _signalBus.Subscribe<TileMatchedSignal>(OnTileMatched);
        }

        private void OnTileMatched()
        {
            Debug.Log("OnTileMatched");
        }

        public void Tick()
        {
            if (_tileCount < 10 && Time.realtimeSinceStartup - _lastSpawnTime > _settings.MinDelayBetweenSpawns)
            {
                SpawnTile();
                _tileCount++;
            }
        }

        private void SpawnTile()
        {
            int x = Random.Range(_settings.XMin, _settings.XMax);
            int y = Random.Range(_settings.YMin, _settings.YMax);
            int type = Random.Range(_settings.TypeMin, _settings.TypeMax);

            Tile tile = _tileFactory.Create(x, y, type);
            tile.OnBizz();
            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        [Serializable]
        public class Settings
        {
            public int XMin;
            public int XMax;

            public int YMin;
            public int YMax;

            public int TypeMin;
            public int TypeMax;

            public float MinDelayBetweenSpawns = 0.5f;
        }
    }
}