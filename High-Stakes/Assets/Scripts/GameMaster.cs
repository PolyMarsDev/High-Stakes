using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public enum State {
		Begin,
		Player,
		Enemy
	}

	State state;

	// Please find a better name for this method
	public IEnumerator Gameloop() {
		state = State.Begin;


		yield return null;
	}
}