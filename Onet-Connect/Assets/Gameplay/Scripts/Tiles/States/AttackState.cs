using UnityEngine;

namespace TGS.OnetConnect
{
    public class AttackState : ITileState
    {
        private readonly TileView _tileView;

        public AttackState(TileView tileView)
        {
            _tileView = tileView;
        }

        public void EnterState()
        {
            Debug.Log("Entering Attack State");
        }

        public void ExitState()
        {
            Debug.Log("Exiting Attack State");
        }

        public void Update()
        {
            // Logic cho trạng thái tấn công
        }

        public void FixedUpdate()
        {
            // Logic FixedUpdate cho trạng thái Attack
        }
    }

}
