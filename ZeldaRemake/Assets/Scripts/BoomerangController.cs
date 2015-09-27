using UnityEngine;
using System.Collections;

public class BoomerangController : MonoBehaviour {
    public float velocityFactor = 4.0f;
    public float travelDistance = 4f;
    public char direction = 'x';
    private float timePassed = 0f;
    private bool reversed = false;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        timePassed += Time.deltaTime;
        if (timePassed * velocityFactor >= travelDistance && !reversed)
        {
            reversed = true;
            setDirection(direction, -1f);
        }
		}
 
    void OnTriggerEnter(Collider coll)
    {
        if ((coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Link") && !reversed)
        {
            reversed = true;
            setDirection(direction, -1f);
        }

    }


    public void setDirection(char dir, float reverse = 1f)
    {
        direction = dir;
        if (direction == 'n')
            GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * velocityFactor * reverse;
        else if (direction == 'e')
        {
            GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * velocityFactor * reverse;
        }
        else if (direction == 's')
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * velocityFactor * reverse;
        }
        else if (direction == 'w')
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * velocityFactor * reverse;
        }
    }
}
