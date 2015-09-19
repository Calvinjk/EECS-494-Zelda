using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject   keesePrefab;
    public GameObject   keyPrefab;
    public bool         _______________________;
    public bool         room1Spawned = false;
    public GameObject   keese1, 
                        keese2, 
                        keese3;
    public GameObject   key1;
    public bool         key1Spawned  = false;

	
	// Update is called once per frame
	void Update () {
        // Room 1 (Keese x3)
        Vector3 room1Loc = new Vector3(23.5f, 5f, -8f);
        
        // Spawn Keese x3 if link enters Room 1
        if (Camera.main.transform.position == room1Loc && !room1Spawned) {
            keese1 = Instantiate(keesePrefab, new Vector3(19f, 7f, 0f), Quaternion.identity) as GameObject;
            keese2 = Instantiate(keesePrefab, new Vector3(20f, 3f, 0f), Quaternion.identity) as GameObject;
            keese3 = Instantiate(keesePrefab, new Vector3(23f, 6f, 0f), Quaternion.identity) as GameObject;
            room1Spawned = true;
        }

        // Spawn key if all 3 Keese are killed
        if (room1Spawned && keese1 == null && keese2 == null && keese3 == null && !key1Spawned) {
            key1 = Instantiate(keyPrefab, new Vector3(24f, 5f, 0f), Quaternion.identity) as GameObject;
            key1Spawned = true;
        }
	}
}
