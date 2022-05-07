using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UI {
	public class EnemyInfoDisplayTracker : MonoBehaviour {
		public static EnemyInfoDisplayTracker Instance;

		bool _active;
		public bool Active {
			get => _active;
			set {
				_active = value;
				if (!_active) {
					ClearTrackers();
					currentlyTracking = null;
				}
			}
		}

		void Awake() {
			Instance = this;
		}

		Enemy currentlyTracking = null;
		List<GameObject> UITrackingObjects = new List<GameObject>();

		void LateUpdate() {
			if (CustomGrid.Instance && Active) {
				Vector2Int pos = CustomGrid.Instance.GetMouseGridPosition();
				if (CustomGrid.Instance.ValidSquare(pos) && CustomGrid.Instance.GetUnitAt(pos) is Enemy) {
					Enemy enemy = CustomGrid.Instance.GetUnitAt(pos) as Enemy;
					if (enemy != currentlyTracking) {
						ClearTrackers();
						GetNewTrackers(enemy);
					}
					currentlyTracking = enemy;
				} else ClearTrackers();
			} 
		}

		public void ClearTrackers() {
			foreach (var tracker in UITrackingObjects)
				Destroy(tracker.gameObject);
			UITrackingObjects.Clear();
		}

		public void GetNewTrackers(Enemy enemy) {
			foreach (var pos in enemy.GetAttackablePositions()) {
				UITrackingObjects.Add(
					GridUI.Instance.AddIndicator(GridUI.Indicator.MOVABLE, GridUI.GridToUI(pos)
					- Vector3.forward * .2f) // this is neccessary to prevent z-fighting
				);
			}
			foreach (var pos in enemy.GetVisionPositions()) {
				UITrackingObjects.Add(
					GridUI.Instance.AddIndicator(GridUI.Indicator.MOVABLE, GridUI.GridToUI(pos)
					- Vector3.forward * .2f) // this is neccessary to prevent z-fighting
				);
			}
		}
	}
}