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
        None = 0,
        Block,
        Idle,
        Selected,
        Matched,
        Attack,
        Die,
    }

    public class TileStateManager : ITickable, IFixedTickable, IInitializable
    {
        private ITileState _currentStateHandler;
        private TileStates _currentState = TileStates.None;
        private TileView _tileView;

        public ITileState BlockState { get; private set; }
        public ITileState IdleState { get; private set; }
        public ITileState SelectedState { get; private set; }
        public ITileState MatchedState { get; private set; }
        public ITileState AttackState { get; private set; }
        public ITileState DieState { get; private set; }
        public TileStates CurrentState => _currentState;

        [Inject]
        public void Construct(TileView tileView)
        {
            _tileView = tileView;
            BlockState = new BlockState(tileView);
            IdleState = new IdleState(tileView);
            SelectedState = new SelectedState(tileView);
            MatchedState = new MatchedState(tileView);
            AttackState = new AttackState(tileView);
            DieState = new DieState(tileView);
        }


        public void Tick()
        {
            _currentStateHandler?.Update();
        }

        public void FixedTick()
        {
            _currentStateHandler?.FixedUpdate();
        }

        public void Initialize()
        {
            ChangeState(TileStates.Idle);
        }

        public void ChangeState(TileStates newState)
        {
            if (_currentState == newState)
                return;

            _currentStateHandler?.ExitState();

            _currentState = newState;

            switch (newState)
            {
                case TileStates.Block:
                    _currentStateHandler = BlockState;
                    break;
                case TileStates.Idle:
                    _currentStateHandler = IdleState;
                    break;
                case TileStates.Selected:
                    _currentStateHandler = SelectedState;
                    break;
                case TileStates.Matched:
                    _currentStateHandler = MatchedState;
                    break;
                case TileStates.Attack:
                    _currentStateHandler = AttackState;
                    break;
                case TileStates.Die:
                    _currentStateHandler = DieState;
                    break;
            }

            _currentStateHandler?.EnterState();
        }
    }
}