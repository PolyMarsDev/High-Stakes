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
<<<<<<< HEAD
<<<<<<< HEAD
				} else ClearTrackers();
=======
=======
>>>>>>> e6c98e3773fe035ae478a3b940fa4c04f7ca4709
				} else {
					ClearTrackers();
					currentlyTracking = null;
				}
<<<<<<< HEAD
>>>>>>> e6c98e3773fe035ae478a3b940fa4c04f7ca4709
=======
>>>>>>> e6c98e3773fe035ae478a3b940fa4c04f7ca4709
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
<<<<<<< HEAD
<<<<<<< HEAD
					GridUI.Instance.AddIndicator(GridUI.Indicator.MOVABLE, GridUI.GridToUI(pos)
					- Vector3.forward * .2f) // this is neccessary to prevent z-fighting
=======
					GridUI.Instance.AddIndicator(GridUI.Indicator.ATTACKABLE, GridUI.GridToUI(pos)
					- Vector3.forward * .3f) // this is neccessary to prevent z-fighting
>>>>>>> e6c98e3773fe035ae478a3b940fa4c04f7ca4709
=======
					GridUI.Instance.AddIndicator(GridUI.Indicator.ATTACKABLE, GridUI.GridToUI(pos)
					- Vector3.forward * .3f) // this is neccessary to prevent z-fighting
>>>>>>> e6c98e3773fe035ae478a3b940fa4c04f7ca4709
				);
			}
			foreach (var pos in enemy.GetVisionPositions()) {
				UITrackingObjects.Add(
<<<<<<< HEAD
<<<<<<< HEAD
					GridUI.Instance.AddIndicator(GridUI.Indicator.MOVABLE, GridUI.GridToUI(pos)
=======
					GridUI.Instance.AddIndicator(GridUI.Indicator.VISIBLE, GridUI.GridToUI(pos)
>>>>>>> e6c98e3773fe035ae478a3b940fa4c04f7ca4709
=======
					GridUI.Instance.AddIndicator(GridUI.Indicator.VISIBLE, GridUI.GridToUI(pos)
>>>>>>> e6c98e3773fe035ae478a3b940fa4c04f7ca4709
					- Vector3.forward * .2f) // this is neccessary to prevent z-fighting
				);
			}
		}
	}
}