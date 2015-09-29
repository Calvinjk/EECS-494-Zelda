using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {
    public GameObject enemyPrefab;
    public int numEnemies;
    public GameObject rewardPrefab;
    public bool cleared = false;
    private List <GameObject> enemies;

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
        for (int i = 0; i < numEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, this.transform.position + new Vector3(i - 2, i - 2, 0), Quaternion.identity) as GameObject;
            EnemyStats script = (EnemyStats) enemy.GetComponent(typeof(EnemyStats));
            script.setRoom(this.gameObject);
            enemies.Add(enemy);
        }
    }
    
    public void killedEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
        if (enemies.Count == 0) {
            cleared = true;
            if (rewardPrefab) {
                Instantiate(rewardPrefab, this.transform.position, Quaternion.identity);
            }
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.gameObject.tag == "Link") {
            foreach (GameObject enemy in enemies) {
                Destroy(enemy);
            }
            enemies.Clear();
        }
    }
}
