using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingshot : MonoBehaviour
{
    public GameObject bullet;
    private GameObject newestBullet;
    public int force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            force += 2;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (force > 200)
            {
                newestBullet = Instantiate(bullet, transform.TransformPoint(Vector3.forward * 2), transform.rotation);
                newestBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
            }
            force = 0;
        }
    }
}
