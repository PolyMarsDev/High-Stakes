using UnityEngine;
using Utils;

[RequireComponent(typeof(Animator))]
public class TrackCamDir : MonoBehaviour {
	public Animator Anim {get; private set; }
	public Optional<CircularRotationOnInput> CameraProvider;

	static float turningLambda = 2f;
	float currentAngle;
	int front => Mathf.RoundToInt(currentAngle / 90f);

	void Awake() {
		Anim = GetComponent<Animator>();

		Vector2 vec = new Vector2(transform.right.x, transform.right.z);
		float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
		currentAngle = angle;

		if (!CameraProvider.Enabled)
			CameraProvider = new Optional<CircularRotationOnInput>(FindObjectOfType<CircularRotationOnInput>());
	}

	int wrapAngle {
		get {
			int rot = CameraProvider.Value.rotationAngle - front;
			if (rot < 0) rot += Mathf.CeilToInt(-rot / 4f) * 4;
			return rot % 4;
		}
	}

	bool overriden;
	float destinationRot;

	public void FixRotationTo(float degree) {
		overriden = true;
		destinationRot = degree;
	}

	public void ReleaseRotation() => overriden = false;

	void LateUpdate() {
		if (overriden) 	currentAngle = Calc.Damp(currentAngle, destinationRot, turningLambda, Time.deltaTime);
		else 			currentAngle = Calc.Damp(currentAngle, CameraProvider.Value.rotationAngle * 90f, turningLambda, Time.deltaTime);
		Anim.SetFloat("CameraDir",  wrapAngle);
	}
}