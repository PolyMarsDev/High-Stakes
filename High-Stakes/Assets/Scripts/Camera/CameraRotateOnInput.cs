using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CameraRotateOnInput : MonoBehaviour {
	public Animator Anim {get; private set; }
	void Awake() { Anim = GetComponent<Animator>(); }

	public void OnRotate(InputValue value) {
		float rotate = value.Get<float>();
		if (rotate == 1) 		Anim?.SetTrigger("RotatePositive");
		else if (rotate == -1)	Anim?.SetTrigger("RotateNegative");
	}
}