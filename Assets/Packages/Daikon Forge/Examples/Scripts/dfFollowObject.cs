using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[AddComponentMenu( "Daikon Forge/Examples/General/Follow Object" )]
public class dfFollowObject : MonoBehaviour
{

	public Camera mainCamera;
	public GameObject attach;

	/// <summary>
	/// Gets or sets control positioning in relation to the target GameObject.
	/// </summary>
	//[Tooltip("Gets or sets control positioning in relation to the target GameObject.")]
	public dfPivotPoint anchor = dfPivotPoint.MiddleCenter;

	public Vector3 offset;
	public float hideDistance = 20;
	public float fadeDistance = 15;
	public bool constantScale = false;

	/// <summary>
	/// If true, the control should never leave the viewable screen area.
	/// </summary>
	//[Tooltip("If true, the control should never leave the viewable screen area.")]
	public bool stickToScreenEdge = false;

	private Transform controlTransform;
	private Transform followTransform;

	private dfControl myControl;
	private dfGUIManager manager;
	private Vector2 lastPosition = Vector2.one * float.MinValue;

	public void Setup( GameObject anchor, Camera mainCamera ) {

		this.attach = anchor;
		this.mainCamera = mainCamera;

		enabled = true;
	}

	void OnEnable()
	{

		if( mainCamera == null )
		{
			mainCamera = Camera.main;
			if( mainCamera == null )
			{
				Debug.LogError( "dfFollowObject component is unable to determine which camera is the MainCamera", gameObject );
				this.enabled = false;
				return;
			}
		}

		myControl = GetComponent<dfControl>();
		if( myControl == null )
		{
			Debug.LogError( "No dfControl component on this GameObject: " + gameObject.name, gameObject );
			this.enabled = false;
		}

		if( myControl == null || attach == null )
		{
			Debug.LogWarning( "Configuration incomplete: " + this.name );
			this.enabled = false;
			return;
		}

		controlTransform = myControl.transform;
		followTransform = attach.transform;

		// Obtain a reference to the dfGUIManager rendering the control
		manager = myControl.GetManager();

		//dfFollowObjectSorter.Register( this );
	}

	void Start() {

		myControl.IsVisibleChanged += IsVisibleChanged;
	}

	void OnDestroy() {

		myControl.IsVisibleChanged -= IsVisibleChanged;
	}

	private void IsVisibleChanged( dfControl control, bool value ) {

		//enabled = value;
	}

	void OnDisable()
	{
		//dfFollowObjectSorter.Unregister( this );
	}

	void Update()
	{

		var targetPosition = followTransform.position;

		var cameraDistance = Vector3.Dot( targetPosition - mainCamera.transform.position, mainCamera.transform.forward );// Vector3.Distance( mainCamera.transform.position, targetPosition ));
		var showControl = cameraDistance < hideDistance && cameraDistance > float.Epsilon;
		if( cameraDistance > hideDistance )
		{
		//	// Hide the control after a given distance
		//	myControl.Opacity = 0f;
		//	return;
		//}
		//else if( cameraDistance > fadeDistance )
		//{
		//	// Apply fade 
		//	myControl.Opacity = 1.0f - ( cameraDistance - fadeDistance ) / ( hideDistance - fadeDistance );
		//}
		//else
		//{
		//	// Fully visible
		//	myControl.Opacity = 1.0f;
		}

		// Calculate 3D point of attachment
		var offsetPoint = followTransform.position + offset;

		// Convert world point to resolution-independant screen point
		var screenPoint = manager.ScreenToGui( mainCamera.WorldToScreenPoint( offsetPoint ) );

		// Calulate resolution adjustment
		if( !manager.PixelPerfectMode )
		{
			if ( constantScale ) {
				
				controlTransform.localScale = Vector3.one * ( manager.FixedHeight / mainCamera.pixelHeight );
			}
			//else
			//	controlTransform.localScale = Vector3.one;
		}

		// Place control over the followed object at designated anchor
		var anchoredPosition = getAnchoredControlPosition();

		screenPoint.x -= anchoredPosition.x * controlTransform.localScale.x;
		screenPoint.y -= anchoredPosition.y * controlTransform.localScale.y;

		var screenSize = manager.GetScreenSize();

		if ( stickToScreenEdge ) {

			screenPoint.x = Mathf.Clamp( screenPoint.x, 0, screenSize.x - myControl.Width );
			screenPoint.y = Mathf.Clamp( screenPoint.y, 0, screenSize.y - myControl.Height );
		}
		else {

			//var controlSize = myControl.Size;

			//showControl = ( ( screenPoint.y > -controlSize.y && screenPoint.y < screenSize.y + controlSize.y ) &&
			//							   ( screenPoint.x > -controlSize.x && screenPoint.x < screenSize.x + controlSize.x ) );

		}

		//myControl.IsVisible = showControl;

		// Don't bother trying to update if nothing has changed 
		if( !showControl || ( screenPoint - lastPosition ).sqrMagnitude <= 0.25f )
			return;

		// Cache the last screen position used, so that it can be determined whether there 
		// is a need to update the control's position, which is an expensive operation
		lastPosition = screenPoint;

		// Do not render the control if the object being followed is not in front of the camera
		//var inFrontOfCamera = cameraDistance > float.Epsilon;
		//if( !inFrontOfCamera )
		//{
		//	myControl.IsVisible = false;
		//	return;
		//}
		//else
		//{
		//	myControl.IsVisible = true;
		//}

		// Position control on screen
		myControl.RelativePosition = screenPoint;

	}

	private Vector2 getAnchoredControlPosition()
	{
		var top = myControl.Height;
		var center = myControl.Width / 2;
		const int bottom = 0;

		var left = myControl.Width;
		var middle = myControl.Height / 2;
		const int right = 0;

		var vector = new Vector2();

		switch ( anchor )
		{
			case dfPivotPoint.TopLeft:
				vector.x = left;
				vector.y = top;
				break;
			case dfPivotPoint.TopCenter:
				vector.x = center;
				vector.y = top;
				break;
			case dfPivotPoint.TopRight:
				vector.x = right;
				vector.y = top;
				break;

			case dfPivotPoint.MiddleLeft:
				vector.x = left;
				vector.y = middle;
				break;
			case dfPivotPoint.MiddleCenter:
				vector.x = center;
				vector.y = middle;
				break;
			case dfPivotPoint.MiddleRight:
				vector.x = right;
				vector.y = middle;
				break;

			case dfPivotPoint.BottomLeft:
				vector.x = left;
				vector.y = bottom;
				break;
			case dfPivotPoint.BottomCenter:
				vector.x = center;
				vector.y = bottom;
				break;
			case dfPivotPoint.BottomRight:
				vector.x = right;
				vector.y = bottom;
				break;
		}

		return vector;
	}
}
