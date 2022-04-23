using UnityEngine;
using Utils;

[RequireComponent(typeof(Animator))]
public class TrackCamDir : MonoBehaviour {
	public Animator Anim {get; private set; }
	public Optional<CircularRotationOnInput> CameraProvider;

	int front;

	void Awake() {
		Anim = GetComponent<Animator>();

		Vector2 vec = new Vector2(transform.right.x, transform.right.z);
		float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
		front = (int) Mathf.Round(angle / 90f);

		if (!CameraProvider.Enabled)
			CameraProvider = new Optional<CircularRotationOnInput>(FindObjectOfType<CircularRotationOnInput>());
	}

	int wrapAngle {
		get {
			int rot = CameraProvider.Value.rotationAngle - front;
			if (rot < 0) rot += (int) Mathf.Ceil(-rot / 4f) * 4;
			return rot % 4;
		}
	}

	void Update() {
		Anim.SetFloat("CameraDir",  wrapAngle);
	}
}