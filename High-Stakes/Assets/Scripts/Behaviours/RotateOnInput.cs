using UnityEngine;
using UnityEngine.InputSystem;

public class RotateOnInput : MonoBehaviour {
	public void OnRotate(InputValue value) {
		float rotate = value.Get<float>();
		if (rotate != 0) 		transform.Rotate(new Vector3(0, 90.0f * rotate, 0), Space.World);
	}
}