using UnityEngine;

public class CustomGridTest : MonoBehaviour {
	private void OnMouseDown() {
		Debug.Log(CustomGrid.Instance.SnapCoordinate(CustomGrid.Instance.GetMouseWorldPosition()));
	}
}