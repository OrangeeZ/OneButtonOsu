using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Utility {

	public class GraphPlotter {

		public MeshFilter meshFilter = null;
		private int iterations = 20;

		public float width = 20f;

		public float from = 0f;
		public float to = 1f;

		private Vector3[] vertices;
		private int[] triangles;
		private Vector2[] uv;
 
		public GameObject gameObject {
			get {
				return ( meshFilter ) ? meshFilter.gameObject : null;
			}
		}

		public static GraphPlotter Create( MeshFilter meshFilter, int iterations ) {
			var plotter = new GraphPlotter { meshFilter = meshFilter };

			plotter.meshFilter.sharedMesh = new Mesh();
			plotter.RebuildMeshCache( iterations );

			return plotter;
		}

		public static GraphPlotter Create( int iterations ) {
			var plotter = new GraphPlotter {meshFilter = ( new GameObject ( "GraphPlotter" ) ).AddComponent <MeshFilter>()};

			plotter.meshFilter.sharedMesh = new Mesh();

			plotter.gameObject.AddComponent<MeshRenderer>();

			plotter.RebuildMeshCache( iterations );

			return plotter;
		}

		public static implicit operator bool( GraphPlotter plotter ) {
			return plotter != null && plotter.meshFilter != null;
		}

		public void Plot( System.Func<float, Vector3> f, int iterations, Vector3 normal ) {
			
			RebuildMeshCache( iterations );
		
			Plot( f, v => v, normal );
		}

		public void Plot( System.Func<float, Vector3> f, System.Func<Vector3, Vector3> postprocessVertex, Vector3 normal ) {

			var points = new Vector3[iterations];

			for ( var i = 0; i < iterations; ++i ) {

				points[i] = f ( from + (float) i / ( iterations - 1 ) * ( to - from ) );
			}

			Plot ( points, postprocessVertex, normal );
		}

		public void Plot( IList <Vector3> points, System.Func <Vector3, Vector3> postprocessVertex, Vector3 normal ) {

			RebuildMeshCache( points.Count );

			if ( points.Count < 2 ) {

				return;
			}

			//var vertices = new Vector3[points.Count * 2];
			//var normals = new Vector3[points.Count * 2];

			var edge = Vector3.zero;

			var edgeLength = -1f;

			for ( var i = 0; i < points.Count; ++i ) {
				if ( i > 0 && i < points.Count - 1 ) {
					edge = Vector3.Cross( points[i + 1] - points[i - 1], normal );
				} else
					if ( i == 0 )
						edge = Vector3.Cross( points[i + 1] - points[i], normal );
					else
						edge = Vector3.Cross( points[i] - points[i - 1], normal );

				if ( edgeLength == -1f ) {

					edgeLength = width * .5f / edge.magnitude;
				}

				edge *= edgeLength;

				vertices[i * 2] = postprocessVertex( points[i] + edge );
				vertices[i * 2 + 1] = postprocessVertex( points[i] - edge );

				//normals[i] = normal;
			}

			meshFilter.sharedMesh.vertices = vertices;
			//meshFilter.sharedMesh.normals = normals;

			meshFilter.sharedMesh.RecalculateBounds();
			meshFilter.sharedMesh.RecalculateNormals();
		}

		private void RebuildMeshCache( int iterations ) {

			if ( this.iterations == iterations ) {

				return;
			}

			this.iterations = iterations;

			vertices = new Vector3[iterations * 2];
			triangles = new int[iterations * 6];
			uv = new Vector2[iterations * 2];

			var uvStep = 2f / ( vertices.Length - 1 );

			for ( var i = 0; i < iterations; ++i ) {
				uv[i * 2].Set( i * uvStep, 0 );
				uv[i * 2 + 1].Set( i * uvStep, 1 );
			}

			for ( var i = 0; i < iterations - 1; ++i ) {

				var doubleI = 2 * i;

				triangles[i * 6] = doubleI;
				triangles[i * 6 + 1] = doubleI + 2;
				triangles[i * 6 + 2] = doubleI + 3;

				triangles[i * 6 + 3] = doubleI;
				triangles[i * 6 + 4] = doubleI + 3;
				triangles[i * 6 + 5] = doubleI + 1;
			}

			meshFilter.sharedMesh.vertices = vertices;
			meshFilter.sharedMesh.triangles = triangles;
			meshFilter.sharedMesh.uv = uv;
		}
	}

	public class BezierCurve {
		private Vector3[] points = null;

		public BezierCurve( Vector3[] points ) {
			this.points = points;
		}

		public Vector3 Evaluate( float t ) {
			var point = Vector3.zero;

			var n = points.Length - 1; // Looped because degree didn't match last point's index
			for ( var i = 0; i < n + 1; ++i )
				point += BinomialCoefficient( n, i ) * Mathf.Pow( 1f - t, n - i ) * Mathf.Pow( t, i ) * points[i];

			return point;
		}

		private static float BinomialCoefficient( float n, float i ) {
			return Factorial( n ) / ( Factorial( i ) * Factorial( n - i ) );
		}

		private static float Factorial( float n ) {
			var factorial = 1f;

			for ( var i = 2; i <= n; ++i )
				factorial *= i;

			return factorial;
		}
	}
}