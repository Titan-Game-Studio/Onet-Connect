using UnityEngine;

namespace TGS.OnetConnect
{
    public class SelectedState : ITileState
    {
        private readonly TileStateManager _stateManager;
        private readonly TileView _tileView;

        public SelectedState(TileView tileView)
        {
            _tileView = tileView;
        }

        public void EnterState()
        {
            Debug.Log("Entering Selected State");
        }

        public void ExitState()
        {
            Debug.Log("Exiting Selected State");
        }

        public void Update()
        {
            // Logic khi tile được chọn
        }

        public void FixedUpdate()
        {
            // Logic FixedUpdate khi tile được chọn
        }
    }

}
