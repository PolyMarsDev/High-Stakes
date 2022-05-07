using UnityEngine;
using UnityEngine.UI;

namespace UI {
	[RequireComponent(typeof(Image))]
	public class PlayerCursorTracker : MonoBehaviour {
		Image Image;

		void Awake() {
			Image = GetComponent<Image>();
		}

		void Update() {
			if (CustomGrid.Instance && GameMaster.Instance && GameMaster.Instance.TurnState == GameMaster.State.PLAYER_TURN) {
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