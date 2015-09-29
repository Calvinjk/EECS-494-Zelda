using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossRoom : MonoBehaviour {
  public GameObject enemyPrefab;
  public int numEnemies;
  public GameObject rewardPrefab;
  public bool cleared = false;
  private GameObject boss;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter(Collider coll)
  {
    if (coll.gameObject.tag == "Link" && !cleared)
    {
			if (!cleared)
				spawnEnemies();
    }
  }

  public void spawnEnemies()
  {
    {
      boss = Instantiate(enemyPrefab, new Vector3(74.5f, 49, 0), Quaternion.identity) as GameObject;
      EnemyStats script = (EnemyStats) boss.GetComponent(typeof(EnemyStats));
      script.setRoom(this.gameObject);
    }
  }
    
  public void killedEnemy(GameObject enemy)
  {
    Destroy(enemy);
		cleared = true;
    if (rewardPrefab)
    {
        Instantiate(rewardPrefab, this.transform.position, Quaternion.identity);
    }
  }

  void OnTriggerExit(Collider coll)
  {
    if (coll.gameObject.tag == "Link")
    {
			Destroy(boss);
    }
  }
}
