using UnityEngine;
using System.Collections;

public class JumpEventCallback : StateMachineBehaviour {
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateExit(animator, stateInfo, layerIndex);

		animator.GetComponent<BeatmapViewHand>().NotifyJumpEnd();
	}
}
