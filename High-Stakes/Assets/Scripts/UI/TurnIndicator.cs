using UnityEngine;

namespace UI {
	public class TurnIndicator : MonoBehaviour {
		void Update() {
			if (GameMaster.Instance && GameMaster.Instance.CurrentUnit)
				transform.position = GameMaster.Instance.CurrentUnit.transform.position;
		}
	}
}