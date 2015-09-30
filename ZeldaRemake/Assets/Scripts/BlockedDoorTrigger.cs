using UnityEngine;
using System.Collections;

public class BlockedDoorTrigger : MonoBehaviour {
	BlockedRoomManager room;

	// Use this for initialization
	void Start () {
		room = (BlockedRoomManager) transform.root.GetComponent(typeof(BlockedRoomManager));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Link" && !room.cleared)
		{
			transform.Find("BlockedDoor").GetComponent<BoxCollider>().enabled = true;
			transform.Find("BlockedDoor").GetComponent<SpriteRenderer>().sortingOrder = 3;
		}
	}
}
