using UnityEngine;
using UnityEngine.UI;
using Utils;

public class PlayerMoveIndicator : MonoBehaviour {
	public enum Mode {
		Normal,
		Special
	}

	public Sprite NormalSprite;
	public Sprite SpecialSprite;

	bool _active;
	public bool Active {
		get => _active;
		set {
			if (value) mode = Mode.Normal; // defaults to normal upon activation
			_active = value;
			if (Image.Enabled) Image.Value.gameObject.SetActive(value);
		}
	}

	Mode _mode;
	public Mode mode {
		get => _mode;
		set {
			if (value != _mode) {
				if (Image.Enabled) Image.Value.sprite = (value == Mode.Normal) ? NormalSprite : SpecialSprite;
				if (Active) {
					// Play effect too, if possible
				}
			}
			_mode = value;
		}
	}

	public Optional<Image> Image;

	void Awake() {
		if (!Image.Enabled) Image = new Optional<Image>(GetComponentInChildren<Image>());
	}

	public void SwitchIndicator() {
		mode = (mode == Mode.Normal) ? Mode.Special : Mode.Normal;
		GameMaster.Instance?.SwitchIndicators(Mode.Special == mode);
	}
}