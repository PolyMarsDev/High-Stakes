using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI;
using Utils;

public class GameMaster : MonoBehaviour {
	public static GameMaster Instance;

	public enum State {
		BEGIN,
		PLAYER_TURN,
		ENEMY_TURN
	}

	public State TurnState;
	public Unit CurrentUnit;
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

	[Header("Prefabs References")]
	public GameObject LevelClear;
	public GameObject GameOver;

	[Header("Level Music")]
	public GameObject LevelMusic;

	[Header("Scene References")]
	[SerializeField] SceneReference GameOverScene;

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
		TurnState = State.BEGIN;

		// Run some initialization code
		TurnState = State.PLAYER_TURN;
		Keys = 0;
		Blood = StartingBlood;

		while (CustomGrid.Instance.Player && !WinConditionSatisfied()) {
			if (TurnState == State.PLAYER_TURN) 		yield return _PlayerTurn();
			else if (TurnState == State.ENEMY_TURN)		yield return _EnemyTurn();
			if (Blood == 0) break;
			TurnState = State.PLAYER_TURN == TurnState ? State.ENEMY_TURN : State.PLAYER_TURN;
		}
		if (WinConditionSatisfied()) yield return OnWin();
		else yield return OnLose();
    }

	public IEnumerator OnWin() {
		Destroy(LevelMusic);
		GameObject.Instantiate(LevelClear, CustomGrid.Instance.Door.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(6f);
		SceneManager_.Instance.LoadScene(SceneManager_.Instance.GetActiveScene() + 1);
	}

	public IEnumerator OnLose() {
		Destroy(LevelMusic);
		GameObject.Instantiate(GameOver, CustomGrid.Instance.Door.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(6f);
		SceneManager_.Instance.LoadScene(GameOverScene.sceneIndex);
	}

	public bool WinConditionSatisfied() => Keys == KeysRequired && CustomGrid.Instance.Door && CustomGrid.Instance.Player 
		&& player.pos == CustomGrid.Instance.Door.pos;

	bool _moveSelected = false;
	bool _specialMove = false;
	List<Vector2Int> _possibleMoveLocations = new List<Vector2Int>();
	List<GameObject> _activeUIIndicators_PossibleMoves = new List<GameObject>();
	Vector2Int _selectedMove;

	void OnMouseDown() {
		// Have a button that you click on to toggle normal/special move? Pseudocode below.
		// if (click on button and not _specialMove) {
		//	this.switchIndicators();
		// }
		// else if (click on button and _specialMove) {
		//	this.switchIndicators();
		// }
		if (TurnState == State.PLAYER_TURN && !_moveSelected) {
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
		foreach (GameObject obj in _activeUIIndicators_PossibleMoves)
			Destroy(obj);
		_activeUIIndicators_PossibleMoves.Clear();

		if (toSpecial) 	_possibleMoveLocations = player.GetSpecialMoveTo();
		else			_possibleMoveLocations = player.GetMoveTo();
		foreach (Vector2Int pos in _possibleMoveLocations)
			_activeUIIndicators_PossibleMoves.Add(
				GridUI.Instance.AddIndicator(GridUI.Indicator.MOVABLE, GridUI.GridToUI(pos)
				- Vector3.forward * .1f) // this is neccessary to prevent z-fighting
			);

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
		CurrentUnit = player;
		PlayerMoveIndicator Indicator = FindObjectOfType<PlayerMoveIndicator>(); // Inefficient as hell so fix if needed
		PlayerCursorTracker CursorTracker = PlayerCursorTracker.Instance;
		EnemyInfoDisplayTracker DisplayTracker = EnemyInfoDisplayTracker.Instance;

		if (Indicator) {
			Indicator.Active = true;
			// check not needed 
			if (Indicator.mode != PlayerMoveIndicator.Mode.Normal) Indicator.mode = PlayerMoveIndicator.Mode.Normal;
		}
		if (CursorTracker) CursorTracker.Active = true;
		if (DisplayTracker) DisplayTracker.Active = true;

		// Render UI
		_specialMove = false;
		_possibleMoveLocations = player.GetMoveTo();
		foreach (Vector2Int pos in _possibleMoveLocations)
			_activeUIIndicators_PossibleMoves.Add(
				GridUI.Instance.AddIndicator(GridUI.Indicator.MOVABLE, GridUI.GridToUI(pos)
				- Vector3.forward * .1f) // this is neccessary to prevent z-fighting
			);
		_moveSelected = false;

		while (!_moveSelected) 
			yield return null;

		if (Indicator) Indicator.Active = false;
		if (CursorTracker) CursorTracker.Active = false;
		if (DisplayTracker) DisplayTracker.Active = false;

		foreach (GameObject obj in _activeUIIndicators_PossibleMoves)
			Destroy(obj);
		_activeUIIndicators_PossibleMoves.Clear();

		if (!_specialMove)	Blood -= NormalBloodCost;
		else				Blood -= SpecialBloodCost;

		bool isCapture = CustomGrid.Instance.GetUnitAt(_selectedMove) is Enemy;
		yield return StartCoroutine(CustomGrid.Instance.MoveUnit(player.pos, _selectedMove));
		if (isCapture) 		Blood += CaptureBloodGain;
		if (CustomGrid.Instance.HasKey(player.pos)) {
			Keys++;
			Key key = CustomGrid.Instance.GetKey(player.pos);
			key.TriggerCollection();
		}
	}
	IEnumerator _EnemyTurn() {
		List<Enemy> Enemies = new List<Enemy>(CustomGrid.Instance.Enemies); 
		foreach (Enemy enemy in Enemies) {
			CurrentUnit = enemy;

			Vector2Int nxt = enemy.GetBestMove();
			if (nxt != enemy.pos) yield return StartCoroutine(CustomGrid.Instance.MoveUnit(enemy.pos, nxt));
		}
		yield return null;
	}
}