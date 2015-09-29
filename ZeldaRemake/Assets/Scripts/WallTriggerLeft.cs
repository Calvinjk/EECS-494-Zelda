using UnityEngine;
using System.Collections;

public class WallTriggerLeft : MonoBehaviour {
	public float timeBetween = 2f;
	private float timePassed = 0f;
	private bool spawned = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider coll) {
		if (coll.gameObject.tag == "Link") {
			timePassed += Time.deltaTime;
			if (!spawned) {
				WallMasterRoom room = (WallMasterRoom)GetComponentInParent(typeof(WallMasterRoom));
				if (coll.transform.position.y - transform.position.y > 0)
				{
					Vector3 spawnLocation = transform.position;
					spawnLocation.x -= 1;
					spawnLocation.y += 1;
					room.spawnEnemy(spawnLocation, 'e', 'n');
				}
				else
				{
					Vector3 spawnLocation = transform.position;
					spawnLocation.x -= 1;
					spawnLocation.y -= 1;
					room.spawnEnemy(spawnLocation, 'e', 's');
				}
				timePassed = 0;
				spawned = true;
			}
			else if (spawned && timePassed >= timeBetween) {
				timePassed = 0;
				spawned = false;
			}
		}

	}
}
