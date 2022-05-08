using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DistanceHack : MonoBehaviour {
	Camera m_cam;
	SpriteRenderer Sprite;

	void Awake() {
		if (!m_cam) m_cam = Camera.main;
		Sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		float dist = 300 - (transform.position - m_cam.transform.position).sqrMagnitude;
		Sprite.sortingOrder = Mathf.RoundToInt(dist * 10f);
	}
}