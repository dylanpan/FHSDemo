using UnityEngine;

namespace Chess
{
	public class ChessGizmosPoint : MonoBehaviour
	{
#if UNITY_EDITOR

	    public bool OnNavMesh = true;
        public Color PointColor = Color.red;
        public float Radius = 0.1f;
        void OnDrawGizmos()
		{
			Gizmos.color = PointColor;
			Gizmos.DrawSphere(transform.position, Radius);
		}
#endif //UNITY_EDITOR
	}
}