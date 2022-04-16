using UnityEngine;
using TMPro;
using Utils;

public class BloodCounter : MonoBehaviour {
	int _lastBlood;
	bool _active = false;

	public bool Active {
		get => _active;
		set {
			_active = value;
			foreach (Transform child in transform) 
				child.gameObject.SetActive(value);
		}
	}

	public Optional<TMP_Text> Text;

	void Awake() {
		if (!Text.Enabled) Text = new Optional<TMP_Text>(GetComponentInChildren<TMP_Text>());
	}

	void Update() {
		if (GameMaster.Instance) {
			if (GameMaster.Instance.Blood < _lastBlood) {
				// blood decreased effect
			} else if (GameMaster.Instance.Blood > _lastBlood) {
				// blood increased effect
			}
			_lastBlood = GameMaster.Instance.Blood;
			Text.Value.text = _lastBlood.ToString();
		}
	}
}