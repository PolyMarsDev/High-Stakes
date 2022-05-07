using UnityEngine;
using Utils;

public class Wavy : MonoBehaviour {
	public float Amount = 2f;
	public float Period = 1.333f;
	public Optional<float> Offset;
	float timeStart;
	Vector2 original;

	RectTransform rectTransform;

	private void Awake() {
		rectTransform = GetComponent<RectTransform>();
	}

	private void OnEnable() {
		timeStart = Time.time;
		original = rectTransform == null ? (Vector2)transform.localPosition : rectTransform.anchoredPosition;
		if (!Offset.Enabled) Offset = new Optional<float>(Random.Range(0,1f));
	}

	void Update() {
		if (rectTransform != null)
			rectTransform.anchoredPosition = original + Wave(Time.time - timeStart);
		else
			transform.localPosition = original + Wave(Time.time - timeStart);
	}

	public Vector2 Wave(float time) {
		return new Vector2(0f, Mathf.Sin(2 * Mathf.PI * (Offset.Value + time / Period)) * Amount);
	}
}