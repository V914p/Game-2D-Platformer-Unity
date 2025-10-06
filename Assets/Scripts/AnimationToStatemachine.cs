using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{

	public AttackState attackState;
	private void TriggerAttach()
	{
		attackState.TriggerAttack();
	}
	private void FinishAttack()
	{
		attackState.FinishAttack();
	}
}
