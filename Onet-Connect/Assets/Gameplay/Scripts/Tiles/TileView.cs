using System;
using System.Collections.Generic;
using TGS.OnetConnect.Gameplay.Scripts.Tiles;
using TGS.OnetConnect.Gameplay.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace TGS.OnetConnect
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer = null;
        [SerializeField] private Collider _collider = null;
        [SerializeField] private Rigidbody _rigidBody = null;

        [SerializeField] private Transform _monterContainer;
        [SerializeField] private Transform _markSelectedContainer;
        [SerializeField] private EventsHandler _eventsHandler = null;

        [SerializeField] private List<GameObject> _monterPrefabs = new List<GameObject>();

        [Inject] public TileModel TileModel { get; set; }

        public Action OnTileSelectedAtc = null;
        public Action OnTileDeselectedAtc = null;

        public GameObject Initialize()
        {
            _eventsHandler.AtcPointerDown += OnTileSelected;
            
            int index = (int)TileModel.Tunables.Type;
            if (index < 0 || index >= _monterPrefabs.Count)
            {
                // Instantiate empty & block
                return null;
            }

            return Instantiate(_monterPrefabs[(int)TileModel.Tunables.Type], _monterContainer);
        }

        public MeshRenderer Renderer => _renderer;

        public Collider Collider => _collider;

        public Rigidbody Rigidbody => _rigidBody;

        public EventsHandler EventsHandler => _eventsHandler;

        public Vector3 LookDir => -_rigidBody.transform.right;

        public Vector3 RightDir => _rigidBody.transform.up;

        public Vector3 ForwardDir => _rigidBody.transform.right;

        public Vector3 Position
        {
            get => _rigidBody.transform.position;
            set => _rigidBody.transform.position = value;
        }

        public Quaternion Rotation
        {
            get => _rigidBody.rotation;
            set => _rigidBody.rotation = value;
        }

        public Vector3 Velocity => _rigidBody.linearVelocity;

        public Vector3 AngularVelocity
        {
            get => _rigidBody.angularVelocity;
            set => _rigidBody.angularVelocity = value;
        }

        public void OnTileSelected()
        {
            OnTileSelectedAtc?.Invoke();
        }

        public void OnTileDeselected()
        {
            OnTileDeselectedAtc?.Invoke();
        }

        private void SetActiveMarkSelected(bool isActive = false)
        {
            _markSelectedContainer.gameObject.SetActive(isActive);
        }

        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force);
        }

        public void AddTorque(float value)
        {
            _rigidBody.AddTorque(Vector3.forward * value);
        }
    }
}