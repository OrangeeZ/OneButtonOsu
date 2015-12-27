using System.Linq;
using UnityEngine;
using System.Collections;

public class MeshWobble : MonoBehaviour {

	public Vector3[] initialVertices;

	public float gridSize = .2f;

	public Vector2 range = new Vector2( -.1f, .1f );

	public float updateInterval = .2f;

	[SerializeField]
	private SkinnedMeshRenderer meshRenderer;

	void Reset() {

		meshRenderer = GetComponent<SkinnedMeshRenderer>();
	}

	IEnumerator Start() {

		initialVertices = meshRenderer.sharedMesh.vertices;

		//while ( true ) {

		//	var newVertices = initialVertices
		//		.Select( each => transform.TransformPoint( each ) )
		//		.Select( each => new Vector3( Mathf.Round( each.x / gridSize ), Mathf.Round( each.y / gridSize ), Mathf.Round( each.z / gridSize ) ) * gridSize )
		//		.Select( each => transform.InverseTransformPoint( each ) );

		//	meshRenderer.sharedMesh.vertices = newVertices.ToArray();

		//	yield return new WaitForSeconds( updateInterval );
		//}

		while ( true ) {

			var newVertices = initialVertices
				.Select( each => new Vector3( each.x + Random.Range( range.x, range.y ), each.y + Random.Range( range.x, range.y ), each.z + Random.Range( range.x, range.y ) ) );

			meshRenderer.sharedMesh.vertices = newVertices.ToArray();

			yield return new WaitForSeconds( updateInterval );
		}
	}
}
