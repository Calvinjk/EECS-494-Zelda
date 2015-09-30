using UnityEngine;
using System.Collections;

public class WallTriggerTop : MonoBehaviour {
	public float timeBetween = 2f;
	private float timePassed = 0f;
	private bool spawned = false;
	private bool entered = false;
	private GameObject link;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (entered)
		{
			timePassed += Time.deltaTime;
			if (!spawned)
			{
				WallMasterRoom room = (WallMasterRoom)GetComponentInParent(typeof(WallMasterRoom));
				if (link.transform.position.x - transform.position.x > 0)
				{
					Vector3 spawnLocation = transform.position;
					spawnLocation.x += 4;
					spawnLocation.y += 4;
					room.spawnEnemy(spawnLocation, 's', 'w');
				}
				else
				{
					Vector3 spawnLocation = transform.position;
					spawnLocation.x -= 4;
					spawnLocation.y += 4;
					room.spawnEnemy(spawnLocation, 's', 'e');
				}
				timePassed = 0;
				spawned = true;
			}
			else if (spawned && timePassed >= timeBetween)
			{
				timePassed = 0;
				spawned = false;
			}
		}
	}

	void OnTriggerEnter(Collider coll) { 
		if (coll.gameObject.tag == "Link") {
			entered = true;
			link = coll.gameObject;
		}
	}
	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Link")
		{
			entered = false;
		}
	}
}
