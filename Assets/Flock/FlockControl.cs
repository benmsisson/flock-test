using UnityEngine;
using System.Collections;

public class FlockControl : MonoBehaviour { //Creates the 4 different types of objects, contains them and handles ball respawning
	public static readonly float ROOM_WIDTH = 38; //Actually twice this
	public static readonly float ROOM_HEIGHT = 30;
	private bool makeBoids = true;
	public GameObject prefBoid;
	private int spawnCount = 300;//Total to spawn
	private int birdCount = 0;//Number spawned
	private Flock[] boids;
	private bool makeObs = true;
	public GameObject prefObst;
	private int obstCount = 10;
	private GameObject[] obstacles;
	private static readonly float MIN_SIZE = 3f;
	private static readonly float MAX_SIZE = 10f;
	private bool makeBall = true;
	public GameObject prefBall;
	private GameObject curBall;
	private bool makePred = true;
	public GameObject prefPred;
	private int predCount = 3;
	private Predator[] preds;

	// Use this for initialization
	void Start() {
		if (makeBoids) {
			boids = new Flock[spawnCount];
			for (int i = 0; i < spawnCount; i++) {
				spawn();
			}
		}
		if (makePred) {
			preds = new Predator[predCount];
			for (int i = 0; i < predCount; i++) {
				preds [i] = Instantiate<GameObject>(prefPred).GetComponent<Predator>();
				preds [i].transform.position = new Vector3(Random.Range(-ROOM_WIDTH, ROOM_WIDTH), Random.Range(-ROOM_HEIGHT, ROOM_HEIGHT));
				preds [i].setup(this);
			}
		}
		if (makeObs) {
			obstacles = new GameObject[obstCount];
			for (int i = 0; i < obstCount; i++) {
				obstacles [i] = Instantiate<GameObject>(prefObst);
				float size = Random.Range(MIN_SIZE, MAX_SIZE);
				obstacles [i].transform.localScale = new Vector3(size, size, 1f);
				obstacles [i].transform.position = new Vector3(Random.Range(-ROOM_WIDTH, ROOM_WIDTH), Random.Range(-ROOM_HEIGHT, ROOM_HEIGHT));
			}
		}
		if (makeBall) {
			spawnBall();
		}
	}
	
	// Update is called once per frame
	void Update() { //Just so we can speed up or slow down simulation
		float timeAdjust = .4f;
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Time.timeScale += timeAdjust;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Time.timeScale -= timeAdjust;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			Time.timeScale = 1f;
		}
	}

	void spawn() {
		if (spawnCount > 0) {
			GameObject boid = Instantiate<GameObject>(prefBoid);
			boid.transform.position = new Vector3(Random.Range(-ROOM_WIDTH, ROOM_WIDTH), Random.Range(-ROOM_HEIGHT, ROOM_HEIGHT));
			boids [birdCount] = boid.GetComponent<Flock>();
			boids [birdCount].Setup(this);
			birdCount++;
			spawnCount--;
		}
	}

	public int flockSize() {
		return birdCount;
	}

	public Flock getBoid(int i) {
		return boids [i];
	}

	public int predSize() {
		return predCount;
	}

	public Predator getPred(int i) {
		return preds [i];
	}

	public int obstSize() {
		return obstCount;
	}

	public GameObject getObst(int i) {
		return obstacles [i];
	}

	public Vector3 getBallPos() {
		if (curBall) {
			return curBall.transform.position;
		} else {
			return Vector3.back;
		}
	}

	public void spawnBall() {
		curBall = Instantiate<GameObject>(prefBall);
		curBall.transform.position = new Vector3(Random.Range(-ROOM_WIDTH, ROOM_WIDTH), Random.Range(-ROOM_HEIGHT, ROOM_HEIGHT));
	}
}
