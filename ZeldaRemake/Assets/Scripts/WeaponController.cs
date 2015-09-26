using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	private char weaponDirection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public char getDirection() {
		return weaponDirection;	
	}

	public void setDirection(char dir) {
		weaponDirection = dir;
	}
}
