﻿using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    public bool     _________;
    public bool     paused = false;
    public bool     selected = false;
    public int      curItem = 0;
    LinkStats       linkStats;
    LinkMovement    linkMovement;

    // Use this for initialization
    void Start () {    
        linkStats = (LinkStats)GameObject.Find("Link").GetComponent(typeof(LinkStats));
        linkMovement = (LinkMovement)GameObject.Find("Link").GetComponent(typeof(LinkMovement));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!paused) {
                //Images
                transform.Find("ItemSelect").GetComponent<UnityEngine.UI.Image>().enabled   = true;
                transform.Find("SwordSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                //Weapon Checks
                if (linkStats.hasBoomerang == true) {
                    transform.Find("BoomerangSprite3").GetComponent<UnityEngine.UI.Image>().enabled = true;
                }
                if (linkStats.numBombs > 0) {
                    transform.Find("BombSprite3").GetComponent<UnityEngine.UI.Image>().enabled = true;                    
                }            
                if (linkStats.hasBow == true) {
                    transform.Find("BowSprite3").GetComponent<UnityEngine.UI.Image>().enabled = true;
                }

                //Where to put selection arrow
                if (linkMovement.itemB == "boom") {
                    transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                    if (!selected) {
                        transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled = true;
                        curItem = 1;
                        selected = true;
                    }
                }
                if (linkMovement.itemB == "bomb") {
                    transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                    if (!selected) {
                        transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                        curItem = 2;
                        selected = true;
                    }
                }
                if (linkMovement.itemB == "bow") {
                    transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                    if (!selected) {
                        transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled = true;
                        curItem = 3;
                        selected = true;
                    }
                }

                //Text
                transform.Find("RupeeCount2").GetComponent<UnityEngine.UI.Text>().enabled   = true;
                transform.Find("KeyCount2").GetComponent<UnityEngine.UI.Text>().enabled     = true;
                transform.Find("BombCount2").GetComponent<UnityEngine.UI.Text>().enabled    = true;
                transform.Find("HeartCount2").GetComponent<UnityEngine.UI.Text>().enabled   = true;
                
                paused = true;
            } else {
                //Images
                transform.Find("ItemSelect").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                transform.Find("SwordSprite2").GetComponent<UnityEngine.UI.Image>().enabled     = false;
                transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = false;
                transform.Find("BombSprite3").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                transform.Find("BoomerangSprite3").GetComponent<UnityEngine.UI.Image>().enabled = false;
                transform.Find("BowSprite3").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = false;

                //Text
                transform.Find("RupeeCount2").GetComponent<UnityEngine.UI.Text>().enabled   = false;
                transform.Find("KeyCount2").GetComponent<UnityEngine.UI.Text>().enabled     = false;
                transform.Find("BombCount2").GetComponent<UnityEngine.UI.Text>().enabled    = false;
                transform.Find("HeartCount2").GetComponent<UnityEngine.UI.Text>().enabled   = false;
                
                paused = false;
                selected = false;
            }
        }

        //Selection process
        if (paused) {
            switch (curItem) {
                case 0:
                    print("ERROR IN SELECTION PROCESS. INVALID ITEM");
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                        if (linkStats.hasBow) {
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = false;
                            transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = true;
                            transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = false;
                            transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled       = true;
                            linkMovement.itemB = "bow";
                            curItem = 3;
                        } else if (linkStats.numBombs > 0) {
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = false;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = false;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            linkMovement.itemB = "bomb";
                            curItem = 2;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow)) {
                        if (linkStats.numBombs > 0) {
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = false;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = false;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            linkMovement.itemB = "bomb";
                            curItem = 2;
                        } else if (linkStats.hasBow) {
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = false;
                            transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = true;
                            transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = false;
                            transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled       = true;
                            linkMovement.itemB = "bow";
                            curItem = 3;
                        }
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                        if (linkStats.hasBoomerang) {
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = true;
                            linkMovement.itemB = "boom";
                            curItem = 1;
                        } else if (linkStats.hasBow) {
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = true;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled       = true;
                            linkMovement.itemB = "bow";
                            curItem = 3;
                        }
                    } if (Input.GetKeyDown(KeyCode.RightArrow)) {
                        if (linkStats.hasBow) {
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = true;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled       = true;
                            linkMovement.itemB = "bow";
                            curItem = 3;
                        } else if (linkStats.hasBoomerang) {
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = true;
                            linkMovement.itemB = "boom";
                            curItem = 1;
                        }
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                        if (linkStats.numBombs > 0) {
                            transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            linkMovement.itemB = "bomb";
                            curItem = 2;
                        } else if (linkStats.hasBoomerang) {
                            transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                            transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                            transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                            transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = true;
                            linkMovement.itemB = "boom";
                            curItem = 1;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow)) {
                        if (linkStats.hasBoomerang) {
                            transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                            transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                            transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                            transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = true;
                            linkMovement.itemB = "boom";
                            curItem = 1;
                        } else if (linkStats.numBombs > 0) {
                            transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            linkMovement.itemB = "bomb";
                            curItem = 2;
                        }
                    }
                    break;
                default:
                    print("ERROR IN SELECTION PROCESS. INVALID ITEM");
                    break;
            }
        }
    }
}
