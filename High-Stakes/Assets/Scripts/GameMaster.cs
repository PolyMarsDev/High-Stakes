using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public enum State {
		BEGIN,
		PLAYER_TURN,
		ENEMY_TURN
	}

	State state;
	Player player;

	void Awake() {
		
	}

	void OnEnable() {
		StartCoroutine(Gameloop());
	}

	// Please find a better name for this method
	public IEnumerator Gameloop() {
		state = State.BEGIN;

		// Run some initialization code

		state = State.PLAYER_TURN;

		while (true) {
			if (state == State.PLAYER_TURN) 		yield return _PlayerTurn();
			else if (state == State.ENEMY_TURN)		yield return _EnemyTurn();
		}
	}

	IEnumerator _PlayerTurn() {
		// Render UI

		yield return null;
	}

	IEnumerator _EnemyTurn() {
		yield return null;
	}
}