using UnityEngine;
using System.Collections;

[System.Serializable]
public class OffsetValue {

	public enum OffsetValueType {

		Constant,
		Rate
	}

	[SerializeField]
	private float value;

	[SerializeField]
	private OffsetValueType valueType;

	public OffsetValue( float value, OffsetValueType valueType ) {

		this.value = value;
		this.valueType = valueType;
	}

	public float Add( float sourceValue ) {

		switch ( valueType ) {

			case OffsetValueType.Rate:
				return sourceValue * value;

			default:
			case OffsetValueType.Constant:
				return sourceValue + value;
		}
	}

	public float Remove( float sourceValue ) {

		switch ( valueType ) {

			case OffsetValueType.Rate:
				return sourceValue / value;

			default:
			case OffsetValueType.Constant:
				return sourceValue - value;
		}
	}

	public OffsetValueType GetValueType() {

		return valueType;
	}

	public float GetValue() {

		return value;
	}
}
