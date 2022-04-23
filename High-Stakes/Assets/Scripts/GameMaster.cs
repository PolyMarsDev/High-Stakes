using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {
	public static GameMaster Instance;

	public enum State {
		BEGIN,
		PLAYER_TURN,
		ENEMY_TURN
	}

	State state;
	Player player;

	[Range(1, 30)]
	public int StartingBlood;
	public int Blood {get; private set; }

	[Range(0, 3)]
	public int KeysRequired;
	public int Keys {get; private set; }

	[Range(1, 10)]
	public int NormalBloodCost = 1;
	[Range(1, 10)]
	public int SpecialBloodCost = 3;
	[Range(1, 10)]
	public int CaptureBloodGain = 3;

	void Awake() {
		player = FindObjectOfType<Player>();
		if (!player) throw new System.Exception("GameMaster: There's no player in this scene. What's my purpose then?");
		Instance = this;
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
		Blood = StartingBlood;

		while (CustomGrid.Instance.Player && !WinConditionSatisfied()) {
			if (state == State.PLAYER_TURN) 		yield return _PlayerTurn();
			else if (state == State.ENEMY_TURN)		yield return _EnemyTurn();
			state = State.PLAYER_TURN == state ? State.ENEMY_TURN : State.PLAYER_TURN;
		}
		if (WinConditionSatisfied()) SceneManager_.Instance.LoadScene(SceneManager_.Instance.GetActiveScene() + 1);
		else SceneManager_.Instance.LoadScene(4);
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
	public void SwitchIndicators(bool toSpecial) {
		if (_specialMove == toSpecial) return;

		// refractor a bit
		foreach (Vector2Int pos in _possibleMoveLocations)
			GridUI.Instance.removeIndicator(pos);
		if (toSpecial) 	_possibleMoveLocations = player.GetSpecialMoveTo();
		else			_possibleMoveLocations = player.GetMoveTo();
		foreach (Vector2Int pos in _possibleMoveLocations)
			GridUI.Instance.addIndicator(GridUI.Indicator.MOVABLE, pos);

		// if (!_specialMove) {
		// 	_possibleMoveLocations = player.GetSpecialMoveTo();
		// 	foreach (Vector2Int pos in _possibleMoveLocations)
		// 		GridUI.Instance.removeIndicator(pos);
		// 	_possibleMoveLocations = player.GetMoveTo();
		// 	foreach (Vector2Int pos in _possibleMoveLocations)
		// 		GridUI.Instance.addIndicator(GridUI.Indicator.MOVABLE, pos);
		// }
		// else {
		// 	_possibleMoveLocations = player.GetMoveTo();
		// 	foreach (Vector2Int pos in _possibleMoveLocations)
		// 		GridUI.Instance.removeIndicator(pos);
		// 	_possibleMoveLocations = player.GetSpecialMoveTo();
		// 	foreach (Vector2Int pos in _possibleMoveLocations)
		// 		GridUI.Instance.addIndicator(GridUI.Indicator.MOVABLE, pos);
		// }
		_specialMove = toSpecial;
	}

	IEnumerator _PlayerTurn() {
		PlayerMoveIndicator Indicator = FindObjectOfType<PlayerMoveIndicator>(); // Inefficient as hell so fix if needed

		if (Indicator) {
			Indicator.Active = true;
			// check not needed 
			if (Indicator.mode != PlayerMoveIndicator.Mode.Normal) Indicator.mode = PlayerMoveIndicator.Mode.Normal;
		}

		// Render UI
		_specialMove = false;
		_possibleMoveLocations = player.GetMoveTo();
		foreach (Vector2Int pos in _possibleMoveLocations)
			GridUI.Instance.addIndicator(GridUI.Indicator.MOVABLE, pos);
		_moveSelected = false;

		while (!_moveSelected) 
			yield return null;

		if (Indicator) Indicator.Active = false;
		foreach (Vector2Int pos in _possibleMoveLocations)
			GridUI.Instance.removeIndicator(pos);

		if (!_specialMove)	Blood -= NormalBloodCost;
		else				Blood -= SpecialBloodCost;

		bool isCapture = CustomGrid.Instance.GetUnitAt(_selectedMove) is Enemy;
		yield return StartCoroutine(CustomGrid.Instance.MoveUnit(player.pos, _selectedMove));
		if (isCapture) 		Blood += CaptureBloodGain;
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