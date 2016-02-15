using UnityEngine;
using System.Collections;

public class Logic : MonoBehaviour {

	public int enemiesPerLine = 25;
	public int lines = 1;
	public float lineSeparation = 0.4f;
	public Transform enemyPrefab;
	public Enemy[] enemies;
	public Transform spawnA;
	public Transform spawnB;

	// Use this for initialization
	void Start () {
		enemies = new Enemy[enemiesPerLine * lines];

		// Spawn some bad guys
		for (int j = 0; j < lines; j++)
		{
			for (int i = 0; i < enemiesPerLine; i++)
			{
				Vector3 newSpawnPos = Vector3.Lerp(spawnA.position, spawnB.position, i * (1f / enemiesPerLine));
				newSpawnPos.x -= lineSeparation * j;
				Transform newEnemy = Instantiate(Resources.Load("Enemy"), newSpawnPos, Quaternion.identity) as Transform;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
