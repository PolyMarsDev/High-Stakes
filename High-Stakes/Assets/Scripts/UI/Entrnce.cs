using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Utils;

public class Entrnce : MonoBehaviour {
	[SerializeField] float Delay = 5f;
	Timer delaying;
	public UnityEvent Ec;

	void Awake() {
		delaying = Delay;
	}

	private void Update() {
		if (!delaying) {

		}
	}

}