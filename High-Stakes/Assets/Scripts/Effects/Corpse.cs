using UnityEngine;

public class Corpse : MonoBehaviour {
	[SerializeField] float UpVelocity = 4;

	void OnEnable() {
		GetComponent<Rigidbody>().velocity = Vector3.up * UpVelocity;
	}
}