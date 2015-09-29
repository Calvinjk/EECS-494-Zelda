using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    public bool _________;
    public bool paused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!paused) {
                transform.Find("ItemSelect").GetComponent<UnityEngine.UI.Image>().enabled = true;
                transform.Find("RupeeCount2").GetComponent<UnityEngine.UI.Text>().enabled = true;
                transform.Find("KeyCount2").GetComponent<UnityEngine.UI.Text>().enabled = true;
                transform.Find("BombCount2").GetComponent<UnityEngine.UI.Text>().enabled = true;
                paused = true;
            } else {
                transform.Find("ItemSelect").GetComponent<UnityEngine.UI.Image>().enabled = false;
                transform.Find("RupeeCount2").GetComponent<UnityEngine.UI.Text>().enabled = false;
                transform.Find("KeyCount2").GetComponent<UnityEngine.UI.Text>().enabled = false;
                transform.Find("BombCount2").GetComponent<UnityEngine.UI.Text>().enabled = false;
                paused = false;
            }
        }
    }
}
