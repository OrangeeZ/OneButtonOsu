using UnityEngine;
using System.Collections;

namespace Utility {

	public struct TargetPosition {

		public Vector3 Value { get { return targetTransform != null ? targetTransform.position : targetPoint.Value; } }

		public bool HasValue { get { return targetTransform != null || targetPoint != null; } }

		private Transform targetTransform;
		private Vector3? targetPoint;

		public TargetPosition( Transform targetTransform ) {

			this.targetTransform = targetTransform;
			this.targetPoint = null;
		}

		public TargetPosition( Vector3 targetPoint ) {

			this.targetTransform = null;
			this.targetPoint = targetPoint;
		}

		public static implicit operator TargetPosition( Vector3 targetPoint ) {

			return new TargetPosition( targetPoint );
		}

		public static implicit operator TargetPosition( Transform targetTransform ) {

			return new TargetPosition( targetTransform );
		}
	}
}