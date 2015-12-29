using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using UnityEngine;

[Serializable]
public class SerializableKeyValuePair<TKey, TValue> {

	public TKey key;

	public TValue value;
}

[Serializable]
public class SerializableDictionary<TKey, TValue, TKVP> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	where TKVP : SerializableKeyValuePair<TKey, TValue>, new() {

	[SerializeField]
	private List<TKVP> keyValuePairs = new List<TKVP>();

	public void OnBeforeSerialize() {

		foreach ( var pair in this ) {

			if ( !keyValuePairs.Any( where => where.key.Equals( pair.Key ) && where.value.Equals( pair.Value ) ) ) {

				keyValuePairs.Add( new TKVP { key = pair.Key, value = pair.Value } );
			}
		}
	}

	public void OnAfterDeserialize() {

		Clear();

		foreach ( var each in keyValuePairs ) {

			this[each.key] = each.value;
		}
	}
}

public class DrawableDictionaryAttribute : PropertyAttribute { }

#if UNITY_EDITOR

[CustomPropertyDrawer( typeof( DrawableDictionaryAttribute ), useForChildren: true )]
public class SerializableDictionaryDrawer : PropertyDrawer {

	private const string collectionFieldName = "keyValuePairs";

	private ReorderableList list;

	private SerializedProperty serializedProperty;

	private float offsetX = 20;

	private ReorderableList UpdateList( SerializedProperty property ) {

		if ( list == null ) {

			list = new ReorderableList( property.serializedObject, property, draggable: false, displayHeader: true, displayAddButton: true, displayRemoveButton: true ) {

				drawHeaderCallback = rect => EditorGUI.LabelField( rect, this.serializedProperty.name ),

				drawElementCallback = ( rect, index, isActive, isFocused ) => {

					rect.x += offsetX;
					rect.width -= offsetX;

					if ( index < property.arraySize ) {

						var element = property.GetArrayElementAtIndex( index );
						var key = element.FindPropertyRelative( "key" );
						var value = element.FindPropertyRelative( "value" );

						var firstRect = rect;
						firstRect.width *= 0.5f;

						var secondRect = firstRect;
						secondRect.x += secondRect.width;

						EditorGUI.PropertyField( firstRect, key, true );
						EditorGUI.PropertyField( secondRect, value, true );
					}
				}
			};
		}

		return list;
	}

	public override float GetPropertyHeight( SerializedProperty property, GUIContent label ) {

		return UpdateList( property.FindPropertyRelative( collectionFieldName ) ).GetHeight();
	}

	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {

		serializedProperty = property;

		var listProperty = property.FindPropertyRelative( collectionFieldName );
		var list = UpdateList( listProperty );

		var height = 0f;

		for ( var i = 0; i < listProperty.arraySize; i++ ) {

			height = Mathf.Max( height, EditorGUI.GetPropertyHeight( listProperty.GetArrayElementAtIndex( i ) ) );
		}

		list.elementHeight = height;
		list.DoList( position );
	}
}

#endif
