using System;
using System.Linq;
using UnityEngine;
using System.Collections;

public class UIHeartsController : MonoBehaviour {

	public LivesController livesController;

	public dfControl root;

	public string spriteName;

	public int healthCount;

	void Start() {

		Enumerable.Range( 0, healthCount ).Select( _ => root.AddControl<dfSprite>() ).Map( _ => _.Atlas = ( root as dfScrollPanel ).Atlas ).Map( _ => _.canChangeTransformManually = true ).MapImmediate( _ => _.SpriteName = spriteName );
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

		if ( root.Controls.Count > 0 ) {

			root.Controls.RemoveAt( 0 );

			root.ResetLayout( true, true );
			root.Invalidate();
		}
	}
}
