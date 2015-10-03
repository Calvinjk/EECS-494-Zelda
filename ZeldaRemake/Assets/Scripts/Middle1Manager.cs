using UnityEngine;
using System.Collections;

public class Middle1Manager : RoomManager {

	// Use this for initialization

	
	// Update is called once per frame

	public override void onClear() {
		transform.Find("BlockedDoor").GetComponent<BoxCollider>().isTrigger = true;
		transform.Find("BlockedDoor").GetComponent<SpriteRenderer>().sortingOrder = -1;
	}
}
