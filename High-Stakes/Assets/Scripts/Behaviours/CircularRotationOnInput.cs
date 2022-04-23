using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class CircularRotationOnInput : MonoBehaviour {
	public float currentRotationAngle {get; private set; }
	public int desiredRotation {get; private set; }
	public int rotationAngle => Mathf.RoundToInt(currentRotationAngle / 90f);
	public float dampAlpha = 2f;

	public void OnRotate(InputValue value) {
		float rotate = value.Get<float>();
		if (rotate != 0) {
			desiredRotation += (int) rotate;
			// if (desiredRotation < 0) desiredRotation += 4;
			// if (desiredRotation > 4) desiredRotation -= 4;
		}
	}

	void Update() {
		float desiredLocationAngle = desiredRotation * 90;
		// Because math on angles is annoying
		// if (Mathf.Abs(currentRotationAngle + 360 - desiredLocationAngle) < 
		// 	Mathf.Abs(currentRotationAngle - desiredLocationAngle)) {
		// 	currentRotationAngle += 360;
		// } else if (Mathf.Abs(currentRotationAngle - 360 - desiredLocationAngle) < 
		// 	Mathf.Abs(currentRotationAngle - desiredLocationAngle)) {
		// 	currentRotationAngle -= 360;
		// }
		currentRotationAngle = Calc.Damp(currentRotationAngle, desiredLocationAngle, dampAlpha, Time.deltaTime);
		transform.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
	}

}