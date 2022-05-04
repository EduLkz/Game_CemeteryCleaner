using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inputs;
public class GameManager : MonoBehaviour {

	#region Singleton
	private static GameManager instance;
	public static GameManager Instance { get { return instance; } }

	void Awake() {
		instance = this;
    }

	#endregion

	[Header("Spawn Settings")]
	[SerializeField] private Bounds spawnBounds;
	[SerializeField] private LayerMask cantSpawn;

	[Space(10)]
	[SerializeField] private float enemySpawnRate = 1.5f;

	[Header("Enemies")]
	[SerializeField] private Transform enemiesParent;

	[Header("Ranged")]
	[SerializeField] private GameObject rangedEnemyPrefab;
	[SerializeField] private int maxRangedEnemies = 19;
	private int currenteRanged;
	[SerializeField] private List<Transform> rangedPoints = new List<Transform>();

	[Header("Melee")]
	[SerializeField] private GameObject meleeEnemyPrefab;
	[SerializeField] private int maxMeleeEnemies = 50;
	private int currenteMelee;

	private float lastEnemySpawn;
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform projectileParent;
	Queue<Arrow> enemyProjectiles = new Queue<Arrow>();


	[Header("Game Stats")]
	[SerializeField] private Text playerPointsTxt;
	[SerializeField] private GameObject initialText;
	[SerializeField] private Text initialTimerTxt;
	[SerializeField] private Text durationTxt;

	private int playerPoints;
	private bool isPaused;

	bool gameStarted;
	float gameDuration;
	float gameMaxDuration = 180;

	Controls controls;

	void Start() {
		playerPointsTxt.text = "Points: " + playerPoints.ToString("#0");
		AddProjectile(10);

        if(PlayerPrefs.HasKey("ContagemInicialSegundos")) {
			StartCoroutine(StartingGame(PlayerPrefs.GetInt("ContagemInicialSegundos")));
		} else {
			StartCoroutine(StartingGame(3));
		}

		if(PlayerPrefs.HasKey("TempoPartidaMin")) {
			gameMaxDuration = PlayerPrefs.GetInt("TempoPartidaMin") * 60f;

			if(PlayerPrefs.HasKey("TempoPartidaSec")) {
				gameMaxDuration += PlayerPrefs.GetInt("TempoPartidaSec");
			}
		} else {
			gameMaxDuration = 180;
        }

		MouseLock(true);

		if(controls == null) {
			controls = new Controls();
		}
		controls.Enable();

		controls.Gameplay.Pause.performed += ctx => TogglePause();
	}

	private void TogglePause() {
		isPaused = !isPaused;

		MouseLock(isPaused);

		Time.timeScale = (isPaused) ? 0 : 1;
	}

	public bool IsPaused() {
		return isPaused;
	}

	void Update() {
		if(!gameStarted || isPaused) { return; }

		if(gameDuration <= gameMaxDuration) { gameDuration += Time.deltaTime; UpdateDurationText(); } 
		else { return; }

		if(lastEnemySpawn < Time.time) {
			if(currenteMelee < maxMeleeEnemies || currenteRanged < maxRangedEnemies)
				SpawnEnemy();
        }
    }


	#region EnemyHandle
	private void SpawnEnemy() {
		float _x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
		float _z = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
		Vector3 _pos = new Vector3(_x, 0, _z);
		RaycastHit hit;

		if(Physics.Raycast(new Vector3(_x, 50f, _z), Vector3.down, 70f, cantSpawn)) {
			return;
        } else if(Physics.Raycast(new Vector3(_x, 50f, _z), Vector3.down, out hit, 70f)) {
			_pos = hit.point;
		}
		lastEnemySpawn = Time.time + enemySpawnRate;

		GameObject go;
		int _i = Random.Range(0, 5);

		if(currenteMelee >= maxMeleeEnemies) { _i = 4; }
		if(currenteRanged >= maxRangedEnemies || rangedPoints.Count <= 0) { _i = 0; }

		if(_i < 3) {
			go = Instantiate(meleeEnemyPrefab);
			currenteMelee++;
        } else{
			go = Instantiate(rangedEnemyPrefab);
			currenteRanged++;
		}

		go.transform.position = _pos;
		go.transform.rotation = Quaternion.identity;
		go.SetActive(true);
	}

	public void MeleeDied() {
		currenteMelee--;
		playerPoints += 10;
		UpdatePointText();
	}

	public void RangedDied(Transform _point) {
		currenteRanged--;
		rangedPoints.Add(_point);
		playerPoints += 15;
		UpdatePointText();
	}

	public Transform GetRangedPoint() {
		int _i = Random.Range(0, rangedPoints.Count);
		
		Transform _t = rangedPoints[_i];

		rangedPoints.Remove(_t);
		return _t;	
    }

	#endregion

	#region Projectile Pool
	public Arrow GetProjectile() {
		if(enemyProjectiles.Count == 0) {
			AddProjectile(1);
		}
		return enemyProjectiles.Dequeue();
	}

	void AddProjectile(int _count) {
		for(int i = 0; i < _count; i++) {
			Arrow _a = Instantiate(projectilePrefab).GetComponent<Arrow>();
			_a.transform.SetParent(projectileParent);
			_a.gameObject.SetActive(false);
			enemyProjectiles.Enqueue(_a);
		}
	}

	public void ReturnToPool(Arrow _projectile) {
		_projectile.gameObject.SetActive(false);
		enemyProjectiles.Enqueue(_projectile);
	}
    #endregion

    #region HUD Handle
    private void UpdatePointText() {
		playerPointsTxt.text = "Points: " + playerPoints.ToString("#0");
	}

	private void UpdateDurationText() {
		durationTxt.text = (gameDuration/60).ToString("00") + ":" + (gameDuration % 60).ToString("00");
    }

	void OnDrawGizmosSelected() {
		Gizmos.DrawWireCube(spawnBounds.center, spawnBounds.size);
	}
    #endregion

    IEnumerator StartingGame(int _sec) {
		initialText.SetActive(true);
		initialTimerTxt.text = _sec.ToString("0");

		for(int i = _sec; i > 0; i--) {
			yield return new WaitForSecondsRealtime(1);
			initialTimerTxt.text = i.ToString("0");
		}
		
		yield return new WaitForSecondsRealtime(1);

		gameStarted = true;
		initialText.SetActive(false);
	}

	void MouseLock(bool _isLocked) {
		Cursor.lockState = (_isLocked) ? CursorLockMode.Locked : CursorLockMode.None;
		Cursor.visible = !_isLocked;
	}
}