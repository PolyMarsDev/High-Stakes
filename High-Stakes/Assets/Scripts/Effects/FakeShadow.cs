using UnityEngine;

namespace Carthul.Effects {
	[RequireComponent(typeof(SpriteRenderer))]
	public class FakeShadow : MonoBehaviour {
		public LayerMask GroundMask;
		SpriteRenderer Sprite;

		void Awake() {
			Sprite = GetComponent<SpriteRenderer>();
		}

		void Update() {
			RaycastHit hit;
			if (Physics.Raycast(transform.parent.position + Vector3.up * 1f, Vector3.down, out hit, 100f, GroundMask)) {
				Sprite.enabled = true;
				transform.position = hit.point + Vector3.up * .01f;
				transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			} else {
				Sprite.enabled = false;
			}
		}
	}
}