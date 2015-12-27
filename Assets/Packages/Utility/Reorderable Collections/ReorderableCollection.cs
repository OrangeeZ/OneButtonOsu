using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

#if UNITY_EDITOR

using UnityEditor;
using UnityEditorInternal;

#endif

public class ReorderableCollection {}

public class ReorderableCollectionWrapper<T> : ReorderableCollection {

	public List<T> collection;
}

#if UNITY_EDITOR

[CustomPropertyDrawer( typeof( ReorderableCollection ), useForChildren: true )]
public class ReorderableCollectionDrawer : PropertyDrawer {

	private const string collectionFieldName = "collection";

	private ReorderableList list;

	private SerializedProperty serializedProperty;

	private float offsetX = 20;

	private ReorderableList UpdateList( SerializedProperty property ) {

		if ( list == null ) {

			list = new ReorderableList( property.serializedObject, property, draggable: true, displayHeader: true, displayAddButton: true, displayRemoveButton: true ) {

				drawHeaderCallback = rect => EditorGUI.LabelField( rect, this.serializedProperty.name ),

				drawElementCallback = ( rect, index, isActive, isFocused ) => {

					rect.x += offsetX;
					rect.width -= offsetX;

					if ( index < property.arraySize ) {
						
						EditorGUI.PropertyField( rect, property.GetArrayElementAtIndex( index ), true );
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
