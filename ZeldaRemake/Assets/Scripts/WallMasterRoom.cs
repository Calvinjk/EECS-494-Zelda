using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallMasterRoom : MonoBehaviour {
  public GameObject enemyPrefab;
  public int numEnemies;
  public GameObject rewardPrefab;
  public bool cleared = false;
  private List <GameObject> enemies;
	public int numKilled = 0;

	// Use this for initialization
	void Start () {
		enemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter(Collider coll)
  {

  }

  public void spawnEnemy(Vector3 coords, char firstDir, char secondDir)
  {
		if (enemies.Count < numEnemies - numKilled)
		{
			GameObject enemy = Instantiate(enemyPrefab, coords, Quaternion.identity) as GameObject;
			WallMasterStats script = (WallMasterStats)enemy.GetComponent(typeof(WallMasterStats));
			script.setRoom(this.gameObject);
			script.firstDir = firstDir;
			script.secondDir = secondDir;
			enemies.Add(enemy);
		}
	}
    
  public void killedEnemy(GameObject enemy)
  {
    enemies.Remove(enemy);
    Destroy(enemy);
		++numKilled;
    if (enemies.Count == 0)
    {
      cleared = true;
      if (rewardPrefab)
      {
        Instantiate(rewardPrefab, this.transform.position, Quaternion.identity);
      }
    }
  }

	public void despawnEnemy(GameObject enemy) {
		enemies.Remove(enemy);
		Destroy(enemy);
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
        }
    }
}
