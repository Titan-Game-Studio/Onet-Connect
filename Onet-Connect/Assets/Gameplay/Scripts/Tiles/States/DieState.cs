using UnityEngine;

namespace TGS.OnetConnect
{
    public class DieState : ITileState
    {
        private readonly TileView _tileView;

        public DieState(TileView tileView)
        {
            _tileView = tileView;
        }

        public void EnterState()
        {
            Debug.Log("Entering Die State");
        }

        public void ExitState()
        {
            Debug.Log("Exiting Die State");
        }

        public void Update()
        {
            // Logic khi tile bị tiêu diệt
        }

        public void FixedUpdate()
        {
            // Logic FixedUpdate cho trạng thái Die
        }
    }

}
