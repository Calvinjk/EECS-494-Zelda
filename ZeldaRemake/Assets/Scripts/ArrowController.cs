using UnityEngine;
using System.Collections;

public class ArrowController : WeaponController {

    public float arrowLifetime = 4f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        arrowLifetime -= Time.deltaTime;
        if (arrowLifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag != "Link" && coll.gameObject.tag != "Room" && coll.gameObject.tag != "block")
        {
            Destroy(this.gameObject);
        }
    }
}
