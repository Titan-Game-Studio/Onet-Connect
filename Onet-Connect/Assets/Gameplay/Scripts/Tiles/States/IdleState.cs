using UnityEngine;
using Zenject;

namespace TGS.OnetConnect
{
    public class IdleState : ITileState
    {
        private readonly TileView _tileView;

        public IdleState(TileView tileView)
        {
            _tileView = tileView;
        }

        public void EnterState()
        {
            Debug.Log("Entering Idle State");
        }

        public void ExitState()
        {
            Debug.Log("Exiting Idle State");
        }

        public void Update()
        {
            // Logic cập nhật cho trạng thái Idle
        }

        public void FixedUpdate()
        {
            // Logic FixedUpdate cho trạng thái Idle nếu cần
        }
    }

}
