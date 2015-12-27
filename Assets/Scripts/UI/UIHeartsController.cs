using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHeartsController : MonoBehaviour {

	public LivesController livesController;

	public GameObject root;

	public Sprite sprite;

	public int healthCount;

	void Start() {

		var range = Enumerable.Range( 0, healthCount );//.Select( _ => root.AddControl<dfSprite>() ).Map( _ => _.Atlas = ( root as dfScrollPanel ).Atlas ).Map( _ => _.canChangeTransformManually = true ).MapImmediate( _ => _.SpriteName = sprite );

		foreach (var each in range)
		{
			var heart = new GameObject("Heart").AddComponent<Image>();
			heart.transform.SetParent(root.transform, false);
		}
	}

	void OnEnable() {

		livesController.Hit += OnHit;
		livesController.Died += OnHit;
	}

	void OnDisable() {

		livesController.Hit -= OnHit;
		livesController.Died -= OnHit;
	}

	private void OnHit() {

		if (transform.childCount > 0)
		{
			Destroy(transform.GetChild(0).gameObject);
		}

		//if ( root.Controls.Count > 0 ) {

		//	root.Controls.RemoveAt( 0 );

		//	root.ResetLayout( true, true );
		//	root.Invalidate();
		//}
	}
}
