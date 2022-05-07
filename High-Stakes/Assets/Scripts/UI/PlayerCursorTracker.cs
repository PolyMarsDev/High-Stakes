using UnityEngine;
using UnityEngine.UI;

namespace UI {
	[RequireComponent(typeof(Image))]
	public class PlayerCursorTracker : MonoBehaviour {
		public static PlayerCursorTracker Instance;

		Image Image;

		bool _active;
		public bool Active {
			get => _active;
			set {
				_active = value;
				if (Image) Image.gameObject.SetActive(value);
			}
		}

		void Awake() {
			Image = GetComponent<Image>();
			Instance = this;
		}

		void Update() {
			if (CustomGrid.Instance && Active) {
				Vector3 position = CustomGrid.Instance.GetMouseWorldPosition();
				if (CustomGrid.Instance.ValidSquare(CustomGrid.Instance.SnapCoordinate(position))) {
					position.x = _roundToHalf(position.x);
					position.z = _roundToHalf(position.z);
					position.y += .2f;
					Image.enabled = true;
					transform.position = position;
				} else Image.enabled = false;
			} else Image.enabled = false;
		}

		float _roundToHalf(float x) => Mathf.Round(x + .5f) - .5f;
	}
}