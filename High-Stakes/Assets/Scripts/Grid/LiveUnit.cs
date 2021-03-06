using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine.Events;
using Utils;

[RequireComponent(typeof(Animator))]
public abstract class LiveUnit : Unit {
	public enum State {
		Idle = 0,
		Run = 1, 
		Attack = 2
	}
	State _AnimState = State.Idle;
	public State AnimState {
		get => _AnimState;
		set {
			_AnimState = value;
			Anim?.SetInteger("State", (int) _AnimState);
		}
	}
	public Animator Anim {get; private set; }

	void Awake() {
		Anim = GetComponent<Animator>();
	}

	[Header("Animation")]
	[HorizontalLine(color: EColor.Green)]
	[Range(.1f, 2f), SerializeField] float MoveDuration = 1f;
	[Range(.1f, 2f), SerializeField] float AttackDuration = 1f;
	public UnityEvent OnMove;
	public UnityEvent OnCapture;
	public UnityEvent OnDeath;
	public GameObject Corpse;

	public override IEnumerator MoveTo(Vector2Int pos) {
		Vector3 originalPos = transform.position;
		Vector3 destination = CustomGrid.Instance.GridToWorld((Vector3Int) pos);
		Timer moving = MoveDuration;
		moving.Start();
		OnMove?.Invoke();
		AnimState = State.Run;
		while (moving) {
			Vector3 realPos = Vector3.Lerp(
				originalPos, 
				destination, 
				Easing.EaseInOutCirc(
					1 - moving.TimeLeft / MoveDuration,
					1f
				)
			);
			transform.position = realPos;
			yield return null;
		}
		AnimState = State.Idle;
		this.pos = pos;
	}

	public override IEnumerator Capture(Unit unit) {
		Vector3 originalPos = transform.position;
		Vector3 destination = CustomGrid.Instance.GridToWorld((Vector3Int) unit.pos);
		Timer moving = AttackDuration;
		moving.Start();
		// OnMove?.Invoke();
		OnCapture?.Invoke();
		AnimState = State.Attack;
		while (moving) {
			Vector3 realPos = Vector3.Lerp(
				originalPos, 
				destination, 
				Easing.EaseInOutCirc(
					1 - moving.TimeLeft / AttackDuration,
					1f
				)
			);
			transform.position = realPos;
			yield return null;
		}
		AnimState = State.Idle;
		this.pos = unit.pos;
        unit.Kill();
	}

	public override void Kill() {
		OnDeath?.Invoke();

		Vector2 rpointIncircle = UnityEngine.Random.insideUnitCircle;
		Vector3 corpseOffset = new Vector3(
			rpointIncircle.x,
			0,
			rpointIncircle.y
		) * .4f;
		GameObject corpse = 
			GameObject.Instantiate(
				Corpse, 
				transform.position + corpseOffset,
				Quaternion.identity
			);
		base.Kill();
	}

}