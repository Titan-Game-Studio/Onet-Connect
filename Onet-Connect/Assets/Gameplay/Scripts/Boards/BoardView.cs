using UnityEngine;
using Zenject;

namespace TGS.OnetConnect.Gameplay.Scripts.Boards
{
    public enum EBoardsCorner
    {
        TopLeft = 0,
        TopRight = 1,
        BottomRight = 2,
        BottomLeft = 3,
    }

    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Transform[] _corners;

        private BoardModel _boardModel;

        [Inject]
        public void Construct(BoardModel boardModel)
        {
            _boardModel = boardModel;
        }

        public MeshRenderer Renderer => _renderer;
        public Rigidbody RigidBody => _rigidBody;

        public float BoardWidth => Vector3.Distance(_corners[0].position, _corners[1].position);
        public float BoardHeight => Vector3.Distance(_corners[1].position, _corners[2].position);

        public Transform GetCorner(EBoardsCorner corner) => _corners[(int)corner];
        public Vector3 GetCornerPosition(EBoardsCorner corner) => _corners[(int)corner].position;

        public class Factory : PlaceholderFactory<BoardModel, BoardView>
        {
        }
    }
}