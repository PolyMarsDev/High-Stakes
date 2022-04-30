using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Key : MonoBehaviour {
	[HideInInspector] public Vector2Int pos;
	public UnityEvent OnCollect;

	void OnEnable() {
		if (CustomGrid.Instance != null)
			pos = CustomGrid.Instance.SnapCoordinate(transform.position);
		CustomGrid.Instance?.RegisterKey(this);
	}

	public void TriggerCollection() {
		StartCoroutine(Collect());
	}

	IEnumerator Collect() {
		yield return null;
		OnCollect?.Invoke();
		CustomGrid.Instance?.RemoveKey(this);
	}
}
