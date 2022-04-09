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
	public int blood;

	[Range(0, 3)]
	public int KeysRequired;
	public int Keys {get; private set; }

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
		Keys = 0;

		while (CustomGrid.Instance.Player && !WinConditionSatisfied()) {
			if (state == State.PLAYER_TURN) 		yield return _PlayerTurn();
			else if (state == State.ENEMY_TURN)		yield return _EnemyTurn();
			state = State.PLAYER_TURN == state ? State.ENEMY_TURN : State.PLAYER_TURN;
		}
	}

	public bool WinConditionSatisfied() => Keys == KeysRequired && CustomGrid.Instance.Door && CustomGrid.Instance.Player 
		&& player.pos == CustomGrid.Instance.Door.pos;

	bool _moveSelected = false;
	bool _specialMove = false;
	List<Vector2Int> _possibleMoveLocations;
	Vector2Int _selectedMove;

	void OnMouseDown() {
		// Have a button that you click on to toggle normal/special move? Pseudocode below.
		// if (click on button and not _specialMove) {
		//	this.switchIndicators();
		// }
		// else if (click on button and _specialMove) {
		//	this.switchIndicators();
		// }
		if (state == State.PLAYER_TURN && !_moveSelected) {
			Vector2Int pos = CustomGrid.Instance.GetMouseGridPosition();
			if (_possibleMoveLocations.Contains(pos)) {
				_moveSelected = true;
				_selectedMove = pos;
			}
		}
	}

	void switchIndicators() {
		if (!_specialMove) {
			_possibleMoveLocations = player.GetSpecialMoveTo();
			foreach (Vector2Int pos in _possibleMoveLocations)
				GridUI.Instance.removeIndicator(pos);
			_possibleMoveLocations = player.GetMoveTo();
			foreach (Vector2Int pos in _possibleMoveLocations)
				GridUI.Instance.addIndicator(GridUI.Indicator.MOVABLE, pos);
		}
		else {
			_possibleMoveLocations = player.GetMoveTo();
			foreach (Vector2Int pos in _possibleMoveLocations)
				GridUI.Instance.removeIndicator(pos);
			_possibleMoveLocations = player.GetSpecialMoveTo();
			foreach (Vector2Int pos in _possibleMoveLocations)
				GridUI.Instance.addIndicator(GridUI.Indicator.MOVABLE, pos);
		}
		_specialMove = !_specialMove;
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

		if (!_specialMove)	blood -= 1;
		else				blood -= 2;

		foreach (Vector2Int pos in _possibleMoveLocations)
			GridUI.Instance.removeIndicator(pos);
	}
	IEnumerator _EnemyTurn() {
		List<Enemy> Enemies = new List<Enemy>(CustomGrid.Instance.Enemies); 
		foreach (Enemy enemy in Enemies) {
			Vector2Int nxt = enemy.GetBestMove();
			if (nxt != enemy.pos) yield return StartCoroutine(CustomGrid.Instance.MoveUnit(enemy.pos, nxt));
		}
		yield return null;
	}
}