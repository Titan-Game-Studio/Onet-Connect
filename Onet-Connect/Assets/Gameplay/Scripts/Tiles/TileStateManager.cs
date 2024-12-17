using UnityEngine;
using Zenject;

namespace TGS.OnetConnect
{
    public interface ITileState
    {
        void EnterState();
        void ExitState();
        void Update();
        void FixedUpdate();
    }

    public enum TileStates
    {
        Idle,
        Attack,
        Follow,
        None
    }

    public class TileStateManager : ITickable, IFixedTickable, IInitializable
    {
        private ITileState _currentStateHandler;
        private TileStates _currentState = TileStates.None;
        private TileView _tileView;

        [Inject]
        public void Construct()
        {
        }

        public TileStates CurrentState => _currentState;

        public void Tick()
        {
            Debug.Log("Tick");
        }

        public void FixedTick()
        {
            Debug.Log("FixedTick");
        }

        public void Initialize()
        {
            Debug.Log("Initialize");
        }
    }
}