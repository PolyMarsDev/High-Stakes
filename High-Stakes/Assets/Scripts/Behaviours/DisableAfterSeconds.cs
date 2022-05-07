using UnityEngine;
using Utils;

public class DisableAfterSeconds : MonoBehaviour {
	[SerializeField] float duration = 3f;
	Timer alive;
	void OnEnable() {
		alive = duration;
	}
	void Update() {
		if (!alive) gameObject.SetActive(false);
	}
}