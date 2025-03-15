using UnityEngine;

namespace TGS.OnetConnect.Gameplay.Scripts.Utilities
{
    public class PathRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject pointDotPrefab;
        private LineRenderer _lineRenderer;
        public Vector3 startPoint;
        public Vector3 endPoint;

        void Start()
        {
            RenderLine(startPoint,endPoint);
        }
        void RenderLine(Vector3 startPoint,Vector3 endPoint)
        {
            _lineRenderer = GetComponent<LineRenderer>();
            Destroy(gameObject, 0.3f);

            Vector3 direction = (endPoint - startPoint).normalized;

            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, startPoint);
            _lineRenderer.SetPosition(1, endPoint);

            Instantiate(pointDotPrefab,startPoint + new Vector3(0,0,-1),Quaternion.identity,gameObject.transform);		
            Instantiate(pointDotPrefab,endPoint + new Vector3(0,0,-1),Quaternion.identity,gameObject.transform);		
        }
    }
}