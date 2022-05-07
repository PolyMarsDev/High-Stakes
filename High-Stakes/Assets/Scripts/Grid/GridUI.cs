using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridUI : MonoBehaviour
{
	public static GridUI Instance;

	public Optional<Canvas> WorldCanvas;

	void Awake() {
		Instance = this;

		if (!WorldCanvas.Enabled) WorldCanvas = new Optional<Canvas>(GetComponentInChildren<Canvas>());
		if (WorldCanvas.Value == null) Debug.LogError("ReferenceError: A suitable canvas is not found to display UI");
	}

	public GameObject AddIndicator(Indicator indicator, Vector3 pos) {
		return AddIndicator(indicator.GetIndicatorPrefab(this), pos);
	}

	GameObject AddIndicator(GameObject prefab, Vector3 pos) {
		// TODO: Use object pooling to optimize this, but probably not needed for small game

		GameObject obj = GameObject.Instantiate(prefab, pos, Quaternion.identity);
		if (WorldCanvas.Value) {
			obj.transform.SetParent(WorldCanvas.Value.transform, false);
			obj.transform.localRotation = Quaternion.identity;
		}

		return obj;
	}

	public static Vector3 GridToUI(Vector2Int pos) {
		return new Vector3(
			pos.x + .5f,
			pos.y + .5f,
			0
		);
	}

	public enum Indicator {
		MOVABLE,
		ATTACKABLE,
		VISIBLE
	}
	public GameObject MoveableIndicatorPrefab;
	public GameObject AttackableIndicatorPrefab;
	public GameObject VisibleIndicatorPrefab;
}

public static class IndicatorExtension {
	public static GameObject GetIndicatorPrefab(this GridUI.Indicator ind, GridUI UI) {
		switch (ind) {
			case GridUI.Indicator.MOVABLE:
				return UI.MoveableIndicatorPrefab;
			case GridUI.Indicator.ATTACKABLE:
				return UI.AttackableIndicatorPrefab;
			case GridUI.Indicator.VISIBLE:
				return UI.VisibleIndicatorPrefab;
		}
		return null;
	}
}
