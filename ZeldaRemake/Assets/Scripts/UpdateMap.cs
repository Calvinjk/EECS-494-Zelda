using UnityEngine;
using System.Collections;

public class UpdateMap : MonoBehaviour {
	private HUD hud;
	public string roomStr;

	// Use this for initialization
	void Start () {
		hud = (HUD) GameObject.Find("HUD").GetComponent(typeof(HUD));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Link" && roomStr != "") {
            hud.curRoom = roomStr;
            if (!hud.roomsVisited.ContainsKey(roomStr)) {
                hud.roomsVisited.Add(roomStr, true);
            }
		}
	}
}
