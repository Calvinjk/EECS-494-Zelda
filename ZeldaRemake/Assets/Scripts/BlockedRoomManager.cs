using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockedRoomManager : MonoBehaviour {
  public GameObject enemyPrefab;
  public int numEnemies;
  public GameObject rewardPrefab;
  public bool cleared = false;
  private List <GameObject> enemies;
	private int numKilled = 0;
	public GameObject specialEnemyPrefab;
	private GameObject specialEnemy;
	private bool specialEnemyDead = false;

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
			KeeseStatsBlocked script = (KeeseStatsBlocked) specialEnemy.GetComponent(typeof(KeeseStatsBlocked));
			script.rm = this.gameObject;
		}
		for (int i = 0; i < numEnemies - numKilled; i++)
    {
      GameObject enemy = Instantiate(enemyPrefab, this.transform.position + new Vector3(i - 2, i - 2, 0), Quaternion.identity) as GameObject;
      KeeseStatsBlocked script = (KeeseStatsBlocked) enemy.GetComponent(typeof(KeeseStatsBlocked));
      script.rm = this.gameObject;
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
      cleared = true;
			transform.Find("BlockedDoorTrigger/BlockedDoor").GetComponent<BoxCollider>().enabled = false;
			transform.Find("BlockedDoorTrigger/BlockedDoor").GetComponent<SpriteRenderer>().sortingOrder = 0;
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
        }
    }
}
