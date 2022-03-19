using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {
	public enum State {
		BEGIN,
		PLAYER_TURN,
		ENEMY_TURN
	}

	State state;
	Player player;

	void Awake() {
		player = FindObjectOfType<Player>();
		if (!player) throw new System.Exception("GameMaster: There's no player in this scene. What's my purpose then?");
	}

	void Start() {
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
			state = State.PLAYER_TURN == state ? State.ENEMY_TURN : State.PLAYER_TURN;
		}
	}

	bool _moveSelected = false;
	List<Vector2Int> _possibleMoveLocations;
	Vector2Int _selectedMove;

	void OnMouseDown() {
		if (state == State.PLAYER_TURN && !_moveSelected) {
			Vector2Int pos = CustomGrid.Instance.GetMouseGridPosition();
			Debug.Log(pos);
			if (_possibleMoveLocations.Contains(pos)) {
				_moveSelected = true;
				_selectedMove = pos;
			}
		}
	}

	IEnumerator _PlayerTurn() {
		// Render UI
		_possibleMoveLocations = player.GetMoveTo();
		foreach (Vector2Int pos in _possibleMoveLocations)
			GridUI.Instance.addIndicator(GridUI.Indicator.MOVABLE, pos);
		_moveSelected = false;

		while (!_moveSelected) 
			yield return null;

		yield return StartCoroutine(CustomGrid.Instance.MoveUnit(player.pos, _selectedMove));

		foreach (Vector2Int pos in _possibleMoveLocations)
			GridUI.Instance.removeIndicator(pos);
	}

	IEnumerator _EnemyTurn() {
		yield return null;
	}
}