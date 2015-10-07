using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {

    public bool     _________;
    public bool     paused = false;
    public bool     selected = false;
    public int      curItem = 0;
    LinkStats       linkStats;
    LinkMovement    linkMovement;
    public Dictionary<string, bool> roomsVisited;
    public string curRoom = "c1";
    public GameObject link;
    public Vector3 oldPos;

    // Use this for initialization
    void Start () {    
        linkStats = (LinkStats)GameObject.Find("Link").GetComponent(typeof(LinkStats));
        linkMovement = (LinkMovement)GameObject.Find("Link").GetComponent(typeof(LinkMovement));
        link = GameObject.Find("Link");
        roomsVisited = new Dictionary<string, bool>();
        roomsVisited.Add("c1", true);
    }
	
	// Update is called once per frame
	void Update () {
        //Keep the minimap updated
        transform.Find("Minimap").Find("Position").GetComponent<UnityEngine.UI.Image>().GetComponent<RectTransform>().position =
            transform.Find("Minimap").Find(curRoom).GetComponent<UnityEngine.UI.Image>().GetComponent<RectTransform>().position;

        //Make sure the correct information displays
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!paused) {
                //Move Link out of harm's way
                oldPos = link.transform.position;
                link.transform.position = new Vector3(500,0,0);

                //Images
                transform.Find("ItemSelect").GetComponent<UnityEngine.UI.Image>().enabled   = true;
                transform.Find("SwordSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;

                //Re-draw the map
                foreach(KeyValuePair<string, bool> room in roomsVisited) {
                    transform.Find("ItemSelect").Find("InventoryMap").Find(room.Key).GetComponent<UnityEngine.UI.Image>().enabled = true;
                }
                transform.Find("ItemSelect").Find("InventoryMap").Find("Position").GetComponent<UnityEngine.UI.Image>().GetComponent<RectTransform>().position =
                    transform.Find("ItemSelect").Find("InventoryMap").Find(curRoom).GetComponent<UnityEngine.UI.Image>().GetComponent<RectTransform>().position;
                transform.Find("ItemSelect").Find("InventoryMap").Find("Position").GetComponent<UnityEngine.UI.Image>().enabled = true;

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
                if (linkStats.hasBoots == true) {
                    transform.Find("BootsSprite3").GetComponent<UnityEngine.UI.Image>().enabled = true;
                }

                if (linkMovement.itemB == "bomb") {
                    transform.Find("BombSprite4").GetComponent<UnityEngine.UI.Image>().enabled = true;
                } else if (linkMovement.itemB == "boot") { 
                   transform.Find("BootsSprite4").GetComponent<UnityEngine.UI.Image>().enabled = true;
                }

                //Map and compass checks
                if (linkStats.hasMap) {
                    transform.Find("Map").GetComponent<UnityEngine.UI.Image>().enabled = true;
                }
                if (linkStats.hasCompass) {
                    transform.Find("Compass").GetComponent<UnityEngine.UI.Image>().enabled = true;
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
                if (linkMovement.itemB == "boot") {
                    transform.Find("BootsSprite2").GetComponent<UnityEngine.UI.Image>().enabled = true;
                    if (!selected) {
                        transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled = true;
                        curItem = 4;
                        selected = true;
                    }
                }

                //Text
                transform.Find("RupeeCount2").GetComponent<UnityEngine.UI.Text>().enabled   = true;
                transform.Find("KeyCount2").GetComponent<UnityEngine.UI.Text>().enabled     = true;
                transform.Find("BombCount2").GetComponent<UnityEngine.UI.Text>().enabled    = true;
                transform.Find("HeartCount2").GetComponent<UnityEngine.UI.Text>().enabled   = true;
                
                paused = true;
            } else { //DESTROY EVERYTHING
                //Bring link back!
                link.transform.position = oldPos;

                //Images
                transform.Find("ItemSelect").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                transform.Find("SwordSprite2").GetComponent<UnityEngine.UI.Image>().enabled     = false;
                transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                transform.Find("BowSprite2").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                transform.Find("BoomerangSprite2").GetComponent<UnityEngine.UI.Image>().enabled = false;
                transform.Find("BootsSprite2").GetComponent<UnityEngine.UI.Image>().enabled     = false;
                transform.Find("BombSprite3").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                transform.Find("BoomerangSprite3").GetComponent<UnityEngine.UI.Image>().enabled = false;
                transform.Find("BowSprite3").GetComponent<UnityEngine.UI.Image>().enabled       = false;
                transform.Find("BootsSprite3").GetComponent<UnityEngine.UI.Image>().enabled     = false;
                transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                transform.Find("SelectionArrow3").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                transform.Find("Compass").GetComponent<UnityEngine.UI.Image>().enabled          = false;
                transform.Find("Map").GetComponent<UnityEngine.UI.Image>().enabled              = false;
                transform.Find("BombSprite4").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                transform.Find("BootsSprite4").GetComponent<UnityEngine.UI.Image>().enabled     = false;

                //Delete the map
                foreach (KeyValuePair<string, bool> room in roomsVisited)
                {
                    transform.Find("ItemSelect").Find("InventoryMap").Find(room.Key).GetComponent<UnityEngine.UI.Image>().enabled = false;
                }
                transform.Find("ItemSelect").Find("InventoryMap").Find("Position").GetComponent<UnityEngine.UI.Image>().enabled = false;

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
                        } else if (linkStats.hasBoots) {
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BootsSprite2").GetComponent<UnityEngine.UI.Image>().enabled     = true;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BootsSprite1").GetComponent<UnityEngine.UI.Image>().enabled     = true;
                            linkMovement.itemB = "boot";
                            curItem = 4;
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
                        } else if (linkStats.hasBoots) {
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BootsSprite2").GetComponent<UnityEngine.UI.Image>().enabled     = true;
                            transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled      = false;
                            transform.Find("BootsSprite1").GetComponent<UnityEngine.UI.Image>().enabled     = true;
                            linkMovement.itemB = "boot";
                            curItem = 4;
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
                case 4:
                    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
                        if (linkStats.numBombs > 0) {
                            transform.Find("SelectionArrow1").GetComponent<UnityEngine.UI.Image>().enabled  = false;
                            transform.Find("SelectionArrow2").GetComponent<UnityEngine.UI.Image>().enabled  = true;
                            transform.Find("BootsSprite2").GetComponent<UnityEngine.UI.Image>().enabled     = false;
                            transform.Find("BombSprite2").GetComponent<UnityEngine.UI.Image>().enabled      = true;
                            transform.Find("BootsSprite1").GetComponent<UnityEngine.UI.Image>().enabled     = false;
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
