using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {
	public GameObject enemyPrefab;
	public int numEnemies;
	public GameObject rewardPrefab;
	public bool cleared = false;
	private List <GameObject> enemies;
	private int numKilled = 0;
	public GameObject specialEnemyPrefab;
	private GameObject specialEnemy;
	private bool specialEnemyDead = false;
	public List<Vector3> spawnPositions;
	public List<GameObject> pits;
	public GameObject enemyWeapon;


	// Use this for initialization
	void Start () {
		enemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter(Collider coll)
  {
    if (coll.gameObject.tag == "Link" && !cleared)
    {
      //set current room
      spawnEnemies();
    }
  }

  public void spawnEnemies()
  {
		if (specialEnemyPrefab != null && !specialEnemyDead) {
			specialEnemy = Instantiate(specialEnemyPrefab, transform.position, Quaternion.identity) as GameObject;
			EnemyStats script = (EnemyStats) specialEnemy.GetComponent(typeof(EnemyStats));
			script.setRoom(this.gameObject);
		}
		for (int i = 0; i < numEnemies - numKilled; i++)
    {
      GameObject enemy = Instantiate(enemyPrefab, spawnPositions[i], Quaternion.identity) as GameObject;
      EnemyStats script = (EnemyStats) enemy.GetComponent(typeof(EnemyStats));
      script.setRoom(this.gameObject);
      enemies.Add(enemy);
    }
  }
    
  public void killedEnemy(GameObject enemy)
  {
		if (specialEnemy == enemy)
		{
			specialEnemyDead = true;
		}
		else
		{
			enemies.Remove(enemy);
			++numKilled;
		}
    Destroy(enemy);
		if (enemies.Count == 0 && ((specialEnemyPrefab != null && specialEnemyDead == true) || (!specialEnemyPrefab)))
    {
			onClear();
      cleared = true;
      if (rewardPrefab)
      {
        Instantiate(rewardPrefab, this.transform.position, Quaternion.identity);
      }
    }
  }

    void OnTriggerExit(Collider coll)
    {
		if (coll.gameObject.tag == "Link")
		{
		    foreach (GameObject enemy in enemies)
		    {
			  Destroy(enemy);
		    }
		    enemies.Clear();
			if (specialEnemyPrefab != null && !specialEnemyDead)
			{
				Destroy(specialEnemy);
			}
			foreach (GameObject pit in pits) {
				Destroy(pit);
			}
			if (enemyWeapon != null) {
				Destroy(enemyWeapon);
			}
        }
    }

	public virtual void onClear() {

	}
}
