using UnityEngine;

namespace TGS.OnetConnect
{
    public class BlockState : ITileState
    {
        private readonly TileView _tileView;

        public BlockState(TileView tileView)
        {
            _tileView = tileView;
        }

        public void EnterState()
        {
            Debug.Log("Entering Block State");
        }

        public void ExitState()
        {
            Debug.Log("Exiting Block State");
        }

        public void Update()
        {
            // Logic cập nhật trạng thái Block
        }

        public void FixedUpdate()
        {
            // Logic cập nhật FixedUpdate cho Block nếu cần
        }
    }

}
