using System.Collections.Generic;

public class Average {
	public float value {
		get {
			var result = 0f;
			foreach ( var each in values )
				result += each;

			return values.Count != 0 ? result / values.Count : 0;
		}
	}

	public int count {
		get {
			return values.Count;
		}
	}

	private List<float> values = new List<float>();

	public void Add( float aValue ) {
		values.Add( aValue );
	}
}
