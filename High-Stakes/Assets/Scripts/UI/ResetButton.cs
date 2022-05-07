using UnityEngine;

public class ResetButton : MonoBehaviour {
	public void OnClick() {
		FindObjectOfType<SceneManager_>().ReloadScene();
	}
}