using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KeyTracker : MonoBehaviour {
	Image image;

	void Awake() {
		image = GetComponent<Image>();
	}

	void Update() {
		if (GameMaster.Instance) {
			image.color = GameMaster.Instance.Keys == GameMaster.Instance.KeysRequired ?
				Color.white : Color.black;
		}
	}
}