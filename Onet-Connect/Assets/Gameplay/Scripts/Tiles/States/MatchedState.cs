using UnityEngine;

namespace TGS.OnetConnect
{
    public class MatchedState : ITileState
    {
        private readonly TileView _tileView;

        public MatchedState(TileView tileView)
        {
            _tileView = tileView;
        }

        public void EnterState()
        {
            Debug.Log("Entering Matched State");
        }

        public void ExitState()
        {
            Debug.Log("Exiting Matched State");
        }

        public void Update()
        {
            // Logic khi tile được kết nối thành công
        }

        public void FixedUpdate()
        {
            // Logic FixedUpdate cho trạng thái Matched
        }
    }

}
